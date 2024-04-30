using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI display;
    public int score;
    
    const string FILE_DIR = "/DATA/";
    const string DATA_FILE = "highScores.txt";
    string FILE_FULL_PATH;
    

    public int Score
    {
        get
        {
            return score;
        }

        set { score = value; }

    }
    string highScoresString = "";
    
    List<int> highScores;

    public List<int> HighScores
    {
        get
        {
            if (highScores == null && File.Exists(FILE_FULL_PATH))
            {
                Debug.Log("got from file");
                
                highScores = new List<int>();

                highScoresString = File.ReadAllText(FILE_FULL_PATH);

                highScoresString = highScoresString.Trim();
                
                string[] highScoreArray = highScoresString.Split("\n");

                for (int i = 0; i < highScoreArray.Length; i++)
                {
                    int currentScore = Int32.Parse(highScoreArray[i]);
                    highScores.Add(currentScore);
                }
            }
            else if(highScores == null)
            {
                Debug.Log("NOPE");
                highScores = new List<int>();
                highScores.Add(2);
                highScores.Add(1);
                highScores.Add(0);
            }

            return highScores;
        }
    }
    //public 
        bool isInGame = true;
            
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        FILE_FULL_PATH = Application.dataPath + FILE_DIR + DATA_FILE;
    }

    void Update()
    {
        if (isInGame)
        {
            display.text = "SCORE: " + score;
        }
        else
        {
            display.text = "GAME OVER\nFINAL SCORE: " + score +
                           "\nHigh Scores:\n" + highScoresString;
        }
    }
    public void EndGame()
    {
        isInGame = false;
        SceneManager.LoadScene("End");
        SetHighScore();
    }
    
    bool IsHighScore(int score)
    {

        for (int i = 0; i < HighScores.Count; i++)
        {
            if (highScores[i] < score)
            {
                return true;
            }
        }

        return false;
    }
    
    void SetHighScore()
    {
        if (IsHighScore(score))
        {
            int highScoreSlot = -1;

            for (int i = 0; i < HighScores.Count; i++)
            {
                if (score > highScores[i])
                {
                    highScoreSlot = i;
                    break;
                }
            }
                
            highScores.Insert(highScoreSlot, score);

            highScores = highScores.GetRange(0, 3);

            string scoreBoardText = "";

            foreach (var highScore in highScores)
            {
                scoreBoardText += highScore + "\n";
            }

            highScoresString = scoreBoardText;
                
            File.WriteAllText(FILE_FULL_PATH, highScoresString);
        }
    }
}
