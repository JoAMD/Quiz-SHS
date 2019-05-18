using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public GameObject data;
    private void Awake()
    {
        if(Data.instance == null)
        {
            Instantiate(data);
        }
    }

    private void Start()
    {
        Data.instance.pseudoStart();
    }

    public void backButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void submitButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
