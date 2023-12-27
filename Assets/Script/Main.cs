using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject startUI;
    public GameObject achUI;
    public GameObject StartBtn;
    public GameObject EndBtn;
    public GameObject achBtn;
    public GameObject XBtn;

    public void StartGame()
    {
        startUI.SetActive(true);
        StartBtn.SetActive(false);
        EndBtn.SetActive(false);
        achBtn.SetActive(false);
    }

    // Update is called once per frame
    public void EndGame()
    {
        Application.Quit();
    }

    public void OnAch()
    {
        achUI.SetActive(true);
        XBtn.SetActive(true);
    }

    public void OffAch()
    {
        achUI.SetActive(false);
        XBtn.SetActive(false);
    }
}
