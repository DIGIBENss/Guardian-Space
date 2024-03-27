using TMPro;
using UnityEngine;

[RequireComponent(typeof(Wallet))]
public class WalletUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _field, _shopField;
    
    private void Awake() =>
        GetComponent<Wallet>().OnValueChanged += Render;

    private void Render(int value)
    {
        _field.text = value.ToString();
        _shopField.text = value.ToString();
    }
        
    
}
