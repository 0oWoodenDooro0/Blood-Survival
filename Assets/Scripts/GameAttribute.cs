using UnityEngine;

[CreateAssetMenu(fileName = "GameAttribute", menuName = "ScriptableObject/GameAttribute")]
public class GameAttribute : ScriptableObject
{
    public bool gameOver;
    public bool pause;
    public float time;
    public int killAmount;
    public int experience;
    public int levelMaxExperience;
    public int level;
    public bool directionAttack;
    public bool isController;
    public Vector3 direction;

    public void Reset()
    {
        gameOver = false;
        pause = false;
        time = 0f;
        killAmount = 0;
        experience = 0;
        levelMaxExperience = 0;
        level = 0;
        directionAttack = false;
        direction = new Vector3(0, 1, 0);
    }
}