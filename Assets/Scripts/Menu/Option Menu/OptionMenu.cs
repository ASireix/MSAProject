using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionMenu : MonoBehaviour
{
	[SerializeField] GameObject questionPrefab;
	[SerializeField] MonoBehaviour load_questions;

	void Start()
	{
		LoadThemesWithQuestions();
	}

	void LoadQuestionMenu(GameObject question)
	{
		transform.Find("Questions Menu").GetComponent<QuestionMenu>().load_questions(question);
	}

	async void LoadThemesWithQuestions()
	{
		var data = GameManager.instance.dataManager.questions;

		GameObject game_themes = GameObject.Find("Option Menu/Scroll View/Content/Themes");
		List<string> themes = new List<string>(data["Particuliers"].Keys);

		for (int i = 0; i < data["Particuliers"].Count; i++)
		{
			Transform game_theme = game_themes.transform.GetChild(i);
			Transform game_questions = game_theme.Find("Questions");
			Question[] questions = data["Particuliers"][themes[i]];
			Debug.Log(questions);
			// load theme name
			game_theme.Find("Label").GetComponent<TMP_Text>().text = themes[i];
			if (questions.Length == 0)
			{
				// if there are no questions in the theme
				// resize game_questions to fit the questions in it
				game_questions.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 70);
				// instanciate the questions
				Instantiate(questionPrefab, new Vector3(5, -10, 0), Quaternion.identity, game_questions);
			}
			else
			{
				// if there is at least 1 question in the theme
				game_questions.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 10 + questions.Length * 60);
				for (int j = 0; j < questions.Length; j++)
				{
					GameObject question = Instantiate(questionPrefab, new Vector3(), Quaternion.identity, game_questions);
					// add EventListener
					question.GetComponent<Button>().onClick.AddListener(delegate { LoadQuestionMenu(question); });
					// set question position
					question.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(5, -(10 + 60 * j), 0), Quaternion.identity);
					// load question name
					question.transform.Find("Label").GetComponent<TMP_Text>().text = questions[i].intitule;
				}
			}
		}
	}
}
