using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; set; }


    public TMP_Text winTimeOne;
    public TMP_Text winTimeTwo;
    public GameObject player;
    
    private Scene scene;
    private string levelOneMin;
    private string levelOneSec;
    private string levelTwoMin;
    private string levelTwoSec;
    PlayerController playerController;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        scene = SceneManager.GetActiveScene();
        LevelOneWinTime(levelOneMin, levelOneSec);
        LevelTwoWinTime(levelTwoMin, levelTwoSec);
        playerController = player.GetComponent<PlayerController>();

    }


    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        levelOneMin = "";
        levelOneSec = "";
        levelTwoMin = "";
        levelTwoSec = "";
}

    private void Update()
    {

        if (playerController.count >= 10 && scene.name == ("LevelOne"))
        {
            levelOneMin = playerController.min;
            levelOneSec = playerController.sec;
            LevelOneWinTime(levelOneMin, levelOneSec);
            Debug.Log("code ran");
        }

        if (playerController.count >= 10 && scene.name == ("LevelTwo"))
        {
            levelTwoMin = playerController.min;
            levelTwoSec = playerController.sec;
            LevelTwoWinTime(levelTwoMin, levelTwoSec);
        }

    }

    private void LevelTwoWinTime(string levelTwoMin, string levelTwoSec)
    {
        winTimeTwo.text = "Elapsed Time: " + levelTwoMin + ":" + levelTwoSec;
    }

    private void LevelOneWinTime(string levelOneMin, string levelOneSec)
    {
        winTimeOne.text = "Elapsed Time: " + levelOneMin + ":" + levelOneSec;
    }
}
