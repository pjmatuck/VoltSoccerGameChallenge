using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        Vector3 _cameraOffset;
        Transform _target;
        bool _onTransition;

        public Transform Target
        {
            private get => _target;
            set
            {
                _target = value;
                StartCoroutine(PerformTransition());
            }
        }

        IEnumerator PerformTransition()
        {
            _onTransition = true;
            float elapsedTime = 0;
            float timeToTransition = 1f;
            var initialPosition = transform.position;
            
            while (elapsedTime < timeToTransition)
            {
                transform.position = Vector3.Lerp(initialPosition, Target.position + _cameraOffset, timeToTransition);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = Target.position + _cameraOffset;
            _onTransition = false;
            yield return null;
        }

        void Update()
        {
            if (!_onTransition)
            {
                transform.position = Target.position + _cameraOffset;
            }
        }

        public void SetInitialPosition(Transform target)
        {
            _cameraOffset = transform.position - target.localPosition;
            Target = target;
        }
    }
}