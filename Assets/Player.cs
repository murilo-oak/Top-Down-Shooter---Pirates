using Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IActor
{
    // Start is called before the first frame update
    void Start()
    {
        InputHandler inputHandler = InputHandler.instance;
        inputHandler.AddActor(this);
    }

    public void ExecuteCommand() 
    {
        
    }

    public GameObject GetGameObject() 
    {
        return gameObject;
    }
}
