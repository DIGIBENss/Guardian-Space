using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MenuUI;
    [SerializeField] private GameObject Button;
    [SerializeField] private UiSkills _uiSkills;


    public void Vivod()
    {
        if (MenuUI.activeSelf)
        {
            MenuUI.SetActive(false);
            Time.timeScale = 1f;
            Button.SetActive(true);
        }
        else
        {
            MenuUI.SetActive(true);
            Time.timeScale = 0f;
            Button.SetActive(false);
            _uiSkills.CheckAvailableSkills();
        }
    }
}
