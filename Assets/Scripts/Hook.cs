using UnityEngine;

public class Hook : MonoBehaviour
{
    [Header("Hook Attributes")] public float radius;
    public int damage;
    public Vector3 direction;
    private float _angle;
    private Vector3 _offset;
    private float _time;

    private void Start()
    {
        _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _offset = Quaternion.Euler(0, 0, _angle) * Vector3.right * radius;
        _angle += 40;
        transform.position += _offset;
        transform.rotation = Quaternion.Euler(0, 0, _angle - 90);
    }

    private void FixedUpdate()
    {
        transform.position = Game.Instance.playerScript.transform.position + _offset;
        if (_time < 0.2f)
        {
            _time += Time.deltaTime;
            var targetRotation = Quaternion.Euler(0f, 0f, -80) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime / 0.2f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}