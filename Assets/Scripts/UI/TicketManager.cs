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
    [SerializeField] private Sprite[] endSprites;
    public List<PackageData> data;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //CreateOrders(data);
    }

    [ContextMenu("CreateEntries")]

    public void CreateOrders(List<PackageData> packages)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        var topOrder = Instantiate(topPrefab, container);
        topOrder.TopSetup(endSprites[0]);
        foreach (var p in packages)
        {
            var newOrder = Instantiate(orderPrefab, container);
            newOrder.Setup(p);

        }
        var bottomOrder = Instantiate(bottomPrefab, container);
        bottomOrder.TopSetup(endSprites[1]);
    }
}
