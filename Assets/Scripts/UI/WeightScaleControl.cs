using Unity.VisualScripting;
using UnityEngine;

public class WeightScaleControl : MonoBehaviour
{
    [SerializeField] public GameObject pointer;

    private bool weight10 = false;

    public int weightX;
    public int weightZ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (weightX == 10)
        {
            if(!weight10){
                pointer.transform.position = pointer.transform.position + new Vector3(40, 0, 0);
                weight10 = true;
            }
        } else {weight10 = false;}
        if (weightX == 0)
        {
            if(!weight10){
                pointer.transform.position = pointer.transform.position + new Vector3(40, 0, 0);
                weight10 = true;
            }
        } else {weight10 = false;}
    }
}
