using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boule : MonoBehaviour
{

    int create = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        create++;
        creBoule();
    }


    void creBoule()
    {
        if (create % 100 == 0)
        { Instantiate(gameObject); }
    }
}
