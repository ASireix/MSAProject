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
    public DataManager dataManager;
    public Scroller Scroller; 

    public int currentAnswer;
    public float ScrollingSpeed = .05f;
    private float _initScrollingSpeed;


    private void Awake() {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    void Start()
    {
        currentAnswer = 0;
        dataManager = GetComponent<DataManager>();
        _initScrollingSpeed = ScrollingSpeed;
        DontDestroyOnLoad(gameObject);
    }

    public DataManager GetDataManager() {
        return dataManager;
    }
}
