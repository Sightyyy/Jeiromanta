using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;

public enum NodeState
{
    Available,
    Current,
    Completed,
    Start,
    End
}

public class MazeNode : MonoBehaviour
{
    [SerializeField] GameObject[] walls;
    [SerializeField] MeshRenderer floor;

    public void RemoveWall(int wallToRemove)
    {
        walls[wallToRemove].gameObject.SetActive(false);
    }

    public void SetState(NodeState state)
    {
        if(state == NodeState.Available)
        {
            floor.material.color = Color.white;
        }
        else if(state == NodeState.Start)
        {
            floor.material.color = Color.black;
        }
        else if(state == NodeState.End)
        {
            floor.material.color = Color.green;
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
