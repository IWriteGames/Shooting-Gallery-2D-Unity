using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [SerializeField] private GameObject pausePanel;

    public bool IsPause;

    void Awake()
    {
        Instance = this;
        IsPause = true;
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGameButton();
        }
    }

    public void PauseGameButton()
    {
        if (IsPause)
        {
            //Return Game
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            GameManager.Instance.HUDGame.SetActive(true);
            IsPause = false;
        }
        else
        {
            //Pause Game
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            GameManager.Instance.HUDGame.SetActive(true);
            IsPause = true;
        }
    }
}
