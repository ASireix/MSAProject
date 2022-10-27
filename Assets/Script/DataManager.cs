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
        string json = ReadFromFile("QData.json");
        questions = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Question[]>>>(json);
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

    string ReadFromFile(string fileName)
    {
        string path = Application.streamingAssetsPath + "/" + fileName;
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.Log("No such file");
        }

        return "";
    }

    public void SaveJson(string fileName)
    {
        string path = Application.streamingAssetsPath + "/" + fileName;
        string content = JsonConvert.SerializeObject(questions);
        File.WriteAllText(path, content);
    }

    public void UpdateQuestions()
    {
        // change the "question" dictionary
    }

}
