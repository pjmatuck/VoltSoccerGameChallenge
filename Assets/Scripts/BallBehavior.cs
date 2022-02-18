using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    SpringJoint _joint;
    Rigidbody _rigidbody;
    PlayerController _playerWithBall;
    
    void Start()
    {
        _joint = GetComponent<SpringJoint>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _joint.connectedBody = other.rigidbody;
            _playerWithBall = other.gameObject.GetComponent<PlayerController>();
        }
    }

    public void Shoot(float power, Vector3 direction)
    {
        _joint.connectedBody = null;
        _rigidbody.AddForce(direction * power , ForceMode.Force);
    }
}
