using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{    
    public static GameManager Instance;

    private int totalScore;

    [Header("Ducks")]
    [SerializeField] private GameObject duckOneRight, duckOneLeft, duckTwoRight, duckTwoLeft, duckThreeRight, duckThreeLeft;

    [Header("Spawners")]
    [SerializeField] private GameObject spawnerDownRight, spawnerDownLeft, spawnerUpRight, spawnerUpLeft;

    [Header("UI")]
    [SerializeField] private TMP_Text timeText, scoreText, scoreTextFinish, bestScoreText;
    [SerializeField] private GameObject finishedGame, panelControlGame;
    public GameObject HUDGame;

    [Header("Audio")]
    [SerializeField] private AudioSource ambienceMusic;
    [SerializeField] private AudioClip musicOne, musicTwo;


    //LifeGame
    public float TimeLifeGame;

    void Awake()
    {
        Instance = this;
        totalScore = 0;
        TimeLifeGame = 35f;
        Time.timeScale = 0f;
        scoreText.text = "Score: 0";
        HUDGame.SetActive(false);
        finishedGame.SetActive(false);
        Destroy(GameObject.Find("MusicMenu"));
        panelControlGame.SetActive(true);
    }
    
    private void Start() {

        if(Random.Range(0, 1) == 1) {
            ambienceMusic.clip = musicOne;
        } else {
            ambienceMusic.clip = musicTwo;
        }
        ambienceMusic.Play();
    }

    private void Update() {
        FinishGame();
    }

    public void SumScore(int points)
    {
        totalScore += points;
        scoreText.text = "Score: " + totalScore;
    }

    IEnumerator SpawnTarget()
    {
        int numberSpawn = 0;

        while (!PauseMenu.Instance.IsPause)
        {
            yield return new WaitForSeconds(1f);
            int randomSpawner = Random.Range(1, 4);
            if(numberSpawn == 8) {
                numberSpawn = 0;
            }
            numberSpawn++;
            switch (randomSpawner)
            {
                case 1:
                    Instantiate(TypeDuck(randomSpawner, numberSpawn), 
                        new Vector3(spawnerDownLeft.transform.position.x, spawnerDownLeft.transform.position.y, 0), 
                        Quaternion.identity);
                    break;
                case 2:
                    Instantiate(TypeDuck(randomSpawner, numberSpawn), 
                        new Vector3(spawnerDownRight.transform.position.x, spawnerDownRight.transform.position.y, 0), 
                        Quaternion.identity);
                    break;
                case 3:
                    Instantiate(TypeDuck(randomSpawner, numberSpawn), 
                        new Vector3(spawnerUpLeft.transform.position.x, spawnerUpLeft.transform.position.y, 0), 
                        Quaternion.identity);
                    break;

                case 4:
                    Instantiate(TypeDuck(randomSpawner, numberSpawn), 
                        new Vector3(spawnerUpRight.transform.position.x, spawnerUpRight.transform.position.y, 0), 
                        Quaternion.identity);
                    break;
            }

        }
    }

    private GameObject TypeDuck(int randomSpawner, int numberSpawn)
    {
        GameObject duck = duckOneLeft;

        switch (randomSpawner)
        {
            case 1:
            case 3:
                if(numberSpawn == 5)
                {
                    duck = duckTwoLeft;
                }
                else if(numberSpawn == 8)
                {
                    duck = duckThreeLeft;
                } else {
                    duck = duckOneLeft;
                }
                break;
            case 2:
            case 4:
                if(numberSpawn == 5)
                {
                    duck = duckTwoRight;
                }
                else if(numberSpawn == 8)
                {
                    duck = duckThreeRight;
                } else {
                    duck = duckOneRight;
                }
                break;

        }
        return duck;
    }

    private void FinishGame()
    {
        TimeLifeGame -= Time.deltaTime;
        timeText.text = "Time: " + TimeLifeGame.ToString("0");

        if(TimeLifeGame <= 0f)
        {

            if(totalScore > PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", totalScore);
                bestScoreText.text = "It's your best score!";
            } else {
                bestScoreText.text = "Your best score is: " + PlayerPrefs.GetInt("BestScore");
            }

            HUDGame.SetActive(false);
            finishedGame.SetActive(true);
            scoreTextFinish.text = "Your Score: " + totalScore;
            Time.timeScale = 0f;
        }
    }

    public void ClosePanelGame()
    {
        PauseMenu.Instance.IsPause = false;
        StartCoroutine(SpawnTarget());
        panelControlGame.SetActive(false);
        HUDGame.SetActive(true);        
        Time.timeScale = 1f;
    }

}
