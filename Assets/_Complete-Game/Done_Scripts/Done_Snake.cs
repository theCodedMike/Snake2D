using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Done_Snake : MonoBehaviour
{

    Vector2 dir = Vector2.right;//蛇的移动方向
    bool ate = false;            //是否吃到食物

    public GameObject tailPrefab;      //表示蛇身体的预制体

    List<Transform> tail = new List<Transform>();//存放蛇身和蛇头的列表

    int score;                          //分数
    public Text scoreText;              //显示分数的UI
    public Text gameOverText;           //游戏结束的UI

    private Vector2 lastDir;

    float lastTime = 0.0f;

    int flag = 1;//标记方向，1,向右，2，向左，3，向上，4，向下

    // Use this for initialization
    void Start()
    {       
        //循环调用Move函数，是贪吃蛇能自动移动
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        //向下并且上一次不向上
        if (Input.GetKeyDown(KeyCode.DownArrow)&&flag!=3)
        {
            dir = Vector2.down;
            flag = 4;
        }
        //向上并且上一次不向下
        if (Input.GetKeyDown(KeyCode.UpArrow)&&flag!=4)
        {
            dir = -Vector2.down;
            flag = 3;
        }
        //向左并且上一次方向不向右
        if (Input.GetKeyDown(KeyCode.LeftArrow)&&flag!=1)
        {
            dir = -Vector2.right;
            flag = 2;
        }
        //向右并且上一次方向不向左
        if (Input.GetKeyDown(KeyCode.RightArrow)&&flag!=2)
        {
            dir = Vector2.right;
            flag = 1;
        }

    }

    void Move()
    {

       // lastDir = dir;
        Vector2 v = transform.position;
        
        //移动
        transform.Translate(dir);

      
        //碰到食物，在蛇头移动前的位置插入一个元素
        if (ate)
        {
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            tail.Insert(0, g.transform);

            ate = false;
        }
        //没有碰到食物的移动，移动蛇头，在蛇头移动前的位置插入一个元素，删除最后一个元素
        else if (tail.Count > 0)
        {
            tail[tail.Count - 1].position = v;

            tail.Insert(0, tail[tail.Count - 1]);

            tail.RemoveAt(tail.Count - 1);

        }
        
        lastTime = Time.time;
        lastDir = dir;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //碰到食物
        if (collision.name.StartsWith("Done_FoodPrefab"))
        {
            ate = true;

            //分数+1
            score++;
            //显示分数
            scoreText.text = "分数：" + score;

            //生成下一个食物
            FindObjectOfType<Done_SpawnFood>().Spawn();
            //销毁当前食物
            Destroy(collision.gameObject);
        }
        //碰到边界
        if (collision.name.StartsWith("border"))
        {
            Debug.Log("撞到边界，游戏结束！");
            gameOverText.text = "Game Over!";
            //停止游戏
            dir = Vector2.zero;
            Time.timeScale = 0;
        }
        //碰到自身
        if (collision.name.StartsWith("Done_TailPrefab"))
        {
            Debug.Log("撞到自身,游戏结束！");
            gameOverText.text = "Game Over!";
            //停止游戏
            Time.timeScale = 0;
        }

    }
}
