using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class EnemyCout
{
    //public static Save Saver ;
    public static event Action<int> OnValueChanged;
    public static int SpaceShipsKilled { get; private set; }
    public static int SpaceShipsKilledMax { get; private set; }

    public void UpdateStat()
    {
        SpaceShipsKilled++;
        if (SpaceShipsKilled > SpaceShipsKilledMax)
        {
            SpaceShipsKilledMax = SpaceShipsKilled;
            ChouseRank(SpaceShipsKilled);
        }

        OnValueChanged?.Invoke(SpaceShipsKilled);
        //Saver.KilledZombie = ZombieKilled;
    }

    public static void ChouseRank(int value)
    {
        switch (value)
        {
            case 0:
                GameManager.Instance.RankText.text = "Молоко";
                break;
            case 5:
                GameManager.Instance.RankText.text = "Капрал";
                break;
            case 10:
                GameManager.Instance.RankText.text = "Сержант";
                break;
            case 15:
                GameManager.Instance.RankText.text = "Капитан";
                break;
            case 20:
                GameManager.Instance.RankText.text = "Командир";
                break;
            case 100:
                GameManager.Instance.RankText.text = "Адмирал";
                break;
        }
    }

    public void SetStat(int value)
    {
        SpaceShipsKilled = value;
        OnValueChanged?.Invoke(SpaceShipsKilled);
    }
}