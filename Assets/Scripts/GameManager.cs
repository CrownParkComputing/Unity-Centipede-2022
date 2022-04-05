using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public Centipede centipede;
    public Centipede centipede2;
    public Vector2 centipedeStart;
    public Vector2 centipede2Start;
    public Text scoreText;
    public Text highScoreText;
    private int score;
    private int highScore;
    private int lives = 3;
    private AudioSource mySound;
    public GameObject startPage;
    public GameObject playGamePage;
    public GameObject gameOverPage;
    public GameObject gameStatsPage;

    public MushroomField mushroomField;
    public Blaster blaster;

    bool gameOver = true;
    public bool GameOver { get { return gameOver; } }
    public int Score { get { return score; } }
    enum PageState
    {
        Start,
        GameOver,
        Game
    }
    private void Update()
    {
        if (lives <= 0 &&  Input.anyKeyDown)
        {
            NewGame();
        }
    }



    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (Instance == null) {
            Instance = this;
        }   else {
            Destroy(gameObject);
        }
        SetPageState(PageState.Start);
        mySound = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    public void StartGame()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetPageState(PageState.Game);
        SetScore(0);
        SetLives(3);
        NewLevel();
         
        string newCentipede = "Centipede";
        GameObject Centipede = ObjectPooler.SharedInstance.GetPooledObject(newCentipede);
    }

    private void NewLevel()
    {
        centipede.Respawn(centipedeStart);
        centipede2.Respawn(centipede2Start);
        blaster.Respawn();
        mushroomField.ClearField();
        mushroomField.GenerateField();
    }
  
    
    public void ResetRound()
    {
        lives--;
         if (lives <-0)
         {
             GameEnd();
             return;
         }
         
         blaster.Respawn();
         mushroomField.HealField();
         
    }

    public void IncreaseScore(int amount)
    {
        SetScore(score + amount);
    }

    public void NextLevel()
    {
        centipede.centipedeSpeed *= 1.1f;
        centipede.Respawn(centipedeStart);
    }


    private void SetLives(int value)
    {
        lives = value;
        //livesText.text = lives.ToString();
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.Start:
                startPage.SetActive(true);
                gameStatsPage.SetActive(false);  
                gameOverPage.SetActive(false);
                playGamePage.SetActive(false);
                break;
            case PageState.GameOver:
                gameStatsPage.SetActive(true);      
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                playGamePage.SetActive(false);
                break;
            case PageState.Game:
                gameStatsPage.SetActive(true);
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                playGamePage.SetActive(true);
                break;
        }
    }

    private void GameEnd()
    {
        SetPageState(PageState.GameOver);
        blaster.gameObject.SetActive(false);
        mushroomField.ClearField();

    }
    public void AdvanceScore(int amount)
    {
        SetScore(score + amount);

    }
    private void SetScore (int score)
    {
        this.score = score;
        scoreText.text = "Score:" + score.ToString();

        if (this.score > this.highScore)
        {
            SetHighScore(this.score);
        }

    }

    private void SetHighScore(int highScore)
    { 
        this.highScore = highScore;
        highScoreText.text = "High Score:" + highScore.ToString();

    }
}
