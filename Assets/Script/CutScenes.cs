using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutScenes : MonoBehaviour
{
    public Sprite[] cutScenes;
    Image image;
    int i = 0;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(i < 5)
            {
                image.sprite = cutScenes[i];
                i++;
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}
