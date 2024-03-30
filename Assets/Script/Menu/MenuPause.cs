using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject ButtonPause;
    [SerializeField] private GameObject ButtonMenuUpgrade;
    [SerializeField] private GameObject Menu;

    public void Pause()
    {
        ButtonMenuUpgrade.SetActive(false);
        ButtonPause.SetActive(false);
        Menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        Menu.SetActive(false);
        ButtonMenuUpgrade.SetActive(true);
        ButtonPause.SetActive(true);
        Time.timeScale = 1f;
    }
}
