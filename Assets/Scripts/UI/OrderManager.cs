using System;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    //[SerializeField] private OrderEntry orderPrefab;
    [SerializeField] private Transform container;

    [Serializable]
    public class OrderInfos
    {
        public Sprite Icon;
        [TextArea(3,8)] public String Text;
    }

   // [SerializeField] private List<OrderInfos> orders = new List<OrderInfos>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateOrders();
    }

    [ContextMenu("CreateOrders")]
    private void CreateOrders()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        //foreach (var order in orders)
        {
            //var newOrder = Instantiate(orderPrefab, container);
            //newOrder.Setup(order.Icon, order.Text);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
