using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Button[] buttons;
    Text info;
    Image logo;

    // Start is called before the first frame update
    void Start()
    {
        buttons = this.gameObject.GetComponentsInChildren<Button>();
        info = this.gameObject.GetComponentInChildren<Text>();
        logo = this.gameObject.GetComponentInChildren<Image>();
        buttons[0].gameObject.SetActive(false);
        info.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Exposition");
    }

    public void Instructions()
    {
        buttons[1].gameObject.SetActive(false);
        buttons[2].gameObject.SetActive(false);
        buttons[3].gameObject.SetActive(false);
        buttons[0].gameObject.SetActive(true);
        info.enabled = true;
        logo.enabled = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        buttons[1].gameObject.SetActive(true);
        buttons[2].gameObject.SetActive(true);
        buttons[3].gameObject.SetActive(true);
        buttons[0].gameObject.SetActive(false);
        info.enabled = false;
        logo.enabled = true;
    }
}
