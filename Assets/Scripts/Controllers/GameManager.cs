using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int level = 1;
    public int cubes;

    // Scripted Events
    public bool firstPlaythrough = false;
    public bool firstDoor = false;
    public bool firstContainer = false;
    public bool firstVendingMachine = false;
    public bool firstCube = false;
    public bool firstPause = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        
    }

    public void TriggerHUDDisplay()
    {

    }
}
