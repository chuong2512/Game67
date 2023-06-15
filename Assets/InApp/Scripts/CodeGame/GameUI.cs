using System;
using System.Collections.Generic;
using XepHinh;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[Serializable]
public class Collection
{
    public Sprite[] images;
}

public class GameUI : Singleton<GameUI>
{
    public StateGame currentState = StateGame.Pause;

    public GameObject lose;
    public GameObject win;

    public Collection[] rand;

    public Checker[] checkers;

    void Start()
    {
        currentState = StateGame.Playing;

        var random = rand[Random.Range(0, rand.Length)].images;

        for (int i = 0; i < 8; i++)
        {
            checkers[i].SetImage(random[i]);
            checkers[i].SetRightImage(random[i]);
        }

        checkers[8].Hide();
        checkers[8].SetRightImage(null);

        RandomImage();
    }

    private void RandomImage()
    {
        for (int i = 0; i < 99; i++)
        {
            var listMove = new List<Checker>();

            for (int j = 0; j < checkers.Length; j++)
            {
                if (checkers[j].CanMove())
                {
                    listMove.Add(checkers[j]);
                }
            }

            if (listMove.Count > 0)
            {
                listMove[Random.Range(0, listMove.Count)].MoveImageRand();
            }
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShowLose()
    {
        currentState = StateGame.Pause;
        lose.SetActive(true);
    }

    public void ShowWin()
    {
        currentState = StateGame.Pause;
        win.SetActive(true);
    }

    public void RestartGame()
    {
        GameDataManager.Instance.ResetLevel();
        SceneManager.LoadScene("Game");
    }

    public void UpLevel()
    {
        GameDataManager.Instance.playerData.NextLevel();
        SceneManager.LoadScene("Game");
    }

    public void PlayAgain()
    {
        if (GameDataManager.Instance.playerData.intDiamond > 0)
        {
            GameDataManager.Instance.playerData.SubDiamond(1);
        }

        SceneManager.LoadScene("Game");
    }

    public void Check()
    {
        if (CheckWin())
        {
            ShowWin();
        }
    }

    public bool CheckWin()
    {
        for (int i = 0; i < checkers.Length; i++)
        {
            if (!checkers[i].CheckTrue())
            {
                return false;
            }
        }

        return true;
    }

    public void Hint()
    {
        if (GameDataManager.Instance.playerData.intDiamond >= 1)
        {
            GameDataManager.Instance.playerData.SubDiamond(1);

            var listMove = new List<Checker>();

            for (int j = 0; j < checkers.Length; j++)
            {
                if (checkers[j].CanMove())
                {
                    listMove.Add(checkers[j]);
                }
            }

            if (listMove.Count > 0)
            {
                listMove[Random.Range(0, listMove.Count)].MoveImageRand();
            }
        }
    }
}