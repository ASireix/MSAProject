using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	string currentDifficulty;
	[SerializeField]
	string currentCategorie;

	public Difficulty difficulty;
	public Categorie categorie;
	public static GameManager instance;
	public DataManager dataManager;

	public int currentAnswer;

	[SerializeField]
	private float score;

	public float obstacleCourseTime;
	float currentOTime = 0;

	public float pauseTime;

	public float timeToAnswer;

	public bool gameRunning;

	public GameState gameState;

	private QuestionManager questionManager;
	float totalTime;

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
		gameRunning = false;
		totalTime = GetQuestions().Length * (obstacleCourseTime + timeToAnswer) + (GetQuestions().Length * 2 * pauseTime - 1);
		Debug.Log("Total time = " + totalTime);
	}

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Obstacle:
				if (currentOTime >= obstacleCourseTime)
				{
					gameState = GameState.Pause;
				}
				else
				{
					currentOTime += Time.deltaTime;
				}
				break;
            case GameState.Pause:
				StartCoroutine(StartPause(pauseTime,GameState.Question));
				gameState = GameState.Void;
                break;
            case GameState.Question:
                break;
            case GameState.End:
                break;
            default:
                break;
        }
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

	public void ChangeScene(string scenename)
    {
		SceneManager.LoadScene(scenename);
    }

	public IEnumerator StartPause(float duration, GameState nextState)
    {
		yield return new WaitForSeconds(duration);
		if (nextState == GameState.Question)
        {
			questionManager.timeToAnswer = timeToAnswer;
			questionManager.TriggerQuestion();
        }
		gameState = nextState;
    }

	public void SetQuestionManager(QuestionManager q)
    {
		questionManager = q;
    }
}
