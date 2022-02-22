using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] CameraController _cameraController;
    [SerializeField] float minimumAnglePass;

    List<PlayerController> _players = new List<PlayerController>();

    void Awake()
    {
        _players = FindObjectsOfType<PlayerController>().ToList();
        
        _players.ForEach(p => p.ReceivedBallCallback += BallIsWithOtherPlayer);
        
        _cameraController.SetInitialPosition(_players.Find(p => p.IsWithBall).transform);
    }

    public Transform FindClosestPlayer(PlayerController playerPassing, Vector3 lookingAt)
    {
        Transform closestPlayer = null;
        float minorAngle = minimumAnglePass + 1;
        foreach (var player in _players)
        {
            if(player == playerPassing) continue;

            var targetDir = player.transform.position - playerPassing.transform.position;
            float angleDegrees = Vector3.Angle(targetDir, lookingAt);
            if (angleDegrees < minorAngle && angleDegrees < minimumAnglePass)
            {
                minorAngle = angleDegrees;
                closestPlayer = player.transform;
            }
        }

        return closestPlayer;
    }

    void BallIsWithOtherPlayer(Transform player)
    {
        _cameraController.Target = player;
    }
}
