using UnityEngine;

public class Game : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject levelUp;
    public GameObject player;
    public Player playerScript;
    public static Game Instance;
    [Header("Game")] public GameAttribute gameAttribute;
    [Header("Player")] public PlayerAttribute playerAttribute;
    [Header("Skill")] public SkillAttribute skillAttribute;
    [Header("Prefab")] public GameObject playerPrefab;
    public GameObject enemy0Prefab;
    public GameObject experiencePrefab;
    public GameObject shovelPrefab;
    public GameObject hookPrefab;
    public GameObject forkPrefab;
    public GameObject pistolBulletPrefab;
    public GameObject rifleBulletPrefab;
    public GameObject shotgunBulletPrefab;
    public GameObject bloodBagPrefab;
    public GameObject magnetPrefab;
    private int _killAmount;

    private void Awake()
    {
        Instance = this;
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerScript = player.GetComponent<Player>();
        playerAttribute.Reset();
        gameAttribute.Reset();
        skillAttribute.Reset();
    }

    private void FixedUpdate()
    {
        if (gameAttribute.gameOver) return;

        if (playerAttribute.health <= 0)
        {
            gameAttribute.gameOver = true;
            return;
        }

        if (_killAmount != gameAttribute.killAmount)
        {
            _killAmount += 1;
            if (_killAmount % 40 == 0)
            {
                SpawnObject(bloodBagPrefab);
            }

            if (_killAmount % 100 == 0)
            {
                SpawnObject(magnetPrefab);
            }
        }

        gameAttribute.time += Time.deltaTime;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gameAttribute.directionAttack = !gameAttribute.directionAttack;
        }
    }

    private void SpawnObject(GameObject prefab)
    {
        var playerPosition = Game.Instance.player.transform.position;
        var chunk = Random.Range(0, 4);
        var position = Vector3.zero;
        switch (chunk)
        {
            case 0:
                position.x = playerPosition.x + 18 + Random.Range(0f, 10f);
                position.y = playerPosition.y + 10 + Random.Range(0f, 10f);
                break;
            case 1:
                position.x = playerPosition.x - 18 - Random.Range(0f, 10f);
                position.y = playerPosition.y + 10 + Random.Range(0f, 10f);
                break;
            case 2:
                position.x = playerPosition.x - 18 - Random.Range(0f, 10f);
                position.y = playerPosition.y - 10 - Random.Range(0f, 10f);
                break;
            case 3:
                position.x = playerPosition.x + 18 + Random.Range(0f, 10f);
                position.y = playerPosition.y - 10 - Random.Range(0f, 10f);
                break;
        }
        Instantiate(prefab, position, Quaternion.identity);
    }
}