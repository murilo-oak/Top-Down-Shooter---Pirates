using System.Collections;
using Controls;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AiEnemyController : MonoBehaviour
{
    [SerializeField] MoveFowardCommand moveFowardCommand;
    [SerializeField] RotateAnticlockwiseCommand rotateAnticlockwiseCommand;


    private void Start()
    {
    }
    private void FixedUpdate()
    {
        moveFowardCommand.PerformCommand(gameObject);
        rotateAnticlockwiseCommand.PerformCommand(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right);
    }
}
