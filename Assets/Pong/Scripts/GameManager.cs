using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public ParticleSystem ballExplosion;
    [Range(0.0001f, 1f)] public float ballSpeedIncrease = 0.01f;
    [Range(0.0001f, 1f)] public float ballSpeedAmount = 0.01f;
    [HideInInspector] public GameObject ball;
    private BallBehaviour ballBehaviour;

    [Range(0f, 20f)] public float delay = 20f;

    [Range(0f, 20f)] public float xLimit = 9f;

    private int player1pts = 0;
    private int player2pts = 0;

    private bool gameStarted = false;

    [Min(0)] public int gameTime = 60 * 2;
    private UIManager uiManager;
    private bool gameover = false;

    [HideInInspector] public static GameManager script;

    [Header("IA learning curve")]
    public PaddleController[] players;
    [Range(0.0001f, 0.1f)] public float botIncreaseSpeed = 0.005f;

    void Start()
    {
        GameManager.script = this;

        ball = GameObject.FindGameObjectWithTag("Ball");
        ballBehaviour = ball.GetComponent<BallBehaviour>();

        uiManager = GetComponent<UIManager>();
        uiManager.UpdateTimer(gameTime);
    }
    
    void Update()
    {
        if (!gameStarted)
        {
            if (ballBehaviour.isMoving())
            {
                delay -= Time.deltaTime;
                if (delay <= 0)
                {
                    ballBehaviour.StartMovement();
                    gameStarted = true;
                    StartCoroutine("TimeCount");
                }
            }
        }
        else
        {
            if (!gameover)
            {
                if (Mathf.Abs(ball.transform.position.x) > xLimit)
                {
                    if (ball.transform.position.x < 0)
                    {
                        player2pts++;
                        players[0].botSmoothness += botIncreaseSpeed;
                    }
                    else
                    {
                        player1pts++;
                        players[1].botSmoothness += botIncreaseSpeed;
                    }
                    uiManager.UpdateScore(player1pts, player2pts);
                    DestroyBall();
                    CreateNewBall();
                }
            }
            else
            {
                if (ball != null)
                {
                    DestroyBall();
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void CreateNewBall()
    {
        ball = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
        ballBehaviour = ball.GetComponent<BallBehaviour>();
        ballBehaviour.startMoving = true;
        ballBehaviour.increaseOfSpeed += ballSpeedIncrease;
        ballSpeedIncrease += ballSpeedAmount;
    }

    private void DestroyBall()
    {
        Instantiate(ballExplosion, ball.transform.position, Quaternion.identity);
        Destroy(ball);
    }

    IEnumerator TimeCount()
    {
        while (!gameover)
        {
            yield return new WaitForSecondsRealtime(1f);
            gameTime--;
            uiManager.UpdateTimer(gameTime);

            if (gameTime == 0)
            {
                gameover = true;
            }
        }
    }
}
