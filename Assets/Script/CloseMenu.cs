using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    [SerializeField] private GameObject UI16x9;
    [SerializeField] private GameObject UI18x9;
    [SerializeField] private GameObject Button;

    public void Close()
    {
        UI18x9.SetActive(false);
        UI16x9.SetActive(false);

        Time.timeScale = 1f;
        Button.SetActive(true);
    }
}
