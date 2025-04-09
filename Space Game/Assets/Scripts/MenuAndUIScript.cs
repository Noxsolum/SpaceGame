/*
Notes:

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuAndUIScript : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// List of Variables
    /// </summary>

    // --- Variables ---
    public int launchDist = 0;
    public bool hasDied = false;
    private int levelUnlock = 0;
    private Vector2 startPos = new Vector2(0.0f, 1.9f);
    public Scene currentScene;

    // --- UI ---
    public Text scoreText;
    public Text finalScoreEnd;
    public Text currHighScore;
    public Text endHighScore;
    public Slider _boostSlider;
    
    public GameObject[] levelButtons;
    public GameObject camera_M, levelPanel, winBoxUI, LoadingScreenUI, ResetButton, failBoxUI, SettingsPanel, permObj, startPanel, tArrow;

    // --- Scipts ---
    public ArrowScript arrowScript;
    public CameraScript cameraScript;
    public FailScript failScript;
    public LevelLoader levelLoader;
    public LineRenderer lineScript;
    public PlanetScript planetScript;
    public UpdateScript updateScript;
    public winTrigger winScript;
    public SaveManager _save;


    // --- GameObjects ---
    public GameObject rocketShip, earth;
    public GameObject[] _planetS, _planetM, _planetL, _debugCircles;

    #endregion

    public void Start()
    {
        // --- Getting the Scene ---
        currentScene = SceneManager.GetActiveScene();

        // --- Getting the SaveState ---

        permObj = GameObject.FindGameObjectWithTag("PersistSettings");
        _save = permObj.GetComponent<SaveManager>();

        // --- Getting GameObjects ---
        rocketShip = GameObject.FindGameObjectWithTag("Player");
        camera_M = GameObject.FindGameObjectWithTag("MainCamera");
        earth = GameObject.FindGameObjectWithTag("Home");
        tArrow = GameObject.FindGameObjectWithTag("Arrow");

        updateScript = earth.GetComponent<UpdateScript>();
        cameraScript = camera_M.GetComponent<CameraScript>();
        levelLoader = earth.GetComponent<LevelLoader>();
        arrowScript = tArrow.GetComponent<ArrowScript>();
        startPos = rocketShip.transform.position;

        // --- Getting UI Objects ---
        winBoxUI = GameObject.FindGameObjectWithTag("WinBoxUI");
        ResetButton = GameObject.FindGameObjectWithTag("ResetButton");
        LoadingScreenUI = GameObject.FindGameObjectWithTag("LoadingScreen");
        failBoxUI = GameObject.FindGameObjectWithTag("FailBoxUI");
        SettingsPanel = GameObject.FindGameObjectWithTag("SettingsPanel");
        startPanel = GameObject.FindGameObjectWithTag("StartBox");
        endHighScore = GameObject.Find("Highscore").GetComponent<Text>();

        // --- Getting Scene Specific Gameobjects ---
        if (currentScene.buildIndex == 1)
        {
            Debug.Log("Loading Story Mode");
            levelPanel = GameObject.FindGameObjectWithTag("LevelMenu");
            levelButtons = GameObject.FindGameObjectsWithTag("LevelButtons");
            winBoxUI.SetActive(false);

            for (int x = 1; x < levelButtons.Length; x++)
            {
                levelButtons[x].SetActive(false);
            }
        }
        else if(currentScene.buildIndex == 2 || currentScene.buildIndex == 3)
        {
            Debug.Log("Loading Endless Mode");
            levelLoader.LoadLevel(213.0f, true);
        }

        SettingsPanel.SetActive(false);
        //ResetButton.SetActive(false);
        failBoxUI.SetActive(false);
        tArrow.SetActive(false);

        LoadingScreenUI.SetActive(false);
    }

    public void Update()
    {
        // --- UI Methods ---
        thrustText();
        boostSlider();
        showHighScore();

        // --- Planets ---
        _planetS = GameObject.FindGameObjectsWithTag("PlanetS");
        _planetM = GameObject.FindGameObjectsWithTag("PlanetM");
        _planetL = GameObject.FindGameObjectsWithTag("PlanetL");
        _debugCircles = GameObject.FindGameObjectsWithTag("DebugCircles");
    }


    #region Text/Boost Updates

    /// <summary>
    /// 
    /// </summary>

    // --- UI For the Power ---
    public void thrustText()
    {
        CameraScript cameraScript_x = cameraScript.GetComponent<CameraScript>();
        if (cameraScript_x.launchPower > 500)
        {
            launchDist = 500;
        }
        else { launchDist = cameraScript_x.launchPower; }
        scoreText.text = "Thrust: " + launchDist;


        if(cameraScript_x.isLaunched == true && currentScene.buildIndex == 2)
        {
            scoreText.text = "Score: " + updateScript.endlessScore;
        }
    }

    public void boostSlider()
    {
        _boostSlider.value = cameraScript.boost;
    }

    public void finalScore()
    {
        if(updateScript.endlessScore > _save.finalScore && currentScene.buildIndex == 2)
        {
            _save.finalScore = updateScript.endlessScore;
            Debug.Log("Saved new Highscore");
        }

        if (failBoxUI.activeSelf == true && currentScene.buildIndex == 2)
        {
            finalScoreEnd.text = "Score: " + updateScript.endlessScore;
            endHighScore.text = "Highscore: " + _save.finalScore;
            Debug.Log(_save.finalScore);
            Debug.Log(" ");
        }
    }

    public void showHighScore()
    {
        if(currentScene.buildIndex == 2 && startPanel.activeSelf == true)
        {
            currHighScore.text = "Current Highscore: " + _save.finalScore;
        }
    }

    #endregion

    #region Reset Level

    /// <summary>
    /// Resets the game state.
    /// </summary>

    public void ResetLevel()
    {
        Debug.Log("Reset Level");
        _save.Save();

        rocketShip = GameObject.FindGameObjectWithTag("Player");
        launchDist = 0;
        cameraScript.boost = 1000;
        rocketShip.transform.position = startPos;
        rocketShip.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        rocketShip.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        rocketShip.GetComponent<Rigidbody2D>().angularVelocity = 0;

        if (rocketShip == null)
        {
            Instantiate(Resources.Load("SpaceShip"));
        }

        if(currentScene.buildIndex == 2)
        {
            levelLoader.destroyPlanets(true);
            levelLoader.LoadLevel(213.0f, true);
        }


        cameraScript.isLaunched = false;
        cameraScript.hasAnimated = false;

        camera_M.transform.localPosition = new Vector3(0.0f, 16.0f, -14.66f);
        camera_M.GetComponent<Camera>().orthographicSize = 10;

        hasDied = false;

        failResetHide();
    }

    #endregion

    #region Clear Level

    /// <summary>
    /// Clears all the planets off the board.
    /// </summary>

    private void clearLevel()
    {
        if (_planetS.Length != 0)
        {
            for (int i = 0; i < _planetS.Length; i++)
            {
                Destroy(_planetS[i]);
            }
        }

        if (_planetM.Length != 0)
        {
            for (int i = 0; i < _planetM.Length; i++)
            {
                Destroy(_planetM[i]);
            }
        }

        if (_planetL.Length != 0)
        {
            for (int i = 0; i < _planetL.Length; i++)
            {
                Destroy(_planetL[i]);
            }
        }
        if (_debugCircles.Length != 0)
        {
            for (int i = 0; i < _debugCircles.Length; i++)
            {
                Destroy(_debugCircles[i]);
            }
        }
    }

    #endregion

    #region Hiding and Opening Stuff
    /// <summary>
    /// 
    /// </summary>

    // --- The Win State ---
    public void openWinBox()
    {
        Debug.Log("It's all connected");
        levelUnlock++;
        winBoxUI.SetActive(true);
    }

    public void hideWinBox()
    {
        LoadingScreenUI.SetActive(true);

        clearLevel();
        winBoxUI.SetActive(false);
        levelPanel.SetActive(true);
        levelButtons[levelUnlock].SetActive(true);
        ResetButton.SetActive(false);
        ResetLevel();

        LoadingScreenUI.SetActive(false);
    }


    // --- Hides the Level Select ---
    public void hideMenu()
    {
        LoadingScreenUI.SetActive(true);

        levelPanel.SetActive(false);
        ResetButton.SetActive(true);

        LoadingScreenUI.SetActive(false);
    }

    public void OpenMenu()
    {

    }

    // --- The Loading Screen ---
    public void openLoadingScreen()
    {
        LoadingScreenUI.SetActive(true);
    }

    public void hideLoadingScreen()
    {
        LoadingScreenUI.SetActive(false);
    }


    // --- Fail State ---
    public void failResetShow()
    {
        failBoxUI.SetActive(true);
        finalScore();
    }

    public void failResetHide()
    {
        if(failBoxUI.activeSelf == true)
        {
            failBoxUI.SetActive(false);
        }
    }


    // --- Settings Menu ---
    public void settingsMenuShow()
    {
        SettingsPanel.SetActive(true);
    }

    public void settingsMenuHide()
    {
        SettingsPanel.SetActive(false);
    }

    #endregion

    #region Misc
    /// <summary>
    /// Assorted Misc Functions
    /// 
    /// </summary>

    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene(0);
    }

    #endregion
}
