using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    public Camera mainCamera;
    [Header("Game")] public static Game Instance;
    public bool gameOver;
    public bool pause;
    public GameObject levelUpCanvas;
    public Player playerScript;
    public GameObject player;
    [Header("Player")] public float playerMoveSpeed;
    public float playerHealth;
    public int playerMaxHealth;
    public float playerArmor;
    [Header("Prefab")] public GameObject playerPrefab;
    public GameObject enemy0Prefab;
    public GameObject experiencePrefab;
    public GameObject shovelPrefab;
    public GameObject forkPrefab;
    public GameObject pistolBulletPrefab;
    public GameObject rifleBulletPrefab;
    public GameObject shotgunBulletPrefab;
    [Header("UI")] public float time;
    public int killAmount;
    public int experience;
    public int levelMaxExperience;
    public int level;
    public int shovel;
    public int fork;
    public int hook;
    public int pistol;
    public int rifle;
    public int shotgun;
    public int armor;
    public int shoe;
    public int maxHp;
    public int damage;

    private void Awake()
    {
        Instance = this;
        gameOver = false;
        pause = false;
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerScript = player.GetComponent<Player>();
        playerMoveSpeed = 2f;
        playerHealth = 100;
        playerMaxHealth = 100;
        playerArmor = 0f;
    }

    private void FixedUpdate()
    {
        if (gameOver) return;
        if (playerHealth <= 0)
        {
            gameOver = true;
            return;
        }

        time += Time.deltaTime;
        switch (pause)
        {
            case true when !levelUpCanvas.activeSelf:
                Pause();
                break;
            case false when levelUpCanvas.activeSelf:
                Resume();
                break;
        }
    }

    private void Resume()
    {
        levelUpCanvas.SetActive(false);
    }

    private void Pause()
    {
        levelUpCanvas.SetActive(true);
        levelUpCanvas.GetComponent<LevelUp>().RandomSelection();
    }
}