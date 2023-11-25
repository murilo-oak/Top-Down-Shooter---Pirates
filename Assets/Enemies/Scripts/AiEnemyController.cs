using System.Collections;
using Controls;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AiEnemyController : MonoBehaviour
{
    InputAction virtualAction;
    [SerializeField] MoveFowardCommand moveFowardCommand;
    [SerializeField] RotateAnticlockwiseCommand rotateAnticlockwiseCommand;
    InputHandler inputHandler;


    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
    }
    private void FixedUpdate()
    {
        moveFowardCommand.MoveFoward(gameObject);
        rotateAnticlockwiseCommand.rotate(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right);
    }
}
