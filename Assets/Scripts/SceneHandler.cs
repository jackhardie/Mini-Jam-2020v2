using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class SceneHandler : MonoBehaviour {
    public bool menuOnOff;
    FirstPersonController player;
    [SerializeField] GameObject SanitySlider;
    [SerializeField] GameObject StaminaSlider;
    [SerializeField] GameObject FlashLightSlider;
    [SerializeField] GameObject VHS;
  public  CutSceneScriptedEvent scriptedEvent;
  
    DeathHandler deathHandler;
    public bool eventTriggered;
    void Awake() {
        int numGameSessions = FindObjectsOfType<SceneHandler>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        player = FindObjectOfType<FirstPersonController>();
        scriptedEvent = FindObjectOfType<CutSceneScriptedEvent>();
        deathHandler = FindObjectOfType<DeathHandler>();
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void TutorialLevel() {
        SceneManager.LoadScene("Tutorial");
    }

    public void RestartLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Maybe reset stamina, mental illness, battery
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenu" &&
            !deathHandler.IsDead()) {
            if (scriptedEvent != null)
            {
                if(scriptedEvent.GetEventTriggered() == false)
                {
                    Debug.Log("escape pressed");
                    menuOnOff = !menuOnOff;
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(menuOnOff);
                    player.enabled = !menuOnOff;
                    if (menuOnOff)
                    {
                        SanitySlider.SetActive(false);
                        StaminaSlider.SetActive(false);
                        FlashLightSlider.SetActive(false);
                        Time.timeScale = 0;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        VHS.SetActive(false);
                    }
                    else
                    {
                        Time.timeScale = 1;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }
            }
            else
            {
                Debug.Log("escape pressed");
                menuOnOff = !menuOnOff;
                this.gameObject.transform.GetChild(0).gameObject.SetActive(menuOnOff);
                player.enabled = !menuOnOff;
                if (menuOnOff)
                {
                    SanitySlider.SetActive(false);
                    StaminaSlider.SetActive(false);
                    FlashLightSlider.SetActive(false);
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    VHS.SetActive(false);
                }
                else
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

            
        }
        if (!this.gameObject.transform.GetChild(0).gameObject.activeSelf &&
            SceneManager.GetActiveScene().name != "MainMenu" &&
              !deathHandler.IsDead()) {
            if (scriptedEvent != null)
            {
                if(scriptedEvent.GetEventTriggered() == false)
                {
                    player.enabled = true;
                    Time.timeScale = 1;
                    SanitySlider.SetActive(true);
                    StaminaSlider.SetActive(true);
                    FlashLightSlider.SetActive(true);
                    VHS.SetActive(true);
                }
            }
            else
            {
                player.enabled = true;
                Time.timeScale = 1;
                SanitySlider.SetActive(true);
                StaminaSlider.SetActive(true);
                FlashLightSlider.SetActive(true);
                VHS.SetActive(true);
            }


        }
    }
}
