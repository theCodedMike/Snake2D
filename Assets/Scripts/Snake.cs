using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left, Right, Up, Down
}
public class Snake : MonoBehaviour
{
    [Header("蛇尾")]
    public GameObject tailPrefab;
    
    private readonly float _moveFreq = 0.3f; // 每隔0.3秒移动一次
    private Vector3 _moveDir = Vector3.right; // 蛇头移动朝向
    private Direction _pressDir; // 用户按下的按键朝向
    private readonly List<Transform> _body = new(); // 蛇身

    private SpawnFood _spawnFood;
    private bool _ateFood;
    
    private void Start()
    {
        _spawnFood = FindFirstObjectByType<SpawnFood>();
        InvokeRepeating(nameof(Move), 0.3f, _moveFreq);
    }

    private void Move()
    {
        Vector3 headPosition = transform.position;
        transform.Translate(_moveDir);

        if (_ateFood)
        {
            GameObject tailObj = Instantiate(tailPrefab, headPosition, Quaternion.identity);
            _body.Insert(0, tailObj.transform);
            _ateFood = false;
        } else if (_body.Count > 0)
        {
            _body[^1].position = headPosition;
            _body.Insert(0, _body[^1]);
            _body.RemoveAt(_body.Count - 1);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && _pressDir != Direction.Down)
        {
            _moveDir = Vector3.up;
            _pressDir = Direction.Up;
        } else if (Input.GetKeyDown(KeyCode.DownArrow) && _pressDir != Direction.Up)
        {
            _moveDir = Vector3.down;
            _pressDir = Direction.Down;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && _pressDir != Direction.Right)
        {
            _moveDir = Vector3.left;
            _pressDir = Direction.Left;
        }else if (Input.GetKeyDown(KeyCode.RightArrow) && _pressDir != Direction.Left)
        {
            _moveDir = Vector3.right;
            _pressDir = Direction.Right;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            _ateFood = true;
            _spawnFood.Spawn();
            Destroy(other.gameObject);
        }
    }
}
