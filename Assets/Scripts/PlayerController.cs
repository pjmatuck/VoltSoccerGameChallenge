using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] bool isWithBall;
    [SerializeField] float speed;
    [SerializeField] int shootingPower;
    [SerializeField] ChargingBarView chargingBar;
    [SerializeField] BallBehavior ball;

    Rigidbody _rigidbody;
    float _xMovement;
    float _zMovement;
    Vector3 _velocityComposition = new Vector3();
    bool _holdingShootingButton;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        chargingBar.OnMaxValue += OnMaxShootingCharge;
        chargingBar.ResetBar();
    }
    
    void Update()
    {
        if (isWithBall)
        {
            _xMovement = Input.GetAxis("Horizontal");
            _zMovement = Input.GetAxis("Vertical");

            _velocityComposition.x = _xMovement * speed * Time.deltaTime;
            _velocityComposition.z = _zMovement * speed * Time.deltaTime;
            
            Debug.DrawLine(transform.position, _velocityComposition * 10f, Color.magenta);

            _rigidbody.velocity = _velocityComposition;
            
            transform.LookAt(transform.position + new Vector3(_xMovement, 0f, _zMovement));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(ball != null) HoldShootingButton();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(ball != null) ReleaseShootingButton();
        }
    }

    void HoldShootingButton()
    {
        Debug.Log("## Hold shooting star! ##");
        _holdingShootingButton = true;
        StartCoroutine(ChargeBar());
    }

    IEnumerator ChargeBar()
    {
        while (_holdingShootingButton)
        {
            chargingBar.Value += Time.deltaTime;
            yield return null;
        }
    }

    void ReleaseShootingButton()
    {
        StopAllCoroutines();
        Debug.Log("## Release shooting star! ##");
        if (!_holdingShootingButton) return;
        
        _holdingShootingButton = false;
        var velocity = _rigidbody.velocity;
        ball.Shoot(chargingBar.Value * shootingPower, 
            velocity != Vector3.zero ? velocity.normalized : transform.forward);
        chargingBar.ResetBar();
    }

    void OnMaxShootingCharge()
    {
        Debug.Log("## On max shooting star! ##");
        _holdingShootingButton = false;
        var velocity = _rigidbody.velocity;
        ball.Shoot(shootingPower, 
            velocity != Vector3.zero ? velocity.normalized : transform.forward);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ball = other.gameObject.GetComponent<BallBehavior>();
        }
    }
}
