using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FMODUnity;

public class OrderEntry : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private Image background;
    private PackageData data;
    private bool packageSpawned = false;
    private Sprite[] icons;

    private void Reset()
    {
        //  textField = GetComponentInChildren<TMP_Text>();
    }

    public void Setup(PackageData data)
    {
        icons = data.icons;
        image.sprite = icons[0];
        textField.SetText(data.packageName);
        background.sprite = data.background;
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
            image.sprite = icons[1];
            GameManager.Instance.SpawnPackage(data, this);
            Debug.Log("Ticket clicked!");
            RuntimeManager.PlayOneShot(data.spawnSound);
            packageSpawned = true;
        }
    }

    public void ReturnPackage()
    {
        image.sprite = icons[0];
        packageSpawned = false;
    }
}
