using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [Header("Resolution")]
    [SerializeField] TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    [Header("HUD")]
    TMP_Dropdown crosshairDropdown;

    [Header("Audio")]
    public AudioMixer masterMixer;

    [Header("References")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();

        #pragma warning disable 0618 //Disables warning for deprecated refreshRate function
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
                filteredResolutions.Add(resolutions[i]);
        }

        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " (" + filteredResolutions[i].refreshRate + " Hz)";
            resolutionOptions.Add(resolutionOption);

            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        #pragma warning restore 0618 //Restores warning

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;  
        resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void SetGraphics(int graphicsIndex)
    {
        QualitySettings.SetQualityLevel(graphicsIndex);
    }

    public void SetVolume(float volume)
    {
        masterMixer.SetFloat("masterVolume", volume);
    }
}
