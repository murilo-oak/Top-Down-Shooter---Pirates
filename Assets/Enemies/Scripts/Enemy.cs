using System.Collections;
using Controls;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IActor
{
    void Start()
    {

    }

    public void ExecuteCommand()
    {
        Debug.Log("IA");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
