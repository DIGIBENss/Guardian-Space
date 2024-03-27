using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public Action<int> OnValueChanged;
    private int _money;
   // [SerializeField] private Save _save;
   // [SerializeField] private SaveLocal _saves;

    public int Money
    {
        get { return _money; }
        set { _money = value; }
    }

  //  private void OnDestroy() => _save.Cash = _money;

    private IEnumerator Start()
    {
     //   _money = _save.Cash;
        yield return new WaitForSeconds(1f);
        OnValueChanged?.Invoke(_money);
    }

    public void AddMoney(int value)
    {
        _money += value;
        OnValueChanged?.Invoke(_money);
     //   _saves.Save();
    }

    public void SpendMoney(int value)
    {
        if (_money - value < 0) return;
        _money -= value;
        OnValueChanged?.Invoke(_money);
    }
}