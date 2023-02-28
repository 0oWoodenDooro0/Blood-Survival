using UnityEngine;

public class ExperienceBall : MonoBehaviour
{
    public bool magnet;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Experience Collector"))
        {
            var distance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (distance < 0.5)
            {
                Game.Instance.gameAttribute.experience += 1;
                Destroy(gameObject);
            }

            var direction = (other.gameObject.transform.position - transform.position).normalized;
            _rigidbody2D.velocity = direction * (5 - distance);
        }
    }

    private void FixedUpdate()
    {
        if (magnet)
        {
            var playerPosition = Game.Instance.playerScript.transform.position;
            var distance = Vector3.Distance(playerPosition, transform.position);
            if (distance < 0.5)
            {
                Game.Instance.gameAttribute.experience += 1;
                Destroy(gameObject);
            }

            var direction = (playerPosition - transform.position).normalized;
            _rigidbody2D.velocity = direction * 20;
        }
    }
}