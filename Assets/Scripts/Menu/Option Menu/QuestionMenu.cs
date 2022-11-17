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
    private GameManager.Difficulty current_difficulty;

    private Dictionary<string, Dictionary<string, Question[]>> datas;

    // self references
    private TMP_InputField question_entry;
    private TMP_InputField[] answers_entries;
    private Slider good_answer;
    
    // external references
    private Transform question;  // question getting edited
    private TMP_Text question_label;
    [SerializeField] GameObject questionPrefab;

    private void Start() {
        question_entry = transform.Find("Question Entry").GetComponent<TMP_InputField>();
        answers_entries = new TMP_InputField[3];
        for (int i = 1; i < 4; i++)
            answers_entries[i-1] = transform.Find("Answer" + i + " Entry").GetComponent<TMP_InputField>();
        good_answer = transform.Find("Slider").GetComponent<Slider>();

        current_difficulty = GameManager.Difficulty.Particuliers;
        StartCoroutine("WaitForData");
    }

    public void loadquestion(GameObject question) {
        // saving gameobject as a transform for conveniance (useful later)
        this.question = question.transform;
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
        yield return new WaitForSeconds(3f);
        datas = GameManager.instance.dataManager.questions;

        Transform tthemes = transform.parent.Find("Scroll View").GetChild(0).GetChild(0).GetChild(0);

        int i = 0;
        foreach ((string theme, Question[] questions) in datas["Particuliers"]) {
            Transform tlabel = tthemes.GetChild(i).GetChild(1);
            tlabel.GetComponent<TMP_Text>().text = theme;
            Transform tquestions = tthemes.GetChild(i).GetChild(2);

            if (questions.Length == 0) {
                tquestions.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 70);
                Instantiate(questionPrefab, new Vector3(5, -10, 0), Quaternion.identity, tquestions);
            } else {
                tquestions.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 10 + questions.Length * 60);
            }
            for (int j = 0; j < questions.Length; j++) {
                GameObject _question = Instantiate(questionPrefab, new Vector3(), Quaternion.identity, tquestions);
                _question.GetComponent<Button>().onClick.AddListener(delegate {loadquestion(_question);});
                Transform tquestion = _question.transform;
                tquestion.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(5, -(10 + 60*j), 0), Quaternion.identity);
                tquestion.GetChild(1).GetComponent<TMP_Text>().text = questions[i].intitule;
            }
            i++;
        }
    }

    public void loadDifficulty(GameManager.Difficulty difficulty) {

    }
}
