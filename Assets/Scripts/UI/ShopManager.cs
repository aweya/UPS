using UnityEngine;
using System.Collections.Generic;
using System;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private ShopEntry shopPrefab;
    [SerializeField] private Transform container;

    [Serializable]
    public class ShopInfos
    {
        public Sprite[] Icons;
        [TextArea(3, 8)] public String Text;
    }

     [SerializeField] private List<ShopInfos> offers = new List<ShopInfos>();
  
    void Start()
    {
        CreateOffers();
    }

    [ContextMenu("CreateEntries")]
  
    private void CreateOffers()
    {
        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }
        foreach(var offer in offers)
        {
            var newOffer = Instantiate(shopPrefab, container);
            newOffer.Setup(offer.Icons[0], offer.Text, offer.Icons[1]);
        }
    }
}
