using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class toggleDeathMenu : MonoBehaviour
{

    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleEndMenu(float score)
    {
        gameObject.SetActive(true);
        scoreText.text = ((int) score).ToString();
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void toMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
