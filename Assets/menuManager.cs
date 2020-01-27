using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{

    void Start()
    {
         
    }

    public void toGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void toExit()
    {
        Application.Quit();
    }
}
