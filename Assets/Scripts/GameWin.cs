using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject player;
    public AudioSource winSong;

    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.SetActive(false);

        AudioManager.Instance.Stop("Nivel1");
        AudioManager.Instance.Stop("AmbientTrack2");
        AudioManager.Instance.Stop("SpaceStationAmbience");

        winScreen.SetActive(true);
        winSong.Play();
    }
}
