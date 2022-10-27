using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Difficulty{ Particuliers, Exploitant };
    public enum Categorie { Viticulture, Élevage, Cultures, Jardins_Espaces_Verts };

    public Difficulty difficulty;

    public Categorie categorie;
    
    public static GameManager instance;
    QuestionManager questionManager;
    DataManager dataManager;

    public int currentAnswer;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentAnswer = 0;
        questionManager = GetComponent<QuestionManager>();
        dataManager = GetComponent<DataManager>();

        StartCoroutine("WaitForData");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForData()
    {
        yield return new WaitForSeconds(1f);
        questionManager.SetQuestions(dataManager.questions[difficulty.ToString()][categorie.ToString()]);
        //questionManager.TriggerQuestion();
    }
}
