using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
	public TextMeshProUGUI timer;
	public float timeToAnswer;
	float currentTime;

	public TextMeshProUGUI question;
	public List<TextMeshProUGUI> rep;
	public List<Image> result;

	List<Question> questionsList;

	bool triggeredQuestion;
	public float fadeSpeed;

	public Color baseTextColor;
	public Color goodAnswerColor;
	public Color badAnswerColor;

	// Start is called before the first frame update
	void Start() {
		questionsList = new List<Question>();
		questionsList.AddRange(GameManager.instance.GetQuestions());
		currentTime = 0f;
	}

	// Update is called once per frame
	void Update() {
		if (currentTime < timeToAnswer && triggeredQuestion) {
			currentTime += Time.deltaTime;
		}
		else {
			currentTime = 0f;
			if (triggeredQuestion) {
				CheckAnswer();
			}
		}

		timer.text = Mathf.Round(timeToAnswer - currentTime) + "";
	}

	public void TriggerQuestion() {
		if (questionsList.Count <= 0) {
			Debug.Log("Plus de questions restante");
			return;
		}

		Question q = questionsList[0];
		triggeredQuestion = true;
		question.text = q.intitule;
		for (int i = 0; i < rep.Count; i++)
		{
			rep[i].text = q.reponses[i];
			StartCoroutine(FadeToColor(fadeSpeed, baseTextColor, rep[i], result[i]));
			StartCoroutine(FadeToColor(fadeSpeed, baseTextColor, rep[i], result[i]));
		}
	}

	void CheckAnswer()
	{
		Question q = questionsList[0];
		int index = q.bonne_Reponse;

		for (int j = 0; j < 3; j++)
		{
			if (j == index)
			{
				StartCoroutine(FadeToColor(fadeSpeed, goodAnswerColor, rep[j], result[j]));
			}
			else
			{
				StartCoroutine(FadeToColor(fadeSpeed, badAnswerColor, rep[j], result[j]));
			}
		}
		questionsList.Remove(q);
		triggeredQuestion = false;

	}

	IEnumerator FadeToColor(float t, Color newColor, TextMeshProUGUI textToChange, Image imageToChange)
	{
		for (float i = 0f; i < t; i += 0.1f)
		{
			imageToChange.color = Color.Lerp(imageToChange.color, newColor, i / t);
			textToChange.color = Color.Lerp(textToChange.color, newColor, i / t);
			yield return null;
		}
	}
}
