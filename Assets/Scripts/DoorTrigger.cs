using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DoorTrigger : MonoBehaviour
{
    PickUpItem key;
    [SerializeField]
    GameObject doorPrompt;
    TextMeshProUGUI promptText;

    private void Start()
    {
        promptText = doorPrompt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StopAllCoroutines();
            key = other.GetComponent<PickUpItem>();

            if (key.hasTheKey)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                StartCoroutine(DisplayDoorMessage());
            }
        }
    }

    IEnumerator DisplayDoorMessage()
    {
        promptText.text = "You don't have the key! Go find it somewhere in the level";
        doorPrompt.SetActive(true);
        yield return new WaitForSeconds(5f);
        doorPrompt.SetActive(false);
    }
}
