using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Scene currentScene;

    void Start()
    {
        AudioManager.Instance.Play("MainMenu");
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return))
        {
            SceneManager.LoadScene("Nivel1");
            AudioManager.Instance.Stop("MainMenu");
            AudioManager.Instance.Play("Nivel1");
            AudioManager.Instance.Play("AmbientTrack2");
        }

        //if (currentScene.name == "Nivel1" && AudioManager.Instance.GetComponent<AudioSource>().isPlaying)
        //{
        //    AudioManager.Instance.Play("Nivel1");
        //    AudioManager.Instance.Play("AmbientTrack2");
        //}
    }
}
