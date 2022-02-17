using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] bool isWithBall;
    [SerializeField] float speed;
    [SerializeField] ChargingBarView chargingBar;

    Rigidbody _rigidbody;
    float _xMovement;
    float _zMovement;
    Vector3 _velocityComposition = new Vector3();
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        chargingBar.OnMaxValue += OnMaxShootingCharge;
        chargingBar.Value = 0;
    }
    
    void Update()
    {
        if (isWithBall)
        {
            _xMovement = Input.GetAxis("Horizontal");
            _zMovement = Input.GetAxis("Vertical");

            _velocityComposition.x = _xMovement * speed * Time.deltaTime;
            _velocityComposition.z = _zMovement * speed * Time.deltaTime;

            _rigidbody.velocity = _velocityComposition;
            
            transform.LookAt(transform.position + new Vector3(_xMovement, 0f, _zMovement));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            chargingBar.Value += Time.deltaTime;
        }
    }

    void OnMaxShootingCharge()
    {
        Debug.Log("## On max shooting star! ##");
    }
}
