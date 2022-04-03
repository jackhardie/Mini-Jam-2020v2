using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyTutorial : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialPromptObject;
    [SerializeField]
    TextMeshProUGUI tutorialPromptText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(KeyTutorialPrompt());

        }
    }

    IEnumerator KeyTutorialPrompt()
    {
        tutorialPromptText.text = "Find the key in a level to open the exit. Press Q to pick it up when nearby";
        tutorialPromptObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        tutorialPromptObject.SetActive(false);
    }
}
