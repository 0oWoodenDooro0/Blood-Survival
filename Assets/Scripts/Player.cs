using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 inputVector;
    public SpriteRenderer leftArm;
    public SpriteRenderer rightArm;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private string _currentState;

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Game.Instance.playerHealth -= (col.gameObject.GetComponent<Enemy>().damage * (1 - Game.Instance.playerArmor) * Time.deltaTime);
        }
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (Game.Instance.gameOver)
        {
            _rigidbody2D.velocity = Vector3.zero;
            ChangeAnimation("Dead");
            leftArm.sprite = null;
            rightArm.sprite = null;
        }
        else
        {
            Movement();
        }
    }

    private void Movement()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");

        var moveVector = inputVector.normalized * Game.Instance.playerMoveSpeed;

        if (inputVector.x != 0 || inputVector.y != 0)
        {
            ChangeAnimation("Run");
            _spriteRenderer.flipX = inputVector.x switch
            {
                > 0 => false,
                < 0 => true,
                _ => _spriteRenderer.flipX
            };
            leftArm.flipX = inputVector.x switch
            {
                > 0 => false,
                < 0 => true,
                _ => leftArm.flipX
            };
            rightArm.flipX = inputVector.x switch
            {
                > 0 => false,
                < 0 => true,
                _ => rightArm.flipX
            };
        }
        else
        {
            ChangeAnimation("Stand");
        }

        _rigidbody2D.velocity = moveVector;
    }

    private void ChangeAnimation(string nextState)
    {
        if (_currentState == nextState) return;
        _animator.Play(nextState);
        _currentState = nextState;
    }
}