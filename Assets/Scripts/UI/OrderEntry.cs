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
            GameManager.Instance.SpawnPackage(data, this);
            Debug.Log("Ticket clicked!");
            RuntimeManager.PlayOneShot(data.spawnSound);
            packageSpawned = true;
        }
    }

    public void ReturnPackage()
    {
        packageSpawned = false;
    }
}
