using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject titlePanel, settingsPanel, creditsPanel, loadingPanel;

    // Start is called before the first frame update
    void Start()
    { 
        titlePanel = GameObject.FindGameObjectWithTag("TitlePanel");
        settingsPanel = GameObject.FindGameObjectWithTag("SettingsPanel");
        creditsPanel = GameObject.FindGameObjectWithTag("CreditsPanel");
        loadingPanel = GameObject.FindGameObjectWithTag("LoadingScreen");

        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);

        loadingPanel.SetActive(false);
    }

    public void StartChallenge()
    {
        SceneManager.LoadScene(1);
    }

    public void StartEndless()
    {
        SceneManager.LoadScene(2);
    }

    public void settingsMenuShow()
    {
        settingsPanel.SetActive(true);
    }

    public void settingsMenuHide()
    {
        settingsPanel.SetActive(false);
    }
}
