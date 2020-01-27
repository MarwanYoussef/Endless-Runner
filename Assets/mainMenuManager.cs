using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuManager : MonoBehaviour
{

    private GameObject canvas1;
    private GameObject canvas2;

    // Start is called before the first frame update
    void Start()
    {
        canvas1 = GameObject.FindGameObjectsWithTag("ui1")[0];
        canvas2 = GameObject.FindGameObjectsWithTag("ui1")[1];
        canvas1.SetActive(false);
    }

    public void howToMenu()
    {
        canvas2.SetActive(false);
        canvas1.SetActive(true);
    }
}
