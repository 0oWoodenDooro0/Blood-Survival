using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public Sprite[] sprites;
    [Header("Shovel Attributes")] public float shovelSpeed;
    public float shovelRadius;
    private List<GameObject> _shovels = new List<GameObject>();
    private float _shovelTime;
    private int _fork;
    private float _forkTime;
    private int _hook;
    private int _pistol;
    private float _pistolTime;
    private int _rifle;
    private float _rifleTime;
    private int _shotgun;
    private float _shotgunTime;
    private int _armor;
    private int _shoe;
    private int _health;
    private int _damage;
    private Vector3 _direction;

    private void FixedUpdate()
    {
        Check();
        Shovel();
        Fork();
        Pistol();
        Rifle();
        Shotgun();
    }

    private void Check()
    {
        if (_shovels.Count != Game.Instance.shovel)
        {
            if (_shovels.Count == 0)
            {
                Arm(0);
            }

            AddShovel();
            return;
        }

        if (_fork != Game.Instance.fork)
        {
            if (_fork == 0)
            {
                Arm(1);
            }

            _fork += 1;
            return;
        }

        if (_hook != Game.Instance.hook)
        {
            if (_hook == 0)
            {
                Arm(2);
            }

            _hook += 1;
            return;
        }

        if (_pistol != Game.Instance.pistol)
        {
            if (_pistol == 0)
            {
                Arm(3);
            }

            _pistol += 1;
            return;
        }

        if (_rifle != Game.Instance.rifle)
        {
            if (_rifle == 0)
            {
                Arm(4);
            }

            _rifle += 1;
            return;
        }

        if (_shotgun != Game.Instance.shotgun)
        {
            if (_shotgun == 0)
            {
                Arm(5);
            }

            _shotgun += 1;
            return;
        }

        if (_armor != Game.Instance.armor)
        {
            Game.Instance.playerArmor += 0.1f;
            _armor += 1;
            return;
        }

        if (_shoe != Game.Instance.shoe)
        {
            Game.Instance.playerMoveSpeed += 0.5f;
            _shoe += 1;
            return;
        }

        if (_health != Game.Instance.maxHp)
        {
            Game.Instance.playerHealth += 50;
            Game.Instance.playerMaxHealth += 50;
            _health += 1;
            return;
        }
    }

    private void Arm(int index)
    {
        if (Game.Instance.playerScript.leftArm.sprite && Game.Instance.playerScript.rightArm.sprite) return;

        if (!Game.Instance.playerScript.leftArm.sprite)
        {
            Game.Instance.playerScript.leftArm.sprite = sprites[index];
            return;
        }

        if (!Game.Instance.playerScript.rightArm.sprite)
        {
            Game.Instance.playerScript.rightArm.sprite = sprites[index];
            return;
        }
    }

    private void AddShovel()
    {
        var shovel = Instantiate(Game.Instance.shovelPrefab, transform.position, Quaternion.identity);
        shovel.transform.parent = transform;
        _shovels.Add(shovel);
    }

    private void Shovel()
    {
        _shovelTime += Time.deltaTime;
        if (_shovels.Count == 0) return;
        for (var i = 0; i < Game.Instance.shovel; i++)
        {
            var angle = i * 360f / Game.Instance.shovel + _shovelTime * shovelSpeed * (Game.Instance.shovel + 1);
            var offset = Quaternion.Euler(0, 0, angle) * Vector3.right * shovelRadius;
            _shovels[i].transform.position = transform.position + offset;
            _shovels[i].transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    private void Fork()
    {
        if (Game.Instance.fork == 0) return;
        _forkTime += Time.deltaTime;
        if (_forkTime >= 3 && EnemyClosed(4))
        {
            _forkTime = 0;
            var fork = Instantiate(Game.Instance.forkPrefab, transform.position, Quaternion.identity);
            fork.GetComponent<Fork>().direction = _direction;
        }
    }

    private void Pistol()
    {
        if (Game.Instance.pistol == 0) return;
        _pistolTime += Time.deltaTime;
        if (_pistolTime >= 2 && EnemyClosed(6))
        {
            _pistolTime = 0;
            var pistolBullet = Instantiate(Game.Instance.pistolBulletPrefab, transform.position, Quaternion.identity);
            pistolBullet.GetComponent<FollowBullet>().direction = _direction;
        }
    }

    private void Rifle()
    {
        if (Game.Instance.rifle == 0) return;
        _rifleTime += Time.deltaTime;
        if (_rifleTime >= 0.1 && EnemyClosed(6))
        {
            _rifleTime = 0;
            var rifleBullet = Instantiate(Game.Instance.rifleBulletPrefab, transform.position, Quaternion.identity);
            rifleBullet.GetComponent<FollowBullet>().direction = _direction;
        }
    }

    private void Shotgun()
    {
        if (Game.Instance.shotgun == 0) return;
        _shotgunTime += Time.deltaTime;
        if (_shotgunTime >= 2 && EnemyClosed(6))
        {
            _shotgunTime = 0;
            var shotgunBullet = Instantiate(Game.Instance.shotgunBulletPrefab, transform.position, Quaternion.identity);
            shotgunBullet.GetComponent<FollowBullet>().direction = Quaternion.Euler(0f, 0f, 40f) * _direction;
            shotgunBullet = Instantiate(Game.Instance.shotgunBulletPrefab, transform.position, Quaternion.identity);
            shotgunBullet.GetComponent<FollowBullet>().direction = Quaternion.Euler(0f, 0f, 20f) * _direction;
            shotgunBullet = Instantiate(Game.Instance.shotgunBulletPrefab, transform.position, Quaternion.identity);
            shotgunBullet.GetComponent<FollowBullet>().direction = _direction;
            shotgunBullet = Instantiate(Game.Instance.shotgunBulletPrefab, transform.position, Quaternion.identity);
            shotgunBullet.GetComponent<FollowBullet>().direction = Quaternion.Euler(0f, 0f, -20f) * _direction;
            shotgunBullet = Instantiate(Game.Instance.shotgunBulletPrefab, transform.position, Quaternion.identity);
            shotgunBullet.GetComponent<FollowBullet>().direction = Quaternion.Euler(0f, 0f, -40f) * _direction;
        }
    }

    private bool EnemyClosed(float searchDistance)
    {
        var closestDistance = Mathf.Infinity;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(Game.Instance.player.transform.position, enemy.transform.position);
            if (distance < closestDistance && distance < searchDistance)
            {
                closestDistance = distance;
                _direction = (enemy.transform.position - transform.position).normalized;
            }
        }

        return !float.IsPositiveInfinity(closestDistance);
    }
}