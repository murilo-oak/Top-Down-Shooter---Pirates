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

    public GameObject GetGameObject() 
    { 
        return gameObject;
    }
}
