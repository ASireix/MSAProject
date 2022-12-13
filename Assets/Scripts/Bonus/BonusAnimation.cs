using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusAnimation : MonoBehaviour
{
    public float rotateSpeed;
    public float maxSize;
    public float minSize;
    public float growSpeed;
    float check;
    Vector3 baseScale;
    // Start is called before the first frame update
    void Start()
    {
        check = minSize;
        baseScale = transform.localScale;     
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed, 0)*Time.deltaTime, Space.Self);
        //Debug.Log(Vector3.Distance(transform.localScale, baseScale * (check / 100)));

        transform.localScale += new Vector3(growSpeed, growSpeed, growSpeed) * Time.deltaTime;
        
        if (CheckAllVector3Values(transform.localScale, baseScale * (check / 100), growSpeed))
        {
            growSpeed = -growSpeed;
            if (growSpeed > 0)
            {
                check = maxSize;
            }
            else
            {
                check = minSize;
            }
            
        }


        
    }

    bool CheckAllVector3Values(Vector3 v1, Vector3 v2, float plusorminus)
    {
        if( plusorminus <= 0)
        {
            return (v1.x <= v2.x && v1.y <= v2.y && v1.z <= v2.z);
        }
        else
        {
            return (v1.x >= v2.x && v1.y >= v2.y && v1.z >= v2.z);
        }
        
    }
}
