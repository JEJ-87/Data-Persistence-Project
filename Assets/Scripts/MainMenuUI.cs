using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUI : MonoBehaviour
{
    [Header("Text Objects")]
    [SerializeField] TMP_InputField playerNameEntry;
    [SerializeField] TextMeshProUGUI highscore;

    [Header("Objects To Hide")]
    [SerializeField] GameObject bestScore;

    // Start is called before the first frame update
    void Start()
    {
        playerNameEntry.text = DataManager.Instance.playerName;
        highscore.text = DataManager.Instance.bestPlayerName + " ~ " + DataManager.Instance.highScore.ToString();
        if (!DataManager.Instance.hasSaved)
        {
            bestScore.SetActive(false);
        }
    }

    //Start the game
    public void StartGame()
    {
        DataManager.Instance.playerName = playerNameEntry.text;
        SceneManager.LoadScene(1);
    }

    //Resets the highscore
    public void ResetHighscore()
    {
        DataManager.Instance.DeleteSaveData();
        SceneManager.LoadScene(0);
    }

    //Quit the game
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
