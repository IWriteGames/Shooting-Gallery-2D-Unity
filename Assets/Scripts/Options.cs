using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public static Options Instance;

    [Header("Panels & Buttons")]
    [SerializeField] private GameObject buttonsMainMenu;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject soundPanel;
    [SerializeField] private GameObject videoPanel;

    [SerializeField] private Button buttonSound;
    [SerializeField] private Button buttonVideo;

    [Header("Sound")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private TextMeshProUGUI masterTextValue;
    [SerializeField] private TextMeshProUGUI effectsTextValue;
    [SerializeField] private TextMeshProUGUI musicTextValue;

    [SerializeField] private AudioMixer generalMixer;
    [SerializeField] private AudioMixerGroup effectsMixerGroup;
    [SerializeField] private AudioMixerGroup musicMixerGroup;

    private float masterVolumeSliderValue;
    private float effectsVolumeSliderValue;
    private float musicVolumeSliderValue;


    [Header("Video")]
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Toggle vSyncToggle;
    [SerializeField] private TMP_Dropdown resolutionMenu;
    [SerializeField] private TMP_Dropdown targetFPS;
    [SerializeField] private bool fullScreen;
    [SerializeField] private int resolutionOption;
    [SerializeField] private int targetFPSOption;



    void Awake()
    {
        Instance = this;

        ValueDefaultUser();
        DeactiveAllPanels();
    }

    void Update()
    {
        //Audio
        generalMixer.SetFloat("MasterVolume", Mathf.Log10(masterSlider.value) * 20);
        effectsMixerGroup.audioMixer.SetFloat("EffectsVolume", Mathf.Log10(effectsSlider.value) * 20);
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);

        masterVolumeSliderValue = masterSlider.value * 100;
        effectsVolumeSliderValue = effectsSlider.value * 100;
        musicVolumeSliderValue = musicSlider.value * 100;

        masterTextValue.text = masterVolumeSliderValue.ToString("0");
        effectsTextValue.text = effectsVolumeSliderValue.ToString("0");
        musicTextValue.text = musicVolumeSliderValue.ToString("0");
    }



    private void ValueDefaultUser()
    {
        /*
         * AUDIO
         */
        generalMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("OptionMasterVolume")) * 20);
        effectsMixerGroup.audioMixer.SetFloat("EffectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("OptionEffectsVolume")) * 20);
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("OptionMusicVolume")) * 20);

        masterSlider.value = PlayerPrefs.GetFloat("OptionMasterVolume");
        effectsSlider.value = PlayerPrefs.GetFloat("OptionEffectsVolume");
        musicSlider.value = PlayerPrefs.GetFloat("OptionMusicVolume");

        /*
         * VIDEO
         */

        //FullScreen
        if (PlayerPrefs.GetInt("OptionFullScreen") == 0)
        {
            fullScreenToggle.isOn = false;
            fullScreen = false;
        }
        else
        {
            fullScreenToggle.isOn = true;
            fullScreen = true;
        }

        //vSync
        if (PlayerPrefs.GetInt("OptionVSync") == 0)
        {
            vSyncToggle.isOn = false;
        }
        else
        {
            vSyncToggle.isOn = true;
        }


        //Resolution

        resolutionMenu.value = PlayerPrefs.GetInt("OptionResolution");
        
        if (PlayerPrefs.GetInt("OptionResolution") != resolutionMenu.value)
        {
            ChangeResolution(resolutionOption);
        }

        //Limit FPS
        targetFPS.value = PlayerPrefs.GetInt("OptionLimitFPS");
        if (PlayerPrefs.GetInt("OptionLimitFPS") != targetFPS.value)
        {
            ChangeLimitFPS(targetFPSOption);
        }
    }



    public void SaveSoundOptions()
    {
        PlayerPrefs.SetFloat("OptionMasterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("OptionEffectsVolume", effectsSlider.value);
        PlayerPrefs.SetFloat("OptionMusicVolume", musicSlider.value);
    }

    public void SaveVideoOptions()
    {

        //Save FullScreen
        if (fullScreenToggle.isOn)
        {
            PlayerPrefs.SetInt("OptionFullScreen", 1);
            fullScreen = true;
        }
        else
        {
            PlayerPrefs.SetInt("OptionFullScreen", 0);
            fullScreen = false;
        }

        //Save vSync
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("OptionVSync", 1);
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("OptionVSync", 0);
        }


        if (Screen.fullScreen == false && fullScreenToggle.isOn)
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        if (Screen.fullScreen == true && !fullScreenToggle.isOn)
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        //Resolution
        int resolutionOption = resolutionMenu.value;
        PlayerPrefs.SetInt("OptionResolution", resolutionOption);
        ChangeResolution(resolutionOption);

        //Limit FPS
        int targetFPSOption = targetFPS.value;
        PlayerPrefs.SetInt("OptionLimitFPS", targetFPSOption);
        ChangeLimitFPS(targetFPSOption);

    }

    public void RestoreSoundOptions()
    {
        generalMixer.SetFloat("MasterVolume", Mathf.Log10(1 * 20));
        effectsMixerGroup.audioMixer.SetFloat("EffectsVolume", Mathf.Log10(1 * 20));
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(1 * 20));

        masterSlider.value = 1;
        effectsSlider.value = 1;
        musicSlider.value = 1;

        masterTextValue.text = "1";
        effectsTextValue.text = "1";
        musicTextValue.text = "1";
    }

    public void RestoreVideoOptions()
    {
        //FullScreen
        fullScreenToggle.isOn = true;
        fullScreen = true;

        //vSync
        vSyncToggle.isOn = true;

        //Resolution
        resolutionMenu.value = 1;

        //Limit FPS
        targetFPS.value = 0;
    }

    private void ChangeResolution(int resolutionOption)
    {

        if (resolutionOption == 0)
        {
            Screen.SetResolution(2560, 1440, fullScreen);
        }
        else if (resolutionOption == 1)
        {
            Screen.SetResolution(1920, 1080, fullScreen);
        }
        else if (resolutionOption == 2)
        {
            Screen.SetResolution(1366, 768, fullScreen);
        }
        else if (resolutionOption == 3)
        {
            Screen.SetResolution(1280, 800, fullScreen);
        }
    }

    private void ChangeLimitFPS(int targetFPSOption)
    {
        if (targetFPSOption == 0)
        {
            Application.targetFrameRate = 60;
        }
        else if (targetFPSOption == 1)
        {
            Application.targetFrameRate = 100;
        }
        else if (targetFPSOption == 2)
        {
            Application.targetFrameRate = 144;
        }
        else if (targetFPSOption == 3)
        {
            Application.targetFrameRate = -1;
        }
    }


    //Panel Management
    public void OpenOptionsPanel()
    {
        buttonsMainMenu.SetActive(false);
        ValueDefaultUser();
        ActiveSoundPanel();
    }

    public void SaveExitAllOptions()
    {
        SaveSoundOptions();
        SaveVideoOptions();
        DeactiveAllPanels();
    }

    public void CloseExitOptionsPanel()
    {
        ValueDefaultUser();
        DeactiveAllPanels();
    }

    public void ActiveSoundPanel()
    {
        buttonSound.GetComponent<Image>().color = new Color(0.47f, 0.97f, 0.4f, 0.65f);
        buttonVideo.GetComponent<Image>().color = Color.white;

        optionsPanel.SetActive(true);
        soundPanel.SetActive(true);
        videoPanel.SetActive(false);
    }

    public void ActiveVideoPanel()
    {
        buttonSound.GetComponent<Image>().color = Color.white;
        buttonVideo.GetComponent<Image>().color = new Color(0.47f, 0.97f, 0.4f, 0.65f);

        optionsPanel.SetActive(true);
        soundPanel.SetActive(false);
        videoPanel.SetActive(true);
    }

    public void ActiveControlPanel()
    {
        buttonSound.GetComponent<Image>().color = Color.white;
        buttonVideo.GetComponent<Image>().color = Color.white;

        optionsPanel.SetActive(true);
        soundPanel.SetActive(false);
        videoPanel.SetActive(false);
    }

    public void DeactiveAllPanels()
    {
        optionsPanel.SetActive(false);
        soundPanel.SetActive(false);
        videoPanel.SetActive(false);
        buttonsMainMenu.SetActive(true);
    }
}
