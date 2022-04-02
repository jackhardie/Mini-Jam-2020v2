using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class TutorialScriptedEvent : MonoBehaviour
{
    FirstPersonController fpsController;
    [SerializeField]
    GameObject promptText;
    [SerializeField]
    Light[] sceneLights;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject mirroredPlayer;
    [SerializeField]
    GameObject shadowSelf;
    [SerializeField]
    Transform lockPoint;

    FlashLight flashLight;

    public AudioClip lightScareSFX;
    public AudioSource audioSource;

    bool eventTriggered;
    bool flashLightOn;
    bool playerMoving;

    private void Start()
    {
        fpsController = player.GetComponent<FirstPersonController>();
        flashLight = player.GetComponentInChildren<FlashLight>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !eventTriggered)
        {
            eventTriggered = true;
            StartCoroutine(ScriptedEvent());

        }
    }

    IEnumerator ScriptedEvent()
    {
        fpsController.enabled = false;
        playerMoving = true;
        yield return new WaitForSeconds(2f);
        playerMoving = false;
        mirroredPlayer.SetActive(true);
        yield return new WaitForSeconds(2f);
        LightsOff();
        yield return new WaitForSeconds(0.5f);
        DisplayPrompt();
        while(!flashLightOn) yield return null;
        ShowShadowSelf();
        yield return new WaitForSeconds(2f);
        ForceFlashlightOff();
        yield return new WaitForSeconds(1f);
        LightsOn();
        Destroy(this);
    }

    void LockPlayerPosition()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, lockPoint.position, Time.deltaTime * 2);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, lockPoint.rotation, Time.deltaTime * 2);
    }

    void LightsOff()
    {
        foreach(Light light in sceneLights)
        {
            light.enabled = false;
        }
        Destroy(mirroredPlayer);
    }

    void DisplayPrompt()
    {
        flashLight.enabled = true;
        promptText.SetActive(true);
    }


    void ShowShadowSelf()
    {
        promptText.SetActive(false);
        shadowSelf.SetActive(true);
        audioSource.PlayOneShot(lightScareSFX, .8f);
    }

    void ForceFlashlightOff()
    {
        flashLight.ForceOffFlashlight();
        flashLight.enabled = false;
        Destroy(shadowSelf);
    }

    void LightsOn()
    {
        foreach (Light light in sceneLights)
        {
            light.enabled = true;
        }

        fpsController.enabled = true;
        flashLight.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !flashLightOn) flashLightOn = true;

        if (player.transform.position != lockPoint.position && playerMoving == true)
        {
            LockPlayerPosition();
        }
    }
}
