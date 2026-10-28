using System.Reflection;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class WeightScaleControl : MonoBehaviour
{

    [SerializeField] private BalanceManager balanceManager;

    [SerializeField] public GameObject pointer;
    [SerializeField] public GameObject parentObject;
    [SerializeField] public int radius;
    [SerializeField] public int displayStrength = 4;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    void Update()
    {
        pointer.transform.position = parentObject.transform.position;
        Vector3 pointerPos = new Vector3(balanceManager.balance.x*displayStrength, balanceManager.balance.y*displayStrength, 0);

        pointer.transform.position += pointerPos;
        pointer.transform.RotateAround(parentObject.transform.position,Vector3.forward,45);

        if (Mathf.Abs(balanceManager.balance.x*displayStrength) > radius || Mathf.Abs(balanceManager.balance.y*displayStrength) > radius || Mathf.Abs(balanceManager.balance.x*displayStrength) >= 30 && Mathf.Abs(balanceManager.balance.y*displayStrength) >= 30)
        {
            transform.localPosition = transform.localPosition.normalized * radius;
        }
    }
}
