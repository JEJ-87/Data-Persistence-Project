using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    [Header("Text Objects")]
    public TextMeshProUGUI highscoreText;
    public Text ScoreText;

    [Header("Objects To Hide")]
    public GameObject highscore;
    public GameObject GameOverText;

    [Header("Brick Spawn Settings")]
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        //Init highscore
        highscoreText.text = "Best Score : " + DataManager.Instance.bestPlayerName + " ~ " + DataManager.Instance.highScore.ToString();
        if (!DataManager.Instance.hasSaved)
        {
            highscore.SetActive(false);
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (m_Points > DataManager.Instance.highScore)
                {
                    DataManager.Instance.bestPlayerName = DataManager.Instance.playerName;
                    DataManager.Instance.highScore = m_Points;
                    DataManager.Instance.hasSaved = true;
                    DataManager.Instance.SaveData();
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        if (m_GameOver && m_Points > DataManager.Instance.highScore)
        {
            DataManager.Instance.bestPlayerName = DataManager.Instance.playerName;
            DataManager.Instance.highScore = m_Points;
            DataManager.Instance.hasSaved = true;
            DataManager.Instance.SaveData();
        }
        SceneManager.LoadScene(0);
    }
}
