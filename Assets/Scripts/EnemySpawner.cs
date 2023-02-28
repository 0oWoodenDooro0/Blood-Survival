using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")] public EnemyAttribute enemyAttribute;
    [Header("Game")] public GameAttribute gameAttribute;
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
        if (gameAttribute.gameOver) return;
        if ((int)(gameAttribute.time / 0.3) == _currentSecond) return;
        _currentSecond = (int)(gameAttribute.time / 0.3);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var minute = (int)gameAttribute.time / 60;
        var playerPosition = Game.Instance.player.transform.position;
        var chunk = Random.Range(0, 4);
        var position = Vector3.zero;
        switch (chunk)
        {
            case 0:
                position.x = playerPosition.x + 18 + Random.Range(0f, 5f);
                position.y = playerPosition.y + Random.Range(-20f, 20f);
                break;
            case 1:
                position.x = playerPosition.x - 18 - Random.Range(0f, 5f);
                position.y = playerPosition.y + Random.Range(-15f, 15f);
                break;
            case 2:
                position.x = playerPosition.x + Random.Range(-23f, 23f);
                position.y = playerPosition.y + 10 + Random.Range(0f, 10f);
                break;
            case 3:
                position.x = playerPosition.x + Random.Range(-23f, 23f);
                position.y = playerPosition.y - 10 - Random.Range(0f, 10f);
                break;
        }

        var spawner = Instantiate(Game.Instance.enemy0Prefab, position, Quaternion.identity);
        var enemyScript = spawner.GetComponent<Enemy>();
        enemyScript.damage = enemyAttribute.damage + minute * enemyAttribute.damageIncreaseAmount;
        enemyScript.health = enemyAttribute.health + minute * enemyAttribute.healthIncreaseAmount;
        enemyScript.moveSpeed = enemyAttribute.moveSpeed + minute * enemyAttribute.moveSpeedIncreaseAmount;
    }
}