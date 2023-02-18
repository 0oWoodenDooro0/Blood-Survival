using System;
using UnityEngine;

public class Fork : MonoBehaviour
{
    [Header("Fork Attributes")] public float moveSpeed;
    public float searchDistance;
    public int damage;
    private Rigidbody2D _rigidbody2D;
    public Vector3 direction;
    private Vector3 _enemyPosition;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        Destroy(gameObject, 3);
    }

    private void FixedUpdate()
    {
        if (Game.Instance.gameOver)
        {
            Destroy(gameObject);
        }
        _rigidbody2D.velocity = direction * moveSpeed;
    }
}