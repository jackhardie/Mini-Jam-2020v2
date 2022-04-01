using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class SceneHandler : MonoBehaviour {
    private bool menuOnOff;
    FirstPersonController player;
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
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenu") {
            Debug.Log("escape pressed");
            menuOnOff = !menuOnOff;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(menuOnOff);
            player.enabled = !menuOnOff;
            if (menuOnOff) {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }


        }
    }

}
