using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using TMPro;
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
        /*
        Player steps into trigger
        Player locked into position, cannot move/look
        Lights flicker, then turn off
        Prompt to turn flashlight on (lock functionality until this point)
        When light on, show shadow self
        Shadow self animate to hit mirror
        Flashlight goes off
        Crack SFX
        Lights on, mirror broken
        Return control to player
        */
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
