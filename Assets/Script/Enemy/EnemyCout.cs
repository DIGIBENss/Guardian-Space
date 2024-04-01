using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class EnemyCout
{
    //public static Save Saver ;
   // public static event Action<int> OnValueChanged;
    public  int SpaceShipsKilled { get; private set; }
    public  int ZombieKilledMax { get; private set; } 
    public  void SetMax(int value) => ZombieKilledMax = value;
    public void UpdateStat()
    {
        SpaceShipsKilled++;
        if(SpaceShipsKilled > ZombieKilledMax)
        {
            ZombieKilledMax = SpaceShipsKilled;
            ChouseRank(SpaceShipsKilled);
        }
        //OnValueChanged?.Invoke(SpaceShipsKilled);
        //Saver.KilledZombie = ZombieKilled;
    }

    public void ChouseRank(int value)
    {
        switch (value)
        {
            case 0:
                GameManager.Instance.RankText.text = "Новобранец";
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
    public  void SetStat(int value)
    {
        SpaceShipsKilled = value;
        //OnValueChanged?.Invoke(SpaceShipsKilled);
    }
}
