using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] MazeNode nodePrefab;
    [SerializeField] GameObject mazePlayerPrefab;
    [SerializeField] GameObject mazeGenerator;
    [SerializeField] GameObject endPoint;
    [SerializeField] Vector2Int mazeSize;
    [SerializeField] Transform mazeGeneratorRotation;
    public Vector3 startPointPosition, endPointPosition; // Store the start point position
    private static GameObject mazePlayer;
    public bool isWin = false;
    

    public void NewMaze()
    {
        GenerateMazeInstant(mazeSize);
        SpawnMazePlayer();
        // StartCoroutine(GenerateMaze(mazeSize));
    }

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(endPoint.transform.position, endPoint.transform.forward, out hit, -10f))
        {
            if(hit.collider.gameObject.name == "Player")
            {
                Debug.Log("You win!");
            }
        }
    }

    void GenerateMazeInstant(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();
        mazeGeneratorRotation.rotation = Quaternion.Euler(0, 0, 0);

        // Create nodes
        for (int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 4f, y - (size.y / 2f) + 103f);
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(newNode);
            }
        }
        
        mazeGeneratorRotation.rotation = Quaternion.Euler(-90f, 0, 0);

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // Choose Starting node
        MazeNode startNode = nodes[Random.Range(0, nodes.Count)];
        currentPath.Add(startNode);
        startNode.SetState(NodeState.Start);  // Set the Start state
        startPointPosition = startNode.transform.position; // Store the start position

        while(completedNodes.Count < nodes.Count)
        {
            // Check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if(currentNodeX < size.x - 1)
            {
                // Check node to the right of the current node
                if(!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                   !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if(currentNodeX > 0)
            {
                // Check node to the left of the current node
                if(!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                   !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if(currentNodeY < size.y - 1)
            {
                // Check node above the current node
                if(!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                   !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if(currentNodeY > 0)
            {
                // Check node below the current node
                if(!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                   !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // Choose next node
            if(possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }

        // Find the furthest node from the start node
        MazeNode endNode = null;
        float maxDistance = -1f;

        foreach (MazeNode node in nodes)
        {
            if (node != startNode)
            {
                float distance = Vector3.Distance(startNode.transform.position, node.transform.position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    endNode = node;
                }
            }
        }

        if (endNode != null)
        {
            endNode.SetState(NodeState.End);
            endPoint.transform.position = endNode.transform.position;
        }
    }

    IEnumerator GenerateMaze(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();

        // Create nodes
        for (int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 5f, y - (size.y / 2f) + 103f);
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(newNode);

                yield return null;
            }
        }
        mazeGeneratorRotation.rotation = Quaternion.Euler(-90f, 0, 0);

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // Choose Starting node
        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
        currentPath[0].SetState(NodeState.Current);

        while(completedNodes.Count < nodes.Count)
        {
            // Check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if(currentNodeX < size.x - 1)
            {
                // Check node to the right of the current node
                if(!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                   !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if(currentNodeX > 0)
            {
                // Check node to the left of the current node
                if(!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                   !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if(currentNodeY < size.y - 1)
            {
                // Check node above the current node
                if(!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                   !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if(currentNodeY > 0)
            {
                // Check node below the current node
                if(!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                   !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // Choose next node
            if(possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);
                chosenNode.SetState(NodeState.Current);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath[currentPath.Count - 1].SetState(NodeState.Completed);
                currentPath.RemoveAt(currentPath.Count - 1);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void SpawnMazePlayer()
    {
        if (mazePlayerPrefab != null)
        {
            // Instantiate the maze player at the start point
            mazePlayer = Instantiate(mazePlayerPrefab, startPointPosition, Quaternion.identity);
        }
    }

    public static void DestroyMazePlayer()
    {
        Destroy(mazePlayer);
    }

    public void DestroyMazeNodes()
    {
        foreach(Transform child in mazeGenerator.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
