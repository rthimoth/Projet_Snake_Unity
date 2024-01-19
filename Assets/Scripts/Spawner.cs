using UnityEngine;
using System.Collections; // Add this to the top of your file
using System.Collections.Generic;
public class Spawner : MonoBehaviour
{
    public Collider2D gridArea;
    public static Spawner Instance { get; private set; } // Singleton instance

    [SerializeField] public List<GameObject> prefabElements; // List of UI elements
    GameManager gm;
    private float delay = 8f;
    private Snake snake;
    public LayerMask obstacle_layer;
    Vector2 boxSize = new Vector2(4, 4);
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        snake = FindObjectOfType<Snake>();
    }
    private void Start()
    {
        gm = GameManager.Instance;
        SetListeners();
        InstantiateObject(prefabElements[0]);
  
    }
    private void SetListeners()
    {
        gm.onExtraLife.AddListener(() => StartCoroutine(InstantiateTemporyObject(prefabElements[1],delay)));
    }

    public Vector2 RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        // Pick a random position inside the bounds
        // Round the values to ensure it aligns with the grid
        int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
        int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));

        // Prevent the food from spawning on the snake
        while (snake.Occupies(x, y) && Physics2D.OverlapBox(new Vector2(x,y), boxSize, 0, obstacle_layer))
        {
            x++;

            if (x > bounds.max.x)
            {
                x = Mathf.RoundToInt(bounds.min.x);
                y++;

                if (y > bounds.max.y) {
                    y = Mathf.RoundToInt(bounds.min.y);
                }
            }
        }

        return new Vector2(x, y);
    }
    public void InstantiateObject(GameObject gameObject)
    {
        Instantiate(gameObject, RandomizePosition(), Quaternion.identity);
    }
    IEnumerator InstantiateTemporyObject(GameObject gamerObject, float delay)
    {
        GameObject instance = Instantiate(gamerObject, RandomizePosition(), Quaternion.identity);
        yield return new WaitForSeconds(delay);
        Destroy(instance);
    }

}