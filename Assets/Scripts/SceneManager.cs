using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public Button button;

    void Start()
    {
        //Identifies what the current scene is and what button functionality to implement
        int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 0)
        {
            button.onClick.AddListener(() => PlayClicked());
        }
        else
        {
            button.onClick.AddListener(() => ExitClicked());
        }
    }

    private void PlayClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void ExitClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}