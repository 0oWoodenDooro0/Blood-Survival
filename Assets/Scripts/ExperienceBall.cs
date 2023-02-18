using System;
using UnityEngine;

public class ExperienceBall : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Experience Collector"))
        {
            var distance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (distance < 0.5)
            {
                Game.Instance.experience += 1;
                Destroy(gameObject);
            }
            var direction = (other.gameObject.transform.position - transform.position).normalized;
            _rigidbody2D.velocity = direction * (3 - distance);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _rigidbody2D.velocity = Vector3.zero;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
}