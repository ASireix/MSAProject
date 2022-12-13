using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	string currentDifficulty;
	string currentCategorie;

	public Difficulty difficulty;
	public Categorie categorie;
	public static GameManager instance;
	public DataManager dataManager;

	public int currentAnswer;

	[SerializeField]
	private float score;


	private void Awake() {
		if (instance != null && instance != this) Destroy(this);
		else instance = this;
	}

	void Start()
	{
		currentAnswer = 0;
		currentDifficulty = difficulty.ToString();
		currentCategorie = categorie.ToString();
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

	public void SetDiffAndCat(Difficulty diff, Categorie cat)
    {
		currentDifficulty = diff.ToString();
		currentCategorie = cat.ToString();
    }

	public void SetDifficulty(Difficulty diff)
	{
		currentDifficulty = diff.ToString();
	}

	public void SetCategorie(Categorie cat)
	{
		currentCategorie = cat.ToString();
	}
}
