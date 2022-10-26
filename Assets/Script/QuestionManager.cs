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

    // Start is called before the first frame update
    void Start()
    {
        questionsList = new List<Question>();
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < timeToAnswer && triggeredQuestion)
        {
            currentTime += Time.deltaTime;
            
        }
        else
        {
            currentTime = 0f;
            if (triggeredQuestion)
            {
                CheckAnswer();
            }
            
            
        }

        timer.text = Mathf.Round(timeToAnswer - currentTime)+"";
    }

    public void SetQuestions(Question[] questions)
    {
        questionsList.AddRange(questions);
    }

    public void TriggerQuestion()
    {
        Question q = questionsList[0];
        triggeredQuestion = true;

        for (int i = 0; i < rep.Count; i++)
        {
            rep[i].text = q.reponses[i];
        }
    }

    void CheckAnswer()
    {
        //GameManager.instance.currentAnswer
        if (questionsList.Count > 0)
        {
            Question q = questionsList[0];

            StartCoroutine(Fade(fadeSpeed));

            triggeredQuestion = false;
        }
    }

    IEnumerator Fade(float t)
    {
        Question q = questionsList[0];

        int index = q.bonne_Reponse;

        Debug.Log("it worked");
        for (float i=0f; i<t; i+= 0.1f)
        {
            for (int j = 0; j < 3; j++)
            {
                if (j == index)
                {
                    rep[j].color = Color.Lerp(rep[j].color, Color.green, i / t);
                    result[j].color = Color.Lerp(result[j].color, Color.green, i / t);
                }
                else
                {
                    rep[j].color = Color.Lerp(rep[j].color, Color.red, i / t);
                    result[j].color = Color.Lerp(result[j].color, Color.red, i / t);
                }
                yield return null;
            }
            
        }
        
    } 
}
