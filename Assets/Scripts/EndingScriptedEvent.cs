using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class EndingScriptedEvent : MonoBehaviour
{
    FirstPersonController fpsController;
    SanityManager sanity;
    [SerializeField]
    FlashLight flashlight;
    Stamina stamina;

    [SerializeField]
    GameObject playerUI;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject shadowPlayer;
    [SerializeField]
    GameObject thirdPersonCamera;
    [SerializeField]
    GameObject door;
    [SerializeField]
    Transform[] points;
    [SerializeField]
    Light finalLight;
    [SerializeField]
    GameObject bloodMessage;

    bool eventTriggered;
    bool moveToPointB;
    bool moveToPointC;
    private void Start()
    {
        fpsController = player.GetComponent<FirstPersonController>();
        sanity = player.GetComponent<SanityManager>();
        stamina = player.GetComponent<Stamina>();
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
        DisablePlayerUIAndControl();
        ThirdPersonCameraOnOff();
        moveToPointB = true;
        yield return new WaitForSeconds(0.1f);
        RevealShadow();
        if (moveToPointB)
        {
            yield return new WaitForSeconds(4.5f);
            moveToPointB = false;
        }
        ThirdPersonCameraOnOff();
        ShutDoor();
        shadowPlayer.transform.position = points[3].position;
        moveToPointC = true;
        if (moveToPointC)
        {
            yield return new WaitForSeconds(4.5f);
            moveToPointC = false;
        }
        yield return new WaitForSeconds(1f);
        LightsOnOff();
        RevealShadow();
        yield return new WaitForSeconds(2.5f);
        DisplayBloodText();
        LightsOnOff();


    }

    private void DisablePlayerUIAndControl()
    {
        sanity.enabled = false;
        flashlight.ForceOffFlashlight();
        stamina.enabled = false;
        playerUI.SetActive(false);
        fpsController.enabled = false;
    }

    private void ThirdPersonCameraOnOff()
    {
        thirdPersonCamera.SetActive(!thirdPersonCamera.activeSelf);
    }

    private void RevealShadow()
    {
        shadowPlayer.SetActive(!shadowPlayer.activeSelf);
    }

    private void MoveToPointB()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, points[0].position, Time.deltaTime * 0.75f);
        
    }
    private void MoveToPointC()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, points[1].position, Time.deltaTime * 0.75f);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, points[1].rotation, Time.deltaTime * 0.75f);

    }
    private void MoveShadow()
    {
        if (moveToPointB) shadowPlayer.transform.position = Vector3.Lerp(shadowPlayer.transform.position, points[2].position, Time.deltaTime * 0.5f);
    }

    private void ShutDoor()
    {
        door.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void LightsOnOff()
    {
        if (finalLight.intensity > 0) finalLight.intensity = 0;
        else if (finalLight.intensity <= 0) finalLight.intensity = 1;
    }

    private void DisplayBloodText()
    {
        bloodMessage.SetActive(true);
    }

    private void Update()
    {
        if(player.transform.position.x >= -31.8 && moveToPointB == true)
        {
            MoveToPointB();
        }
        if (player.transform.position != points[1].position && moveToPointC == true)
        {
            MoveToPointC();
        }

        if (shadowPlayer.transform.position.x >= -28 && moveToPointB == true)
        {
            MoveShadow();
        }
    }
    public bool GetEventTriggered() {
        return eventTriggered;
    }

}
