using UnityEngine;

public class BloodBag : MonoBehaviour
{
    [Header("Player")] public PlayerAttribute playerAttribute;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerAttribute.health += playerAttribute.maxHealth / 4;
            if (playerAttribute.maxHealth > playerAttribute.health)
            {
                playerAttribute.health = playerAttribute.maxHealth;
            }
            Destroy(gameObject);
        }
    }
}