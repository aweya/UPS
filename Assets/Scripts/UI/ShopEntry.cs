using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopEntry : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private Image background;

    private void Reset()
    {
      //  textField = GetComponentInChildren<TMP_Text>();
    
    }

    public void Setup(Sprite icon, string text, Sprite background)
    {
        image.sprite = icon;
        textField.SetText(text);  
        image.sprite = background;
    }
}
