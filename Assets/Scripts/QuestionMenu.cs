using System.Globalization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionMenu : MonoBehaviour
{
    // data
    private string intitule = "";
    private string[] answers = new string[3];
    private int answer = 0;

    // meta data (useful for saving in file)
    private int current_index;
    private GameManager.Categorie current_theme;
    private GameManager.Difficulty current_difficulty;

    private Dictionary<string, Dictionary<string, Question[]>> datas;

    // self references
    private TMP_InputField question_entry;
    
    // external references
    private Transform question;
    private TMP_Text question_label;


    private void Start() {
        question_entry = GetComponent<Transform>().Find("Question Entry").GetComponent<TMP_InputField>();
        StartCoroutine("WaitForData");
    }

    public void loadquestion(GameObject question) {
        // saving gameobject as a transform for conveniance (useful later)
        this.question = question.GetComponent<Transform>();
        // getting question text
        TMP_Text label = this.question.Find("Label").GetComponent<TMP_Text>();
        question_label = label;  // saving label (useful later)
        intitule = label.text;
        // getting the question index in the section
        Transform parent = this.question.parent;
        index = this.question.GetSiblingIndex();
        // getting theme of the question
        theme = parent.parent.Find("Label").GetComponent<TMP_Text>().text;

        // set form values according to saved datas
        question_entry.text = intitule;
    }

    public void savequestion() {

    }

    private void Update() {
        // visualize real time update
        if (question_label)
            question_label.text = question_entry.text;
    }

    IEnumerator WaitForData()
    {
        yield return new WaitForSeconds(1f);
        datas = GameManager.instance.dataManager.questions;

        Debug.Log(datas);
    }
}
