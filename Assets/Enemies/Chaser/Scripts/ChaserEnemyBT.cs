using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Controls;

public class ChaserEnemyBT : BTree
{
    [Header("Commands of Behaviour Tree")]
    [SerializeField] private MoveFowardCommand moveFowardCommand;
    [SerializeField] private RotateAnticlockwiseCommand rotateAnticlockwiseCommand;
    [SerializeField] private RotateClockwiseCommand rotateClockwiseCommand;


    private void Start()
    {
        root = SetupTree();
        
        Transform player = GameObject.Find("Player").transform;
        
        if(player != null)
        {
            root.SetData("target", player);
        }
    }
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>{new TaskGoToTarget(transform, moveFowardCommand, rotateClockwiseCommand, rotateAnticlockwiseCommand)}) 
        }); 

        return root;
    }
}
