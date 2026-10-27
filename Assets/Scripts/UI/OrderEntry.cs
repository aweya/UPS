using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrderEntry : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private Image background;
    private PackageData data;
    private bool packageSpawned = false;

    private void Reset()
    {
        //  textField = GetComponentInChildren<TMP_Text>();
    }

    public void Setup(PackageData data)
    {
        image.sprite = data.icons[0];
        textField.SetText(data.packageName);
        //image = data.background;
        this.data = data;
    }

    public void TopSetup(Sprite background)
    {
        image.sprite = background;
    }

    public void TicketClicked()
    {
        if (!packageSpawned)
        {
            GameManager.Instance.SpawnPackage(data);
            Debug.Log("Ticket clicked!");
            packageSpawned = true;
        }
    }
}
