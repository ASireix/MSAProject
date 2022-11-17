using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionMenu : MonoBehaviour
{
	private GameObject question_used;
	private Question question_data = new Question();

	public void loadquestion(GameObject question)
	{
		if (question_used)
		{
			savequestion();
		}
		this.question_used = question;
		// getting question text
		TMP_Text label = question.transform.Find("Label").GetComponent<TMP_Text>();
		intitule = label.text;
		// getting the question index in the section
		Transform parent = this.question.parent;
		this.current_index = this.question.GetSiblingIndex();
		// getting theme of the question
		this.current_theme = parent.parent.Find("Label").GetComponent<TMP_Text>().text;

		// set form values according to saved datas
		question_entry.text = intitule;
	}

	public void savequestion()
	{
		if (GameManager.instance)
			GameManager.instance.dataManager.questions = question_data;
	}

	private void Update()
	{
		// visualize real time update
		if (question_label)
			question_label.text = question_entry.text;
	}
}
