using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private GameObject[] _experiences;
    private Vector3 _playerPosition;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            CollectAllExperiences();
        }
    }

    private void Update()
    {
        if (_experiences == null) return;
        if (_experiences != null && _experiences.All(experience => experience == null))
        {
            Destroy(gameObject);
        }
        _playerPosition = Game.Instance.playerScript.transform.position;
        foreach (var experience in _experiences)
        {
            if (experience == null) break;
            var distance = Vector3.Distance(_playerPosition, experience.transform.position);
            if (distance < 0.5)
            {
                Game.Instance.gameAttribute.experience += 1;
                Destroy(gameObject);
            }

            var direction = (_playerPosition - experience.transform.position).normalized;
            experience.GetComponent<ExperienceBall>().rigidbody2D.velocity = direction * 20;
        }
    }

    private void CollectAllExperiences()
    {
        _experiences = GameObject.FindGameObjectsWithTag("Experience");
    }
}