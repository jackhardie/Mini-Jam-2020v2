using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class TutorialScriptedEvent : CutSceneScriptedEvent {
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
    GameObject mirrorObject;
    [SerializeField]
    GameObject shadowSelf;
    [SerializeField]
    Transform lockPoint;

    [SerializeField]
    GameObject keyObject;
    [SerializeField]
    GameObject keyTutorialTrigger;

    [SerializeField]
    GameObject[] bloodText;

    FlashLight flashLight;

    public AudioClip lightScareSFX;
    public AudioClip flashlightSFX;
    public AudioSource audioSource;
    public AudioClip heavyScareSFX;

    bool flashLightOn;
    bool playerMoving;
    bool promptActive;

    private void Start()
    {
        this.eventTriggered = false;
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
        audioSource.PlayOneShot(lightScareSFX, .8f);
        yield return new WaitForSeconds(2f);
        LightsOff();
        audioSource.PlayOneShot(flashlightSFX, .8f);
        yield return new WaitForSeconds(0.5f);
        DisplayPrompt();
        while(!flashLightOn) yield return null;
        ShowShadowSelf();
        audioSource.PlayOneShot(heavyScareSFX, .8f);
        yield return new WaitForSeconds(2f);
        ForceFlashlightOff();
        yield return new WaitForSeconds(1f);
        BreakMirror();
        DisplayBloodText();
        LightsOn();
        RevealKey();
        yield return new WaitForSeconds(5f);
        LightsOff();
        HideBloodText();
        yield return new WaitForSeconds(0.5f);
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
        promptActive = true;
        promptText.SetActive(true);
    }


    void ShowShadowSelf()
    {
        promptText.SetActive(false);
        promptActive = false;
        shadowSelf.SetActive(true);
        
    }

    void BreakMirror()
    {
        mirrorObject.SetActive(true);
    }

    void RevealKey()
    {
        keyObject.SetActive(true);
        keyTutorialTrigger.SetActive(true);
    }

    void ForceFlashlightOff()
    {
        flashLight.ForceOffFlashlight();
        Destroy(shadowSelf);
    }

    void DisplayBloodText()
    {
        foreach(GameObject canvas in bloodText)
        {
            canvas.SetActive(true);
        }
    }
    void HideBloodText()
    {
        foreach (GameObject canvas in bloodText)
        {
            canvas.SetActive(false);
        }
    }

    void LightsOn()
    {
        foreach (Light light in sceneLights)
        {
            light.enabled = true;
        }

        fpsController.enabled = true;
        flashLight.enabled = true;
        eventTriggered = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !flashLightOn && eventTriggered && promptActive) flashLightOn = true;

        if (player.transform.position != lockPoint.position && playerMoving == true)
        {
            LockPlayerPosition();
        }
    }



}
