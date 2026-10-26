using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public int[] orders;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("HI");
        orders = new int[10];
    }

    // Update is called once per frame
    void Update()
    {
        int listSize = orders.Length;
        //Debug.Log(listSize);
        Debug.Log("HI");
    }
}
