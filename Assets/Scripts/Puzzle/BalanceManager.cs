using UnityEngine;

public class BalanceManager : MonoBehaviour
{
    [SerializeField] private PuzzleGrid grid;
    public float swingAmaount = 2f; // in degrees
    public float balanceBias = 1f;
    public float maxBalnceOffset = 20f;

    public float xBalance;
    public float zBalance;


    private float xMiddle;
    private float zMiddle;

    void Start()
    {
        Initialize(grid.GridCells);
    }

    void Update()
    {
        (xBalance, zBalance) = Checkbalance(grid.GridCells);
        // print("xbalance" + xBalance);
        TiltPlatform(xBalance, zBalance);
    }


    (float xSum, float zSum) Checkbalance(GridCell[,] gridCells)
    {
        //looks at every cell and calculates balance
        //Fr = wight * Hebel(distance from center) add all frs = balance

        float xSum = 0;
        float zSum = 0;

        //read every cell

        for (int x = 0; x < gridCells.GetLength(0); x++)
        {
            for (int z = 0; z < gridCells.GetLength(1); z++)
            {
                //if not empty get weight
                float weight = gridCells[x, z].GetWeight();
                //cal hebel
                float xHebel = x - (xMiddle);
                float yHebel = z - (zMiddle);
                //add to sum
                xSum += weight * xHebel;
                zSum += weight * yHebel;
            }
        }

        return (xSum, zSum);
    }

    void TiltPlatform(float xBalance, float zBalance)
    {
        // tilt plattform :)
        float xTilt = Mathf.Clamp(zBalance, -maxBalnceOffset, maxBalnceOffset) * swingAmaount;
        float zTilt = Mathf.Clamp(-xBalance, -maxBalnceOffset, maxBalnceOffset) * swingAmaount;

        transform.localRotation = Quaternion.Euler(xTilt, 0f, zTilt);
    }

    void Initialize(GridCell[,] gridCells)
    {
        //calc center
        xMiddle = (gridCells.GetLength(0) - 1) / 2f;
        zMiddle = (gridCells.GetLength(1) - 1) / 2f;
    }
}
