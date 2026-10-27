using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Animations;
using Mono.Cecil.Cil;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private OrderEntry orderPrefab;
    [SerializeField] private Transform container;

    [Serializable]
    public class OrderInfos
    {
        //public Sprite Icon;

        public Sprite[] Icons;
        [TextArea(3, 8)] public String Text;
        //public Sprite Background;
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
        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }
        foreach(var order in orders)
        {
            var newOrder = Instantiate(orderPrefab, container);
            newOrder.Setup(order.Icons[0], order.Text, order.Icons[1]);
        }
    }
}
