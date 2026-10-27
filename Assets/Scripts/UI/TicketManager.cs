using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Animations;
using Mono.Cecil.Cil;

public class TicketManager : MonoBehaviour
{
    [SerializeField] private OrderEntry orderPrefab;
    [SerializeField] private TopTicketEntry topPrefab;
    [SerializeField] private TopTicketEntry bottomPrefab;
    [SerializeField] private Transform container;

    [Serializable]
    public class OrderInfos
    {
        //public Sprite Icon;

        public Sprite[] Icons;
        [TextArea(3, 8)] public String Text;
        public int TicketType;
    }
    [SerializeField] private List<OrderInfos> orders = new List<OrderInfos>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateOrders();
    }

    [ContextMenu("CreateEntries")]

    private void CreateOrders()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        foreach (var order in orders)
        {
            if (order.TicketType == 0)
            {
                var newOrder = Instantiate(topPrefab, container);
                newOrder.TopSetup(order.Icons[0]);
            }
            else if (order.TicketType == 2)
            {
                var newOrder = Instantiate(bottomPrefab, container);
                newOrder.TopSetup(order.Icons[0]);
            }
            else
            {
                var newOrder = Instantiate(orderPrefab, container);
                newOrder.Setup(order.Icons[0], order.Text, order.Icons[1]);
            }

        }
    }
}
