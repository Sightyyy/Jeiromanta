using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public int rows = 10;
    public int cols = 10;
    public GameObject wallPrefab;
    public GameObject mousePrefab;
    public GameObject goalPrefab;
    public GameObject backgroundPrefab; // Background object prefab
    public Transform mazeParent; // Parent untuk semua elemen maze
    private GameObject background; // Background object instance
    private GameObject mouse;
    private Vector2Int mousePosition;
    private Vector2Int goalPosition;
    
    private float cellSize = 40f; // Ukuran sel grid sesuai dengan ukuran wallPrefab
    private Vector2 gridSize;

    void Start()
    {
        gridSize = new Vector2(cols * cellSize, rows * cellSize);
        CreateBackground();
        InitializeMaze();
        PlaceMouse();
        PlaceGoalPoint();
        PlaceMazeWalls();
    }

    void CreateBackground()
    {
        // Instantiate the background object
        background = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity);
        background.transform.SetParent(mazeParent, false);
        RectTransform bgRect = background.GetComponent<RectTransform>();
        bgRect.sizeDelta = gridSize;
        background.transform.localScale = new Vector3(1, 1, 1); // Reset scale to default
    }

    void InitializeMaze()
    {
        // Initialize the maze layout and place borders
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (x == 0 || x == cols - 1 || y == 0 || y == rows - 1)
                {
                    Vector2Int borderPos = new Vector2Int(x, y);
                    Instantiate(wallPrefab, GetWorldPosition(borderPos), Quaternion.identity, background.transform);
                }
            }
        }
    }

    void PlaceMouse()
    {
        mousePosition = new Vector2Int(1, 1); // Start position inside border
        mouse = Instantiate(mousePrefab, GetWorldPosition(mousePosition), Quaternion.identity, background.transform);
        SetObjectScale(mouse, 20f); // Set scale for mouse
    }

    void PlaceGoalPoint()
    {
        goalPosition = new Vector2Int(cols - 2, rows - 2); // Set goal in the grid
        GameObject goal = Instantiate(goalPrefab, GetWorldPosition(goalPosition), Quaternion.identity, background.transform);
        SetObjectScale(goal, 40f); // Set scale for goal
    }

    void PlaceMazeWalls()
    {
        // Randomly place walls inside the maze, excluding borders
        for (int i = 0; i < (rows * cols) / 4; i++) // Number of walls adjusted as needed
        {
            Vector2Int wallPos;
            do
            {
                wallPos = new Vector2Int(Random.Range(1, cols - 1), Random.Range(1, rows - 1));
            } while (wallPos == mousePosition || wallPos == goalPosition);

            GameObject wall = Instantiate(wallPrefab, GetWorldPosition(wallPos), Quaternion.identity, background.transform);
            SetObjectScale(wall, 40f); // Set scale for walls
        }
    }

    void SetObjectScale(GameObject obj, float desiredSize)
    {
        // Get the current size of the prefab
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            float currentSize = sr.sprite.bounds.size.x; // Assuming uniform scale
            float scale = desiredSize / currentSize;
            obj.transform.localScale = new Vector3(scale, scale, 1);
        }
    }

    Vector3 GetWorldPosition(Vector2Int gridPos)
    {
        return new Vector3(
            gridPos.x * cellSize - gridSize.x / 2 + cellSize / 2,
            gridPos.y * cellSize - gridSize.y / 2 + cellSize / 2,
            0
        );
    }
}
