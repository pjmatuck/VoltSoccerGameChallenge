using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDBillboard : MonoBehaviour
{
    [SerializeField] Transform camera;

    void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
