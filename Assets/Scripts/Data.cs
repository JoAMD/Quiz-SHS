using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Data : MonoBehaviour
{
    public static Data instance;
    private string jsonPath = "data.json";  
    public QuestionsHolder loadedGameData;

    private TextMeshProUGUI questionText;
    private GameObject[] options;
    private int k = 0;

    public int startInBetweenAt = -1;

    public int score = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    public void pseudoStart()
    {
        loadGameData();
        /*
        for (int i = 0; i < loadedGameData.Questions.Length; i++)
        {
            Debug.Log(loadedGameData.Questions[i].Question);
            Debug.Log(loadedGameData.Questions[i].Options[0]);
            Debug.Log(loadedGameData.Questions[i].Options[1]);
            Debug.Log(loadedGameData.Questions[i].Options[2]);
            Debug.Log(loadedGameData.Questions[i].Options[3]);
        }
        */

        //Getting references and initialising
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            initializeAndOnClick();
        }
    }

    private void loadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonPath);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            loadedGameData = JsonUtility.FromJson<QuestionsHolder>(dataAsJson);
        }
        else
        {
            Debug.Log("Error!");
        }
    }

    private void initializeAndOnClick()
    {
        //Back Button
        //Button b = GameObject.Find("BackButton").GetComponent<Button>();//.onClick.AddListener(delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); });
        //b.onClick.AddListener(delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); }); 

        //Questions and Options
        questionText = GameObject.Find("QuestionText").GetComponent<TextMeshProUGUI>();
        options = GameObject.FindGameObjectsWithTag("Option");

        for (int i = 0; i < 4; i++)
        {
            options[i].GetComponent<Button>().onClick.AddListener(checkAnswerAndnextQuestion);
        }

        if (startInBetweenAt == -1)
        {
            checkAnswerAndnextQuestion();
        }
        else
        {
            loadQuestion(startInBetweenAt);
        }
        
    }

    public void checkAnswerAndnextQuestion()
    {
        //check answer
        if (k != 0 && EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text.Equals(loadedGameData.Questions[k - 1].Options[0]))
        {
            score++;
            if(k >= loadedGameData.Questions.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //GetComponent<Camera>().GetComponent<Loader>().submitButton();
            }
        }

        Debug.Log(score);
        if(EventSystem.current.currentSelectedGameObject != null)
            Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text);
        Debug.Log(loadedGameData.Questions[k].Options[0]);

        //load next question
        questionText.text = loadedGameData.Questions[k].Question;

        List<int> list = new List<int>();
        int index;

        for (int i = 0; i < 4; i++)
        {
            list.Add(i);
        }

        for (int i = 0; i < 4; i++)
        {
            TextMeshProUGUI textMeshProUGUI = options[i].GetComponentInChildren<TextMeshProUGUI>();

            index = Random.Range(0, list.Count - 1);
            //Debug.Log(index);

            textMeshProUGUI.text = loadedGameData.Questions[k].Options[list[index]];

            list.RemoveAt(index);
        }
        ++k;
    }

    public void loadQuestion(int n)
    {
        //Debug.Log(n);
        questionText.text = loadedGameData.Questions[n].Question;

        List<int> list = new List<int>();
        int index;

        for (int i = 0; i < 4; i++)
        {
            list.Add(i);
        }

        for (int i = 0; i < 4; i++)
        {
            TextMeshProUGUI textMeshProUGUI = options[i].GetComponentInChildren<TextMeshProUGUI>();

            index = Random.Range(0, list.Count - 1);
            //Debug.Log(index);

            textMeshProUGUI.text = loadedGameData.Questions[n].Options[list[index]];

            list.RemoveAt(index);
        }
        k = n + 1;
    }
}
