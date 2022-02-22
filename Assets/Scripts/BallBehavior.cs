using System;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] float highness;
    [SerializeField] Vector3 playerOffSet;
    
    Rigidbody _rigidbody;
    PlayerController _playerWithBall;

    Transform _thisTransform;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _thisTransform = transform;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerWithBall = other.gameObject.GetComponent<PlayerController>();
        }
    }

    public void Shoot(float power, Vector3 target)
    {
        _playerWithBall = null;
        _thisTransform.parent = null;

        
        
        _rigidbody.velocity = CalculateVelocity(target);
    }

    Vector3 CalculateVelocity(Vector3 target)
    {
        var position = _thisTransform.position;
        float gravityY = Physics.gravity.y;
        
        float displacementY = target.y - position.y;
        Vector3 displacementXZ =
            new Vector3(target.x - position.x, 0, target.z - position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravityY * highness);
        Vector3 velocityXZ = displacementXZ /
                             (Mathf.Sqrt(-2 * highness / gravityY) + Mathf.Sqrt(2 * (displacementY - highness) / gravityY));

        return velocityXZ + velocityY;
    }

    void Update()
    {
        if (_playerWithBall)
        {
            _thisTransform.position = _thisTransform.parent.position;
        }
    }
}
