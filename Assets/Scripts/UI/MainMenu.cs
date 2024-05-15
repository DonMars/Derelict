using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator derelictLogo;
    public Animator menuAnimator;
    public Animator blackFade;
    public Animator pressStartButton;
    public GameObject mainMenu;
    public GameObject mainMenuObject;
    public GameObject introLogos;
    public GameObject extrasMenu;
    public Button viewExtrasButton;
    bool viewExtrasSwitch = false;
    bool startGameSwitch = false;

    Scene currentScene;

    void Start()
    {
        AudioManager.Instance.Stop("Nivel1");
        AudioManager.Instance.Stop("AmbientTrack2");
        AudioManager.Instance.Stop("SpaceStationAmbience");

        AudioManager.Instance.Play("MainMenu");
        currentScene = SceneManager.GetActiveScene();
        
        blackFade.ResetTrigger("fadeout");
        blackFade.SetTrigger("fadeout");
    }

    void Update()
    {
        if (GameManager.Instance.firstPlaythrough && !viewExtrasSwitch)
        {
            viewExtrasButton.interactable = true;
            viewExtrasSwitch = true;
        }

        if (Input.GetKeyUp(KeyCode.Return) && introLogos.activeSelf)
        {
            // Skip Logos
            introLogos.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if(Input.GetKeyUp(KeyCode.Return) && !introLogos.activeSelf && !startGameSwitch)
        {
            startGameSwitch = true;
            pressStartButton.SetTrigger("startPressed");

            mainMenuObject.SetActive(true);
        }
    }

    public void NewGameStart()
    {
        FadeLogoOut();

        menuAnimator.SetTrigger("startGame");

        blackFade.ResetTrigger("fadein");
        blackFade.SetTrigger("fadein");

        StartCoroutine(GameStart());
    }

    public void ShowExtras()
    {
        FadeLogoOut();
        StartCoroutine(DisplayExtras());
    }

    public void DisplayMainMenu()
    {
        //mainMenuObject.SetActive(true);

        menuAnimator.ResetTrigger("RestartMenu");
        menuAnimator.SetTrigger("RestartMenu");
    }

    public void FadeLogoOut()
    {
        derelictLogo.ResetTrigger("logoFadeOut");
        derelictLogo.SetTrigger("logoFadeOut");
    }
    public void RestartLogo()
    {
        derelictLogo.ResetTrigger("logoRestart");
        derelictLogo.SetTrigger("logoRestart");
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2f);
        startGameSwitch = true;
        AudioManager.Instance.Stop("MainMenu");
        AudioManager.Instance.Play("Nivel1");
        AudioManager.Instance.Play("AmbientTrack2");
        SceneManager.LoadScene("Nivel1");
    }

    IEnumerator DisplayExtras()
    {
        menuAnimator.ResetTrigger("viewExtras");
        menuAnimator.SetTrigger("viewExtras");

        yield return new WaitForSeconds(2f);

        extrasMenu.SetActive(true);
    }
}
