using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMenu : MonoBehaviour
{
    [SerializeField] private GameObject UI16x9;
    [SerializeField] private GameObject UI18x9;
    [SerializeField] private GameObject Button;


    public void Vivod()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        if (aspectRatio >= 1.8f)
        {
            UI18x9.SetActive(true);
        }
        else
        {
            UI16x9.SetActive(true);
        }
        Time.timeScale = 0f;
        Button.SetActive(false);
    }
}
