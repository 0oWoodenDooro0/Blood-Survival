using UnityEngine;

public class FollowBullet : MonoBehaviour
{
    public float moveSpeed;
    public int damage;
    public  Vector2 direction;
    private Rigidbody2D _rigidbody2D;
    private Vector3 _enemyPosition;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        Destroy(gameObject, 3f);
    }

    private void FixedUpdate()
    {
        if (Game.Instance.gameAttribute.gameOver)
        {
            Destroy(gameObject);
        }
        _rigidbody2D.velocity = direction * moveSpeed;
    }
}