using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    // patterns prédéfinis
    public List<GameObject> PatternOfTiles = new List<GameObject>();
    private List<int> TilesPoolPositionIndexes = new List<int>();// associe une ligne de pool à chaque prefab du pattern dès le start
    private Dictionary<GameObject, int> association_prefab_pool_number;
    public float TILE_SIZE = 4;
    [SerializeField] private uint NB_TILES_IN_FIELD;
    private Vector3 tileSpawnerPosition;
    public Transform poolPosition;
    private Vector3 TilesOrderingDirectionInPool = new Vector3(0, 0, 1); // donne la direction dans laquelle on range dans la pool associée les tuiles inutilisées
    private float SpaceBetweenLinesOfPool = 30f;

    private List<GameObject> ElementsToCopy; // = original tiles prefabs

    private Queue<GameObject> field = null;
    private Queue<int> pool_associated = null;// pour savoir où remettre une tuile; car ne peut pas utiliser le même index que pour récupérer depuis le pattern
    private List<Queue<GameObject>> pool;
    private List<Vector3> poolsPositions;
    private Vector3 speed_vector;
    private int indexPattern = 0;

    public void Start()
    {
        ResetAllScroller();
    }

    void Update()
    {
        // Faire avancer l'aire de jeu
        speed_vector = new Vector3(GameManager.instance.ScrollingSpeed, 0, 0);

        /*
        if (GameManager.Instance.GameIsOver || GameManager.instance.GameIsPaused) return;
        */

        foreach (GameObject go in field)
        {
            go.transform.position -= speed_vector * Time.deltaTime;
        }
    }

    public void ResetAllScroller()
    {
        // Dépiler field + indexes à fond et ranger le contenu dans pool
        // reset valeurs indices et vitesse
        // pool lines
        // pool
        // init field

        bool newgame = field == null;
        
            if (newgame)
            { // first game
                InitializeField();
                print("new game");
                AssociatePoolLinesToTile(PatternOfTiles, 0, TilesPoolPositionIndexes);
                CreatePoolLines();
            }
            else
            { // after game over
                ResetField();
                ResetAllScroller();
                return;
            }
            FillField(newgame);
        
    }

  

    private void InitializeField()
    {
        field = new Queue<GameObject>();
        pool_associated = new Queue<int>();
        pool = new List<Queue<GameObject>>();
        ElementsToCopy = new List<GameObject>();
        association_prefab_pool_number = new Dictionary<GameObject, int>();
    }

    private void CreatePoolLines()
    {
        poolsPositions = new List<Vector3>();
        for (int i = 0; i < ElementsToCopy.Count; i++)
        {
            poolsPositions.Add(new Vector3(poolPosition.position.x, SpaceBetweenLinesOfPool * i, 0));
        }
    }

    private int AssociatePoolLinesToTile(List<GameObject> tile_chain, int pool_line_tmp, List<int> pool_positions)
    {
        foreach (GameObject tile in tile_chain)
        {     // associe prefab du pattern à une ligne de pool
            if (!ElementsToCopy.Contains(tile))
            {
                ElementsToCopy.Add(tile);
                pool.Add(new Queue<GameObject>());
                association_prefab_pool_number.Add(tile, pool_line_tmp);
                pool_line_tmp++;
            }
            pool_positions.Add(association_prefab_pool_number[tile]);
        }
        return pool_line_tmp;
    }

    private void ResetField()
    {
        /*GameObject tile;
        int poolLine;
        while(field.Count > 0){
            tile = field.Dequeue();
            poolLine = pool_associated.Dequeue();
            MoveToPool(tile, poolLine);
            DisableInteractiveElement(tile);
        }
        */
        GameObject tile;
        int poolLine;
        // on détruit tout
        while (field.Count > 0)
        {
            tile = field.Dequeue();
            poolLine = pool_associated.Dequeue();
            Object.Destroy(tile);
        }

        foreach (Queue<GameObject> file in pool)
        {
            while (file.Count > 0)
            {
                tile = file.Dequeue();
                Object.Destroy(tile);
            }
        }

        field = null;
        pool_associated = null;
        pool = null;
        ElementsToCopy = null;
        association_prefab_pool_number = null;
    }

    private void FillField(bool isNewGame)
    {
        // initialisation de field
        indexPattern = 0;

        Vector3 initializer = new Vector3(0, 0, 0);
        GameObject tmpo = null;
        initializer.x = transform.position.x + transform.localScale.x / 2 + TILE_SIZE / 2;
        tileSpawnerPosition = new Vector3(transform.position.x + transform.localScale.x / 2 + NB_TILES_IN_FIELD * TILE_SIZE - TILE_SIZE / 2,
                                                transform.position.y, transform.position.z);

        while (initializer.x <= tileSpawnerPosition.x)
        {
            if (isNewGame)
            {
                tmpo = Instantiate(PatternOfTiles[indexPattern]);
                tmpo.transform.position = poolsPositions[TilesPoolPositionIndexes[indexPattern]];
                tmpo.transform.eulerAngles = new Vector3(0, 90f, 0);
                tmpo.transform.localScale = new Vector3(1.03f, 1, 1);
                
                pool_associated.Enqueue(TilesPoolPositionIndexes[indexPattern]);
            }
            else
            {
                tmpo = PickFromPool(indexPattern);
                AddElementToField(tmpo);
            }

            indexPattern = (indexPattern + 1) % PatternOfTiles.Count;


            field.Enqueue(tmpo);
            tmpo.transform.position = initializer;
            initializer.x += TILE_SIZE;
        }
        indexPattern = (indexPattern - 1) % PatternOfTiles.Count;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && field.Count > 0)
        {   // si l'élément ayant atteint le plan est le gizmo de fin de pattern
            Contact(other.gameObject);
        }
    }

    private void Contact(GameObject other)
    {
        // phase 1 : remplir l'aire de jeu avec une nouvelle tuile et la placer au Spawn
        GameObject nextTile = PickFromPool(ChooseTile());
        AddElementToField(nextTile);

        // phase 1.5 : jouer probablement un son à l'arrivée de la tuile dans l'aire de jeu

        // phase 2 : supprimer la tuile de l'aire de jeu, la mettre dans la réserve et la reset pour plus tard
        GameObject tile = field.Dequeue();
        int poolLine = pool_associated.Dequeue();
        DisableInteractiveElement(tile);
        MoveToPool(tile, poolLine);// met la tuile dans la même pool que là où on a pris la dernière tuile
    }

    private void MoveToPool(GameObject tile, int line)
    {
        tile.transform.position = poolsPositions[line] + TilesOrderingDirectionInPool * tile.transform.localScale.z * pool[line].Count;
        pool[line].Enqueue(tile);
    }

    private void DisableInteractiveElement(GameObject tile)
    {
        /*
        if (tile.GetComponentInChildren<Obstacle>() != null)
        {
            tile.GetComponentInChildren<Obstacle>().ResetObstacle();
        }
        */
    }

    public void AddElementToField(GameObject tileToAdd)
    {
        field.Enqueue(tileToAdd);
        pool_associated.Enqueue(TilesPoolPositionIndexes[indexPattern]);
        tileToAdd.transform.position = tileSpawnerPosition;
    }

    private int ChooseTile()
    {
        indexPattern = (indexPattern + 1) % PatternOfTiles.Count;
        return indexPattern;
    }

    private GameObject PickFromPool(int indexForPoolLine)
    {
        // si pool non vide -> défiler
        // sinon créer instance de la tuile demandée
        // retourner
        Queue<GameObject> poolLine = pool[TilesPoolPositionIndexes[indexForPoolLine]];
        if (poolLine.Count > 0)
        {
            return poolLine.Dequeue();
        }
        else
        {
            GameObject tmpo = Instantiate(PatternOfTiles[indexPattern], poolsPositions[TilesPoolPositionIndexes[indexPattern]], Quaternion.identity);
            tmpo.transform.localScale = new Vector3(1.03f, 1, 1);
            return tmpo;
        }
    }
}