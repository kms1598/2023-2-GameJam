using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public static AchiveManager instance;
    public GameObject[] lockAchieve;
    public GameObject[] unlockAchieve;

    enum Achieve {shell, win }
    Achieve[] achieves;
    void Awake()
    {
        if(AchiveManager.instance == null)
        {
            AchiveManager.instance = this;
        }

        achieves = (Achieve[])Enum.GetValues(typeof(Achieve));
        if(!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
        UnlockAchieve();
        DontDestroyOnLoad(gameObject);
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        foreach(Achieve achieve in achieves)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    void UnlockAchieve()
    {
        for(int i = 0; i < lockAchieve.Length; i++)
        {
            string achieveName = achieves[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achieveName) == 1;
            lockAchieve[i].SetActive(!isUnlock);
            unlockAchieve[i].SetActive(isUnlock);
        }
    }

    public void UnlockShell()
    {
        if(PlayerPrefs.GetInt("shell") == 0)
        {
            PlayerPrefs.SetInt("shell", 1);
        }
    }

    public void UnlockWin()
    {
        if (PlayerPrefs.GetInt("win") == 0)
        {
            PlayerPrefs.SetInt("win", 1);
        }
    }
}
