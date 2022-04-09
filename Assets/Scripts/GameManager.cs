using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [System.Serializable]
    public class LevelData
    {
        public int level;
        public Centipede centipede;
        public Vector2 centipedeStart;
        public Mushroom mushroom;
        public int fleaIntervalSeconds;
        public int snailFrequencySeconds;
    }

    public List<LevelData> levelData;
    int level;

    public static GameManager Instance {get; private set;}
    public Text scoreText;
    public Text highScoreText;
    private int score;
    private int highScore;
    private int lives = 3;
    public GameObject startPage;
    public GameObject playGamePage;
    public GameObject gameOverPage;
    public GameObject gameStatsPage;

    public MushroomField mushroomField;
    public Blaster blaster;
    public Fleas fleas;
    public Snails snails;


    private Centipede centipede;
    private Vector2 startPosition;
    private int fleaIntervalSeconds;
    private int snailFrequencySeconds;

    bool gameOver = true;
    public bool GameOver { get { return gameOver; } }
    public int Score { get { return score; } }
    enum PageState
    {
        Start,
        GameOver,
        Game
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
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    public void NewGame()
    {
        level = 0;
        SetLevelData(level);
        SetPageState(PageState.Game);
        SetScore(0);
        SetLives(3);
        NewLevel();
    }

    private void SetLevelData(int level)
    {
        centipede = levelData[level].centipede;
        startPosition = levelData[level].centipedeStart;
        fleaIntervalSeconds = levelData[level].fleaIntervalSeconds; 
        snailFrequencySeconds = levelData[level].snailFrequencySeconds; 
    }

    private void NewLevel()
    {
        centipede.Respawn(startPosition);
        blaster.Respawn();
        mushroomField.ClearField();
        mushroomField.GenerateField();
        fleas.StartFleas(fleaIntervalSeconds);
        snails.StartSnails(snailFrequencySeconds);
    }
  
    
    public void ResetRound()
    {
        fleas.KillFleas();
        snails.KillSnails();
        lives--;
         if (lives <-0)
         {
             GameEnd();
             return;
         }
         
         blaster.Respawn();
         mushroomField.HealField();
         fleas.StartFleas(fleaIntervalSeconds);
         snails.StartSnails(snailFrequencySeconds);
         
    }

    public void IncreaseScore(int amount)
    {
        SetScore(score + amount);
    }

    public void NextLevel()
    {
        level++;
        SetLevelData(level);
        NewLevel();
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
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
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
        fleas.KillFleas();

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
