using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Pool;

public class Generobstacle : MonoBehaviour
{
    public float timer;
    private float Ptime;

    public BoxCollider zone;

    public GameObject obstacle;

    public ObjectPool<GameObject> obstaclePool;

    RaycastHit hit;

    void Awake()
    {


    }

    // Start is called before the first frame update
    void Start()
    {

        obstaclePool = new ObjectPool<GameObject>(createFunc: () => Instantiate(obstacle),
                                          actionOnGet: (obj) => obj.SetActive(true),
                                          actionOnRelease: (obj) => obj.SetActive(false),
                                          actionOnDestroy: (obj) => Destroy(obj),
                                          false,
                                          100,
                                          200);









        Ptime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < Ptime)
        {
            

            Vector3 point = RandomPointInBounds(zone.bounds);

            if (Physics.Raycast(point + new Vector3(0, 100.0f, 0), Vector3.down, out hit, 200.0f))
            {
                if (hit.collider.gameObject.layer != 8)
                {
                    SpawnObject();
                }
            }
            else
            {
                Debug.Log("there seems to be no ground at this position");
            }



            Ptime = 0;
        }
        else
        {
            Ptime += Time.deltaTime;
        }
    }
    public Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public void SpawnObject()
    {
        GameObject Tobj = obstaclePool.Get();
        Tobj.transform.position = hit.point;
    }

}
