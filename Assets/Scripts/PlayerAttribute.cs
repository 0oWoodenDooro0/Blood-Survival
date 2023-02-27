using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttribute", menuName = "ScriptableObject/PlayerAttribute")]
public class PlayerAttribute : ScriptableObject
{
    public float moveSpeed;
    public float health;
    public float maxHealth;
    public float armor;

    public void Reset()
    {
        moveSpeed = 2;
        health = 50;
        maxHealth = 50;
        armor = 0;
    }
}