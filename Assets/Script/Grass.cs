using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float rnd = Random.Range(0, 360);
        transform.Rotate(new Vector3(0f,rnd,0f),Space.Self);

        rnd = Random.Range(1f, 1.5f);
        transform.localScale = new Vector3(rnd, rnd, rnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
