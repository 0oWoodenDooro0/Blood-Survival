using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text time;
    public Text killAmount;
    public Text level;
    public Slider experience;
    private int _minute;
    private int _second;

    private void Start()
    {
        Game.Instance.experience = 0;
        Game.Instance.levelMaxExperience = 2;
    }

    private void FixedUpdate()
    {
        _minute = (int)Game.Instance.time / 60;
        _second = (int)Game.Instance.time % 60;
        if (Game.Instance.experience >= Game.Instance.levelMaxExperience)
        {
            LevelUp();
        }
        time.text = _minute.ToString().PadLeft(2, '0') + ":" + _second.ToString().PadLeft(2, '0');
        killAmount.text = Game.Instance.killAmount.ToString().PadLeft(3, '0');
        level.text = "Lv." + Game.Instance.level;
        experience.value = (float)Game.Instance.experience / Game.Instance.levelMaxExperience;
    }

    private void LevelUp()
    {
        Game.Instance.experience -= Game.Instance.levelMaxExperience;
        Game.Instance.levelMaxExperience = 1 + (int)Mathf.Pow(Game.Instance.level, 2 - 0.03f * Game.Instance.level);
        Game.Instance.level += 1;
        Game.Instance.pause = true;
    }
}