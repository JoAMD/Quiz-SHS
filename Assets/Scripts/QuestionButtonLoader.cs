using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionButtonLoader : MonoBehaviour
{
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        int s = -1, m = 0;
        for (int i = 0; i < Data.instance.loadedGameData.Questions.Length; i++)
        {
            m = i;
            if (i >= 6)
            {
                s = 1;
                m = i - 6;
            }
            GameObject gb = Instantiate(button, transform);
            gb.transform.position += new Vector3(s * 125, -90 * m, 0);
            gb.GetComponentInChildren<Text>().text += i + 1;
            gb.GetComponent<Button>().onClick.AddListener(delegate {
                Data.instance.startInBetweenAt = int.Parse(gb.GetComponentInChildren<Text>().text[9].ToString()) - 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            });
        }
    }
}
