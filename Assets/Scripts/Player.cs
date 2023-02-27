using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Game")] public GameAttribute gameAttribute;
    [Header("Player")] public PlayerAttribute playerAttribute;
    public Vector3 inputVector;
    public SpriteRenderer leftArm;
    public SpriteRenderer rightArm;
    public GameObject arrow;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerControls _playerControls;
    private string _currentState;

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            playerAttribute.health -= (col.gameObject.GetComponent<Enemy>().damage * (1 - playerAttribute.armor) * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerControls = new PlayerControls();
    }

    private void Update()
    {
        if (gameAttribute.gameOver)
        {
            _rigidbody2D.velocity = Vector3.zero;
            ChangeAnimation("Dead");
            leftArm.sprite = null;
            rightArm.sprite = null;
            return;
        }

        if (gameAttribute.pause) return;
        Direction();
        Movement();
    }

    private void Movement()
    {
        inputVector = _playerControls.Controls.Movement.ReadValue<Vector2>().normalized;

        var moveVector = inputVector * playerAttribute.moveSpeed;

        if (inputVector.x != 0 || inputVector.y != 0)
        {
            ChangeAnimation("Run");
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

    public void OnDeviceChange(PlayerInput playerInput)
    {
        gameAttribute.isController = playerInput.currentControlScheme.Equals("Controller") ? true : false;
    }

    private void Direction()
    {
        var aim = _playerControls.Controls.Aim.ReadValue<Vector2>();
        if (gameAttribute.directionAttack)
        {
            switch (gameAttribute.isController)
            {
                case true when aim != Vector2.zero:
                    gameAttribute.direction = aim.normalized;
                    break;
                case true:
                    gameAttribute.direction = inputVector;
                    break;
                default:
                    var mousePosition = Game.Instance.mainCamera.ScreenToWorldPoint(aim);
                    mousePosition.z = 0f;
                    gameAttribute.direction = (mousePosition - transform.position).normalized;
                    break;
            }
        }
        else
        {
            if (gameAttribute.isController && aim != Vector2.zero)
            {
                gameAttribute.direction = aim.normalized;
            }
            else if (inputVector != Vector3.zero)
            {
                gameAttribute.direction = inputVector;
            }
        }

        if (gameAttribute.direction == Vector3.zero) return;
        _spriteRenderer.flipX = gameAttribute.direction.x switch
        {
            > 0 => false,
            < 0 => true,
            _ => _spriteRenderer.flipX
        };
        leftArm.flipX = gameAttribute.direction.x switch
        {
            > 0 => false,
            < 0 => true,
            _ => leftArm.flipX
        };
        rightArm.flipX = gameAttribute.direction.x switch
        {
            > 0 => false,
            < 0 => true,
            _ => rightArm.flipX
        };
        var angle = Mathf.Atan2(gameAttribute.direction.y, gameAttribute.direction.x) * Mathf.Rad2Deg;
        var offset = Quaternion.Euler(0, 0, angle) * Vector3.right * 1.5f;
        arrow.transform.position = transform.position + offset;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}