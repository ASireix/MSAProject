using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDifficulty(int diff)
    {
        GameManager.instance.SetDifficulty((Difficulty)diff);
    }

    public void ChangeCategorie(int cat)
    {
        GameManager.instance.SetCategorie((Categorie)cat);
    }
}
