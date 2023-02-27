using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [Header("Game")] public GameAttribute gameAttribute;
    [Header("Player")] public PlayerAttribute playerAttribute;
    [Header("Skill")] public SkillAttribute skillAttribute;
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
    private Vector3 _mousePosition;
    private GameObject[] _enemies;

    private void FixedUpdate()
    {
        if (gameAttribute.gameOver)
        {
            foreach (var shovel in _shovels)
            {
                Destroy(shovel);
            }

            return;
        }

        Check();
        Shovel();
        Fork();
        Pistol();
        Rifle();
        Shotgun();
        _enemies = null;
    }

    private void Check()
    {
        if (_shovels.Count != skillAttribute.shovel)
        {
            if (_shovels.Count == 0)
            {
                Arm(0);
            }

            AddShovel();
            return;
        }

        if (_fork != skillAttribute.fork)
        {
            if (_fork == 0)
            {
                Arm(1);
            }

            _fork += 1;
            return;
        }

        if (_hook != skillAttribute.hook)
        {
            if (_hook == 0)
            {
                Arm(2);
            }

            _hook += 1;
            return;
        }

        if (_pistol != skillAttribute.pistol)
        {
            if (_pistol == 0)
            {
                Arm(3);
            }

            _pistol += 1;
            return;
        }

        if (_rifle != skillAttribute.rifle)
        {
            if (_rifle == 0)
            {
                Arm(4);
            }

            _rifle += 1;
            return;
        }

        if (_shotgun != skillAttribute.shotgun)
        {
            if (_shotgun == 0)
            {
                Arm(5);
            }

            _shotgun += 1;
            return;
        }

        if (_armor != skillAttribute.armor)
        {
            playerAttribute.armor += 0.1f;
            _armor += 1;
            return;
        }

        if (_shoe != skillAttribute.shoe)
        {
            playerAttribute.moveSpeed += 0.5f;
            _shoe += 1;
            return;
        }

        if (_health != skillAttribute.maxHealth)
        {
            playerAttribute.health += 50;
            playerAttribute.maxHealth += 50;
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
        for (var i = 0; i < _shovels.Count; i++)
        {
            var angle = i * 360f / skillAttribute.shovel + _shovelTime * shovelSpeed * (skillAttribute.shovel + 10);
            var offset = Quaternion.Euler(0, 0, angle) * Vector3.right * shovelRadius;
            _shovels[i].transform.position = transform.position + offset;
            _shovels[i].transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    private void Fork()
    {
        if (skillAttribute.fork == 0) return;
        _forkTime += Time.deltaTime;
        if (_forkTime >= 3 - _fork * 0.2)
        {
            _forkTime = 0;
            var fork = Instantiate(Game.Instance.forkPrefab, transform.position, Quaternion.identity);
            fork.GetComponent<Fork>().direction = gameAttribute.direction;
        }
    }

    private void Pistol()
    {
        if (skillAttribute.pistol == 0) return;
        _pistolTime += Time.deltaTime;
        if (_pistolTime >= 2 - _pistol * 0.1 && EnemyClosed(6))
        {
            _pistolTime = 0;
            var pistolBullet = Instantiate(Game.Instance.pistolBulletPrefab, transform.position, Quaternion.identity);
            pistolBullet.GetComponent<FollowBullet>().direction = _direction;
        }
    }

    private void Rifle()
    {
        if (skillAttribute.rifle == 0) return;
        _rifleTime += Time.deltaTime;
        if (_rifleTime >= 0.1 && EnemyClosed(10))
        {
            _rifleTime = 0;
            var rifleBullet = Instantiate(Game.Instance.rifleBulletPrefab, transform.position, Quaternion.identity);
            rifleBullet.GetComponent<FollowBullet>().direction = _direction;
        }
    }

    private void Shotgun()
    {
        if (skillAttribute.shotgun == 0) return;
        _shotgunTime += Time.deltaTime;
        if (_shotgunTime >= 2 - _shotgun * 0.1 && EnemyClosed(4))
        {
            _shotgunTime = 0;
            var shotgunBullet = Instantiate(Game.Instance.shotgunBulletPrefab, transform.position, Quaternion.identity);
            shotgunBullet.GetComponent<FollowBullet>().direction = Quaternion.Euler(0f, 0f, 20f) * _direction;
            shotgunBullet = Instantiate(Game.Instance.shotgunBulletPrefab, transform.position, Quaternion.identity);
            shotgunBullet.GetComponent<FollowBullet>().direction = Quaternion.Euler(0f, 0f, 10f) * _direction;
            shotgunBullet = Instantiate(Game.Instance.shotgunBulletPrefab, transform.position, Quaternion.identity);
            shotgunBullet.GetComponent<FollowBullet>().direction = _direction;
            shotgunBullet = Instantiate(Game.Instance.shotgunBulletPrefab, transform.position, Quaternion.identity);
            shotgunBullet.GetComponent<FollowBullet>().direction = Quaternion.Euler(0f, 0f, -10f) * _direction;
            shotgunBullet = Instantiate(Game.Instance.shotgunBulletPrefab, transform.position, Quaternion.identity);
            shotgunBullet.GetComponent<FollowBullet>().direction = Quaternion.Euler(0f, 0f, -20f) * _direction;
        }
    }

    private bool EnemyClosed(float searchDistance)
    {
        if (_enemies == null)
        {
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        var rangedEnemies = new List<Vector3>();
        foreach (var enemy in _enemies)
        {
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < searchDistance)
            {
                rangedEnemies.Add(enemy.transform.position);
            }
        }

        if (rangedEnemies.Count == 0) return false;
        _direction = (rangedEnemies[Random.Range(0, rangedEnemies.Count)] - transform.position).normalized;
        return true;
    }
}