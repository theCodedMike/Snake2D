using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnFood : MonoBehaviour
{
    [Header("食物")]
    public GameObject foodPrefab;
    [Header("左边界")]
    public Transform borderLeft;
    [Header("右边界")]
    public Transform borderRight;
    [Header("上边界")]
    public Transform borderTop;
    [Header("下边界")]
    public Transform borderBottom;


    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        int x = Random.Range((int)borderLeft.position.x + 1, (int)borderRight.position.x - 1);
        int y = Random.Range((int)borderBottom.position.y + 1, (int)borderTop.position.y - 1);
        //print($"Food position: {y}, {x}");
        GameObject foodObj = Instantiate(foodPrefab, transform, true);
        foodObj.transform.position = new Vector3(x, y, 0);
    }
    
}
