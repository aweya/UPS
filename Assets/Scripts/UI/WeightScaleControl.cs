using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class WeightScaleControl : MonoBehaviour
{
    [SerializeField] public GameObject pointer;
    [SerializeField] public GameObject parentObject;
    [SerializeField] public int radius;


    public int weightX;
    public int weightZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }


    void Update()
    {
        //set pointer to new position from the origin point
        pointer.transform.position = parentObject.transform.position;
        Vector3 pointerPos = new Vector3(weightX, weightZ, 0);

        // add new position to the pointer
        pointer.transform.position += pointerPos;

        // restrict pointer from leaving certain radius 
        if (Vector3.Distance(parentObject.transform.position, pointerPos) > radius)
        {
            transform.localPosition = transform.localPosition.normalized * radius;
        }
        





        /*Vector3 FromCircleToObject = pointerPos - groupCenter;
        FromCircleToObject *= radius / FromCircleToObject;
        pointerPos = radius + FromCircleToObject;   */

        

    
        
        
    }
}
