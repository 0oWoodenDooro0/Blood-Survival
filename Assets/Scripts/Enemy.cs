using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Skill")] public SkillAttribute skillAttribute;
    [Header("Enemy Attributes")] public float moveSpeed;
    public float health;
    public float damage;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Shovel":
                health -= (col.gameObject.GetComponent<Shovel>().damage + (skillAttribute.shovel - 1) * 5) * (1 + .1f * skillAttribute.damage);
                StartCoroutine(Hurt());
                break;
            case "Fork":
                health -= (col.gameObject.GetComponent<Fork>().damage + (skillAttribute.fork - 1) * 10) * (1 + .1f * skillAttribute.damage);
                StartCoroutine(Hurt());
                break;
            case "Pistol Bullet":
                health -= (col.gameObject.GetComponent<FollowBullet>().damage + (skillAttribute.pistol - 1) * 10) * (1 + .1f * skillAttribute.damage);
                StartCoroutine(Hurt());
                Destroy(col.gameObject);
                break;
            case "Rifle Bullet":
                health -= (col.gameObject.GetComponent<FollowBullet>().damage + (skillAttribute.rifle - 1) * 2) * (1 + .1f * skillAttribute.damage);
                StartCoroutine(Hurt());
                Destroy(col.gameObject);
                break;
            case "Shotgun Bullet":
                health -= (col.gameObject.GetComponent<FollowBullet>().damage + (skillAttribute.shotgun - 1) * 5) * (1 + .1f * skillAttribute.damage);
                StartCoroutine(Hurt());
                Destroy(col.gameObject);
                break;
        }
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckHealth();
        if (!Game.Instance.gameAttribute.gameOver)
        {
            FollowPlayer();
        }
        else
        {
            _rigidbody2D.velocity = Vector3.zero;
        }
    }

    private void FollowPlayer()
    {
        var moveDirection = (Game.Instance.player.transform.position - transform.position).normalized;
        _rigidbody2D.velocity = moveDirection * moveSpeed;
        if (moveDirection.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private IEnumerator Hurt()
    {
        _animator.Play("Hurt");
        yield return new WaitForSeconds(0.1f);
        _animator.Play("Run");
    }

    private void CheckHealth()
    {
        if (health > 0) return;
        Game.Instance.gameAttribute.killAmount += 1;
        Instantiate(Game.Instance.experiencePrefab, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}