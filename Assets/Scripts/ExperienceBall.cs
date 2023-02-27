using UnityEngine;

public class ExperienceBall : MonoBehaviour
{
    public new Rigidbody2D rigidbody2D;

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
            rigidbody2D.velocity = direction * (5 - distance);
        }
    }
}