using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMngr : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene"); 
    }

    public void Quit()
    {
        Application.Quit();
    }
}
