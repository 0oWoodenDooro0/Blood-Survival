using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [Header("Game")] public GameAttribute gameAttribute;
    [Header("Skill")] public SkillAttribute skillAttribute;
    public Sprite[] sprites;
    [Header("Shovel Attributes")] public float shovelSpeed;
    public float shovelRadius;
    private List<GameObject> _shovels = new List<GameObject>();
    private float _shovelTime;
    private float _forkTime;
    private float _hookTime;
    private float _pistolTime;
    private float _rifleTime;
    private float _shotgunTime;
    private Vector3 _direction;
    private Vector3 _mousePosition;
    private GameObject[] _enemies;

    private void Start()
    {
        Arm(1);
    }

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

        Shovel();
        Hook();
        Fork();
        Pistol();
        Rifle();
        Shotgun();
        _enemies = null;
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

    private void Shovel()
    {
        if (skillAttribute.shovel == 0) return;
        if (_shovels.Count != skillAttribute.shovel)
        {
            var shovel = Instantiate(Game.Instance.shovelPrefab, transform.position, Quaternion.identity);
            shovel.transform.parent = transform;
            _shovels.Add(shovel);
        }

        _shovelTime += Time.deltaTime;
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
        if (_forkTime >= 3 - skillAttribute.fork * 0.2)
        {
            _forkTime = 0;
            var fork = Instantiate(Game.Instance.forkPrefab, transform.position, Quaternion.identity);
            fork.GetComponent<Fork>().direction = gameAttribute.direction;
        }
    }

    private void Hook()
    {
        if (skillAttribute.hook == 0) return;
        _hookTime += Time.deltaTime;
        if (_hookTime >= 2 && EnemyClosed(3))
        {
            _hookTime = 0;
            var hook = Instantiate(Game.Instance.hookPrefab, transform.position, Quaternion.identity);
            hook.GetComponent<Hook>().direction = _direction;
        }
    }

    private void Pistol()
    {
        if (skillAttribute.pistol == 0) return;
        _pistolTime += Time.deltaTime;
        if (_pistolTime >= 2 - skillAttribute.pistol * 0.1 && EnemyClosed(6))
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
        if (_shotgunTime >= 2 - skillAttribute.shotgun * 0.1 && EnemyClosed(4))
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