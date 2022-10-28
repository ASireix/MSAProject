using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Difficulty{ Particuliers, Exploitant };
    string currentDifficulty;
    string currentCategorie;
    public enum Categorie { Viticulture, Élevage, Cultures, Jardins_Espaces_Verts };

    public Difficulty difficulty;
    public Categorie categorie;
    public static GameManager instance;
    public DataManager dataManager;

    public int currentAnswer;


    private void Awake() {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    void Start()
    {
        currentAnswer = 0;
        currentDifficulty = Difficulty.Particuliers.ToString();
        currentCategorie = Categorie.Viticulture.ToString();
        dataManager = GetComponent<DataManager>();
        DontDestroyOnLoad(gameObject);
    }

    public DataManager GetDataManager() {
        return dataManager;
    }

    public Question[] GetQuestions()
    {
        return dataManager.questions[currentDifficulty][currentCategorie];
    }
}
