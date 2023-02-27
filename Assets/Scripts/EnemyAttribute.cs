using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttribute", menuName = "ScriptableObject/EnemyAttribute")]
public class EnemyAttribute : ScriptableObject
{
    public float moveSpeed;
    public float health;
    public float damage;
    public float moveSpeedIncreaseAmount;
    public float healthIncreaseAmount;
    public float damageIncreaseAmount;
}