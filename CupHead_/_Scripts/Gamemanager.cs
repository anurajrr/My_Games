using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Gamemanager : MonoBehaviour
{   
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private GameObject gameOver_Pnl;
    [SerializeField] private GameObject mainmenu_Pnl;
    [SerializeField] private GameObject about_Pnl;
    [Header("Audio Clip")]
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip _coinSFX;
    [SerializeField] private AudioClip _playerHurtSFX;
    [SerializeField] private AudioClip _backgroundSFX;

    [Header("Score")]
    int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public float multiplier = 5;
    float currentScore;
    private int highScore = 0;
    private bool isPlayerAlive = true;




    private float playerHealth = 3;
    private float coins = 0;
    public static Gamemanager instance;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {   
        currentScore= 0;
        healthText.text = " HP : " + playerHealth.ToString(); 
        coinsText.text =  coins.ToString();
        gameOver_Pnl.SetActive(false); 
    }

    private void Update() 
    {   
        if(playerHealth <= 0)
        {
            PlayerDead();
        }
         if (isPlayerAlive)
    {
        currentScore += Time.deltaTime;
        score = Mathf.RoundToInt(currentScore * multiplier);
        scoreText.text = score.ToString();
    }       
    }

    public void IncreaseCoins()
    {   
        AudioManager.instance.PlayCoinsSoundClip(_coinSFX);
        coins++;
        coinsText.text =  coins.ToString(); 
    }

    public void PlayerDead()
    {   
        isPlayerAlive = false;
    gameOver_Pnl.SetActive(true);
    player.SetActive(false);

    // Check if the current score is higher than the high score
    if (score > highScore)
    {
        highScore = score;
        // Update the high score text
        highscoreText.text = highScore.ToString();
    }
    }
   public void PlayerHurt()
    {   
        AudioManager.instance.PlayPlayerHurtSoundClip(_playerHurtSFX);
        playerHealth--;
        // Update the health text in the UI
        healthText.text = " HP : " + playerHealth.ToString(); 
    }

    public void StartGame()
    {   
        AudioManager.instance.PlayBackgroundSoundClip(_backgroundSFX);
        SceneManager.LoadScene("Cuphead _1");
    }

    public void Options()
    {
    }
    public void About()
    {   
        gameOver_Pnl.SetActive(false);
        mainmenu_Pnl.SetActive(false);
        about_Pnl.SetActive(true);
    }
    public void Retry()
    {   
        SceneManager.LoadScene("Cuphead _1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        about_Pnl.SetActive(false);
        mainmenu_Pnl.SetActive(true);
    }



}
