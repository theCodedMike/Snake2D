using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_SpawnFood : MonoBehaviour {

    public GameObject foodPrefab; //食物预制体

    public Transform borderLeft;  //左边界
    public Transform borderRight; //右边界
    public Transform borderTop;   //上边界
    public Transform borderBottom;//下边界


    // Use this for initialization
    void Start()
    {
        //调用生成食物的函数
        Spawn();
    }

    public void Spawn()
    {
        //在上下边界范围内生成随机数
        int y = Random.Range((int)borderBottom.position.y + 1, (int)borderTop.position.y - 1);
        //在左右边界范围内生成随机数
        int x = Random.Range((int)borderLeft.position.x + 1, (int)borderRight.position.x - 1);
        //实例化食物
        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
    }

}
