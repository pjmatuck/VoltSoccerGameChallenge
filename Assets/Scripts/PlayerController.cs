using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] bool _isWithBall;
    [SerializeField] float _speed;

    Rigidbody _rigidbody;
    float _xMovement;
    float _zMovement;
    Vector3 _velocityComposition = new Vector3();
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (_isWithBall)
        {
            _xMovement = Input.GetAxis("Horizontal");
            _zMovement = Input.GetAxis("Vertical");

            _velocityComposition.x = _xMovement * _speed * Time.deltaTime;
            _velocityComposition.z = _zMovement * _speed * Time.deltaTime;

            _rigidbody.velocity = _velocityComposition;
        }
    }
}
