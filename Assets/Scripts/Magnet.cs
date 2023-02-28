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

    private void CollectAllExperiences()
    {
        _experiences = GameObject.FindGameObjectsWithTag("Experience");
        foreach (var experience in _experiences)
        {
            experience.GetComponent<ExperienceBall>().magnet = true;
        }
        Destroy(gameObject);
    }
}