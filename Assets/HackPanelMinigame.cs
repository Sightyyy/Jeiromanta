using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public int rows = 10;
    public int cols = 10;
    public GameObject wallPrefab;
    public GameObject mousePrefab;
    public GameObject goalPrefab;
    public RectTransform canvas; // Referensi ke Canvas yang sudah ada di dalam background
    private GameObject mouse;
    private Vector2Int mousePosition;
    private Vector2Int goalPosition;

    private float cellSize = 40f; // Ukuran sel grid sesuai dengan ukuran wallPrefab
    private Vector2 gridSize;

    void Start()
    {
        gridSize = new Vector2(cols * cellSize, rows * cellSize);
        InitializeMaze();
        PlaceMouse();
        PlaceGoalPoint();
        PlaceMazeWalls();
    }

    void InitializeMaze()
    {
        // Membuat border menggunakan wallPrefab di sekitar grid
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (x == 0 || x == cols - 1 || y == 0 || y == rows - 1)
                {
                    Vector2Int borderPos = new Vector2Int(x, y);
                    GameObject wall = Instantiate(wallPrefab, GetWorldPosition(borderPos), Quaternion.identity, canvas);
                    SetObjectScale(wall, 40f);
                }
            }
        }
    }

    void PlaceMouse()
    {
        mousePosition = new Vector2Int(1, 1); // Start position di dalam border
        mouse = Instantiate(mousePrefab, GetWorldPosition(mousePosition), Quaternion.identity, canvas);
        SetObjectScale(mouse, 20f); // Set scale for mouse
    }

    void PlaceGoalPoint()
    {
        goalPosition = new Vector2Int(cols - 2, rows - 2); // Set goal di ujung dalam border
        GameObject goal = Instantiate(goalPrefab, GetWorldPosition(goalPosition), Quaternion.identity, canvas);
        SetObjectScale(goal, 40f); // Set scale for goal
    }

    void PlaceMazeWalls()
    {
        // Randomisasi tembok di dalam maze, tanpa mengganggu border
        for (int i = 0; i < (rows * cols) / 4; i++) // Jumlah tembok disesuaikan dengan kebutuhan
        {
            Vector2Int wallPos;
            do
            {
                wallPos = new Vector2Int(Random.Range(1, cols - 1), Random.Range(1, rows - 1));
            } while (wallPos == mousePosition || wallPos == goalPosition);

            GameObject wall = Instantiate(wallPrefab, GetWorldPosition(wallPos), Quaternion.identity, canvas);
            SetObjectScale(wall, 40f); // Set scale for walls
        }
    }

    void SetObjectScale(GameObject obj, float desiredSize)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.localScale = new Vector3(1, 1, 1); // pastikan skala default
            rt.sizeDelta = new Vector2(desiredSize, desiredSize); // set ukuran
        }
        else
        {
            obj.transform.localScale = new Vector3(desiredSize / 100f, desiredSize / 100f, 1);
        }
    }

    Vector3 GetWorldPosition(Vector2Int gridPos)
    {
        // Menghitung posisi dalam Canvas
        RectTransform rt = canvas.GetComponent<RectTransform>();
        Vector2 canvasSize = rt.rect.size;

        float xPos = gridPos.x * cellSize - (canvasSize.x / 2) + cellSize / 2;
        float yPos = gridPos.y * cellSize - (canvasSize.y / 2) + cellSize / 2;

        return new Vector3(xPos, yPos, 0);
    }
}
