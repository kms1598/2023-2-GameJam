using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text timer;
    public TMP_Text score;
    public GameObject gameoverUI;
    public Image face;
    public TMP_Text ment;
    public int maxScore = 5;
    public int nowScore;
    float time;

    public Sprite win;
    public Sprite lose;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        Time.timeScale = 1;
        nowScore = 0;
        time = 60;
    }

    void Update()
    {
        if(nowScore == maxScore)
        {
            AchiveManager.instance.UnlockWin();
            face.sprite = win;
            ment.text = "5���� �ݱ��� ��� ã���̱���!\n���ϵ帳�ϴ�!";
            gameoverUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (time > 0)
        {
            timer.text = time.ToString("F0");
            score.text = nowScore.ToString() + "/" + maxScore.ToString();
            time -= Time.deltaTime;
        }
        else
        {
            face.sprite = lose;
            ment.text = nowScore.ToString() + "���� �ݱֹۿ� ã�� ���ϼ̱���!\n���� ��ȸ�� ���������~";
            gameoverUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }
}
