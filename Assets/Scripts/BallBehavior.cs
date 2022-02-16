using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    SpringJoint _joint;
    PlayerController _playerWithBall;
    
    void Start()
    {
        _joint = GetComponent<SpringJoint>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _joint.connectedBody = other.rigidbody;
            _playerWithBall = other.gameObject.GetComponent<PlayerController>();
        }
    }
}
