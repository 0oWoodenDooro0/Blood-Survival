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
    public GameObject forkPrefab;
    public GameObject pistolBulletPrefab;
    public GameObject rifleBulletPrefab;
    public GameObject shotgunBulletPrefab;

    private void Awake()
    {
        Instance = this;
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerScript = player.GetComponent<Player>();
        playerAttribute.Reset();
        gameAttribute.Reset();
    }

    private void FixedUpdate()
    {
        if (gameAttribute.gameOver) return;

        if (playerAttribute.health <= 0)
        {
            gameAttribute.gameOver = true;
            return;
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
}