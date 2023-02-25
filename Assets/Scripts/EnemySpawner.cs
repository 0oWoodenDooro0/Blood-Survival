using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Base Attributes")] public float baseMoveSpeed;
    public int baseHealth;
    public int baseDamage;
    [Header("Enemy Increase Amount")] public float moveSpeedIncreaseAmount;
    public int healthIncreaseAmount;
    public int damageIncreaseAmount;
    private int _currentSecond;

    private void Start()
    {
        for (var i = 0; i < 20; i++)
        {
            SpawnEnemy();
        }
    }

    private void FixedUpdate()
    {
        if (Game.Instance.gameOver) return;
        if ((int)(Game.Instance.time / 0.3) == _currentSecond) return;
        _currentSecond = (int)(Game.Instance.time / 0.3);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var minute = (int)Game.Instance.time / 60;
        var playerPosition = Game.Instance.player.transform.position;
        var chunk = Random.Range(0, 4);
        var position = Vector3.zero;
        switch (chunk)
        {
            case 0:
                position.x = playerPosition.x + 10 + Random.Range(0f, 40f);
                position.y = playerPosition.y + 10 + Random.Range(0f, 40f);
                break;
            case 1:
                position.x = playerPosition.x - 10 - Random.Range(0f, 40f);
                position.y = playerPosition.y + 10 + Random.Range(0f, 40f);
                break;
            case 2:
                position.x = playerPosition.x - 10 - Random.Range(0f, 40f);
                position.y = playerPosition.y - 10 - Random.Range(0f, 40f);
                break;
            case 3:
                position.x = playerPosition.x + 10 + Random.Range(0f, 40f);
                position.y = playerPosition.y - 10 - Random.Range(0f, 40f);
                break;
        }
        var spawner = Instantiate(Game.Instance.enemy0Prefab, position, quaternion.identity);
        var enemyScript = spawner.GetComponent<Enemy>();
        enemyScript.damage = baseDamage + minute * damageIncreaseAmount;
        enemyScript.health = baseHealth + minute * healthIncreaseAmount;
        enemyScript.moveSpeed = baseMoveSpeed + minute * moveSpeedIncreaseAmount;
    }
}