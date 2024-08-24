using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum NodeState
{
    Available,
    Current,
    Completed
}

public class MazeNode : MonoBehaviour
{
    [SerializeField] GameObject[] walls;
    [SerializeField] MeshRenderer floor;

    public void SetState(NodeState state)
    {
        if(state == NodeState.Available)
        {
            floor.material.color = Color.white;
        }
        else if(state == NodeState.Current)
        {
            floor.material.color = Color.yellow;
        }
        else if(state == NodeState.Completed)
        {
            floor.material.color = Color.blue;
        }
    }
}
