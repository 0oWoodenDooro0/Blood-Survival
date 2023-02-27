using UnityEngine;

[CreateAssetMenu(fileName = "SkillAttribute", menuName = "ScriptableObject/SkillAttribute")]
public class SkillAttribute : ScriptableObject
{
    public int shovel;
    public int fork;
    public int hook;
    public int pistol;
    public int rifle;
    public int shotgun;
    public int armor;
    public int shoe;
    public int maxHealth;
    public int damage;

    public void Reset()
    {
        shovel = 0;
        fork = 1;
        hook = 0;
        pistol = 0;
        rifle = 0;
        shotgun = 0;
        armor = 0;
        shoe = 0;
        maxHealth = 0;
        damage = 0;
    }
}