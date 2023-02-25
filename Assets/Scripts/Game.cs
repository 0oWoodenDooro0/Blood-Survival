using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    public Camera mainCamera;
    [Header("Game")] public static Game Instance;
    public bool gameOver;
    public GameObject levelUp;
    public bool directionAttack;
    public bool isController;
    public Vector3 mousePosition;
    public Vector3 direction;
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
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerScript = player.GetComponent<Player>();
        directionAttack = false;
        playerMoveSpeed = 2f;
        playerHealth = 50;
        playerMaxHealth = 50;
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
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            directionAttack = !directionAttack;
        }
    }
}