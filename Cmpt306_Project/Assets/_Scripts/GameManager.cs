using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject player;
    public GameObject camera;
    public static bool isBossPhase = false;
    public static int score = 0;
    public int counter = 0;
    public float scoreRate = 1f;
    private float nextScore = 0.0f;
    public TextMeshProUGUI scoreTxt;
    public bool playerDead = false;
    public TextMeshProUGUI text;
    public TextMeshProUGUI arriveText;
    public EnemySpawner enemySpawner;
    public enemyProjectile enemyProj;
    public NormalEnemyDrop normalEnemyDrop;
    public GameObject gameOver;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject bossCanvas;
    public GameObject pauseMenu;
    public TextMeshProUGUI gameScoreText;
    public static bool gameEnded = false;
    public static bool scaleDown = false;

    void Start()
    {
        gameEnded = false;
        scaleDown = false;
        isBossPhase = false;
        gameOver.SetActive(false);
        bossCanvas.SetActive(false);
        pauseMenu.SetActive(true);
        playerDead = false;
        score = 0;
    }

    void Update()
    {
        if (Time.time > nextScore && isBossPhase == false && playerDead == false)
        {
            nextScore = Time.time + scoreRate;
            score += 1;
            counter += 1;
            gameScoreText.text = "Score: " + score.ToString();
            print(score);
            if (counter == 17)
            {
                bossCanvas.SetActive(true);
                ArrivalText script = arriveText.GetComponent<ArrivalText>();
                script.blink();
            }

            if (score % 20 == 0)
            {
                counter = 0;
                changePhase();
            }
        }
    }

    //Reset scene when this is called.
    public void GameOver()
    {
        LeaderboardManager.isGameOver = true;
        isBossPhase = false;
        gameEnded = true;
        pauseMenu.SetActive(false);
        scoreTxt.text = "Score: " + score.ToString();
        gameOver.SetActive(true);
        BlinkText script = text.GetComponent<BlinkText>();
        script.blink();
        Enemy1.GetComponent<Enemy1>().resetStats();
        Enemy2.GetComponent<Enemy2>().resetStats();
        Enemy3.GetComponent<Enemy3>().resetStats();
        StartCoroutine(LoadLeaderboard());
    }

    IEnumerator LoadLeaderboard()
    {
        yield return new WaitForSeconds(4.0f);
        FindObjectOfType<AudioManager>().changeVolume("BackgroundMusic", 0.035f);
        SceneManager.LoadScene("LeaderboardScene");
    }

    public void turnOn()
    {
        scaleDown = true;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int scoreToAdd)
    {
        if (!playerDead)
        {
            Debug.Log(playerDead);
            score += scoreToAdd;
        }
    }

    public int GetPoints()
    {
        return score;
    }

    public void changePhase()
    {
        isBossPhase = !isBossPhase;
        if (isBossPhase)
        {
            // enemySpawner.SpawnBoss();
            GameObject.Find("TerrainGenerator").GetComponent<TerrainGenerator>().switchTerrain();
        }
    }
    public void summonDrop(Vector3 dropPosition)
    {
        normalEnemyDrop.DropItem(dropPosition);
    }

    //Return true if in boss phase, else turn false if in normal phase.
    public static bool getIsBossPhase()
    {
        return isBossPhase;
    }
}