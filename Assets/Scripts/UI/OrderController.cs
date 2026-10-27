using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text textField;

    private void Reset()
    {
        textField = GetComponentInChildren<TMP_Text>();
    
    }

    public void Setup(Sprite icon, string text)
    {
        image.sprite = icon;
        textField.SetText(text);  
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
