using System.Globalization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionMenu : MonoBehaviour {
    // data getting modified
    private string intitule = "";
    private string[] answers = new string[3];
    private int answer = 0;

    // meta data of edited data
    private int current_index;
    private string current_theme;
    private Difficulty current_difficulty;

    private Dictionary<string, Dictionary<string, Question[]>> datas;

    // self references
    private TMP_InputField question_entry;
    private TMP_InputField[] answers_entries;
    private Slider good_answer;
    
    // external references
    private Transform question;  // question getting edited
    private TMP_Text question_label;


    private void Start() {
        Transform t = GetComponent<Transform>();
        question_entry = t.Find("Question Entry").GetComponent<TMP_InputField>();
        answers_entries = new TMP_InputField[3];
        for (int i = 1; i < 4; i++)
            answers_entries[i-1] = t.Find("Answer" + i + " Entry").GetComponent<TMP_InputField>();
        good_answer = t.Find("Slider").GetComponent<Slider>();

        current_difficulty = Difficulty.Particuliers;
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
        this.current_index = this.question.GetSiblingIndex();
        // getting theme of the question
        this.current_theme = parent.parent.Find("Label").GetComponent<TMP_Text>().text;

        // set form values according to saved datas
        question_entry.text = intitule;
    }

    public void savequestion() {
        // datas[current_difficulty][current_theme][current_index] = new Question();
        GameManager.instance.dataManager.questions = datas;
    }

    private void Update() {
        // visualize real time update
        if (question_label)
            question_label.text = question_entry.text;
    }

    IEnumerator WaitForData() {
        yield return new WaitForSeconds(2f);
        datas = GameManager.instance.dataManager.questions;

        Transform t = GetComponent<Transform>();
        Transform tthemes = t.parent.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0);

        for (int i = 0; i < datas["Particuliers"].Count; i++) {
            // string theme, Question[] questions = datas["Particuliers"];
            // Transform ttheme = tthemes.getChild(i);
            // foreach (Question question in questions) {
                
            // }
        }
    }

    public void loadDifficulty(Difficulty difficulty) {

    }
}
