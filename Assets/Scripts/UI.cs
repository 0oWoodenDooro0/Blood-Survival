using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("Player")] public PlayerAttribute playerAttribute;
    [Header("Game")] public GameAttribute gameAttribute;
    public Text time;
    public Text killAmount;
    public Text level;
    public Slider experience;
    public Slider playerHealth;
    private int _minute;
    private int _second;

    private void Start()
    {
        gameAttribute.experience = 0;
        gameAttribute.levelMaxExperience = 10;
    }

    private void FixedUpdate()
    {
        _minute = (int)gameAttribute.time / 60;
        _second = (int)gameAttribute.time % 60;
        if (gameAttribute.experience >= gameAttribute.levelMaxExperience)
        {
            LevelUp();
        }

        time.text = _minute.ToString().PadLeft(2, '0') + ":" + _second.ToString().PadLeft(2, '0');
        killAmount.text = gameAttribute.killAmount.ToString().PadLeft(3, '0');
        level.text = "Lv." + gameAttribute.level;
        experience.value = (float)gameAttribute.experience / gameAttribute.levelMaxExperience;
        playerHealth.value = playerAttribute.health / playerAttribute.maxHealth;
    }

    private void LevelUp()
    {
        gameAttribute.experience -= gameAttribute.levelMaxExperience;
        gameAttribute.levelMaxExperience = 20 + (int)Mathf.Pow(gameAttribute.level, 2 - 0.01f * gameAttribute.level);
        gameAttribute.level += 1;
        Pause();
    }

    private void Pause()
    {
        gameAttribute.pause = true;
        Game.Instance.levelUp.SetActive(true);
        Game.Instance.levelUp.GetComponent<LevelUp>().RandomSelection();
    }
}