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
    TutorialScriptedEvent tutorialScriptedEvent;
    EndingScriptedEvent endingScriptedEvent;
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
        tutorialScriptedEvent = FindObjectOfType<TutorialScriptedEvent>();
        endingScriptedEvent = FindObjectOfType<EndingScriptedEvent>();
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
            !FindObjectOfType<DeathHandler>().IsDead() &&
            tutorialScriptedEvent.GetEventTriggered() &&
            endingScriptedEvent.GetEventTriggered()) {
            Debug.Log("escape pressed");
            menuOnOff = !menuOnOff;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(menuOnOff);
            player.enabled = !menuOnOff;
            if (menuOnOff) {
                SanitySlider.SetActive(false);
                StaminaSlider.SetActive(false);
                FlashLightSlider.SetActive(false);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                VHS.SetActive(false);
            }
            else {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        if (!this.gameObject.transform.GetChild(0).gameObject.activeSelf &&
            SceneManager.GetActiveScene().name != "MainMenu" &&
            !FindObjectOfType<DeathHandler>().IsDead() &&
            tutorialScriptedEvent.GetEventTriggered() &&
            endingScriptedEvent.GetEventTriggered()) {
            player.enabled = true;
            Time.timeScale = 1;
            SanitySlider.SetActive(true);
            StaminaSlider.SetActive(true);
            FlashLightSlider.SetActive(true);
            VHS.SetActive(true);
        }
    }
}
