using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, Question[]>> questions;

    // Start is called before the first frame update
    void Start()
    {
        WWW data = new WWW(Application.streamingAssetsPath + "/" + "QData.json");
        questions = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Question[]>>>(data.text);

        /*foreach (KeyValuePair<string, Dictionary<string, Question[]>> o in questions)
        {
            //Debug.Log(o.Key);
            Debug.Log("Difficulty = "+o.Key);
            foreach (KeyValuePair<string, Question[]> o2 in o.Value)
            {
                Debug.Log(o2.Value[1].reponses[0]);
            }
        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
