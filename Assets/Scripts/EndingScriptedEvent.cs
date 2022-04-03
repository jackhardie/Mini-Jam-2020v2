using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EndingScriptedEvent : CutSceneScriptedEvent {
    FirstPersonController fpsController;
    SanityManager sanity;
    [SerializeField]
    GameObject flashlight;
    Stamina stamina;

    [SerializeField]
    GameObject playerUI;
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject runningPlayer;
    [SerializeField]
    GameObject lightCheck;

    [SerializeField]
    GameObject shadowPlayer;
    [SerializeField]
    GameObject shadowPlayer2;

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
    [SerializeField]
    Transform lookAtPoint;

    public AudioSource audioSource;
    public AudioSource lightAudioSource;
    public AudioClip screamSFX;
    public AudioClip lightscareSFX;
    public AudioClip lightSFX;
    public AudioClip lightFlickerSFX;

    bool moveToPointB;
    bool moveToPointC;
    private void Start()
    {
        this.eventTriggered = false;
        fpsController = player.GetComponent<FirstPersonController>();
        sanity = player.GetComponent<SanityManager>();
        stamina = player.GetComponent<Stamina>();
        lightAudioSource.clip = lightSFX;
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
        SwapModels();
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
        SwapModels();
        flashlight.SetActive(true);
        flashlight.GetComponent<FlashLight>().ForceOffFlashlight();
        ShutDoor();
        shadowPlayer2.SetActive(false);
        
        moveToPointC = true;
        if (moveToPointC)
        {
            lightAudioSource.Play();
            yield return new WaitForSeconds(6f);
            player.GetComponentInChildren<Camera>().transform.LookAt(lookAtPoint.position);
            moveToPointC = false;
            audioSource.PlayOneShot(lightFlickerSFX, .4f);
            LightsOnOff();
            lightAudioSource.Stop();
            yield return new WaitForSeconds(0.5f);
            LightsOnOff();
            lightAudioSource.Play();
            audioSource.PlayOneShot(screamSFX, .8f);
            shadowPlayer.SetActive(true);
        }
        
        yield return new WaitForSeconds(2f);
        LightsOnOff();
        shadowPlayer.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        audioSource.PlayOneShot(lightscareSFX, .6f);
        DisplayBloodText();
        LightsOnOff();

        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(0);


    }

    private void DisablePlayerUIAndControl()
    {
        sanity.enabled = false;
        flashlight.SetActive(false);
        stamina.enabled = false;
        playerUI.SetActive(false);
        fpsController.enabled = false;
    }

    private void ThirdPersonCameraOnOff()
    {
        thirdPersonCamera.SetActive(!thirdPersonCamera.activeSelf);
    }

    private void SwapModels()
    {
        lightCheck.SetActive(!lightCheck.activeSelf);
        runningPlayer.SetActive(!runningPlayer.activeSelf);
    }

    private void RevealShadow()
    {
        shadowPlayer2.SetActive(!shadowPlayer2.activeSelf);
    }

    private void MoveToPointB()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, points[0].position, Time.deltaTime * 0.5f);
        
    }
    private void MoveToPointC()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, points[1].position, Time.deltaTime * 0.5f);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, points[1].rotation, Time.deltaTime * 0.5f);

    }
    private void MoveShadow()
    {
        if (moveToPointB) shadowPlayer2.transform.position = Vector3.Lerp(shadowPlayer2.transform.position, points[2].position, Time.deltaTime * 0.6f);
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
        if (player.transform.localRotation.y <= 269 && moveToPointC == true)
        {
            MoveToPointC();
        }

        if (shadowPlayer.transform.position.x >= -28 && moveToPointB == true)
        {
            MoveShadow();
        }
    }

}
