using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TopTicketEntry : MonoBehaviour
{
    [SerializeField] private Image Ibackground;


    public void TopSetup(Sprite background)
    { 
        Ibackground.sprite = background;
    }
}
