using UnityEngine;
using System.Collections.Generic;
public class Spawner : MonoBehaviour
{
    public Collider2D gridArea;
    public static Spawner Instance { get; private set; } // Singleton instance

    [SerializeField] public List<GameObject> prefabElements; // List of UI elements
    GameManager gm;
    private Snake snake;
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
        gm.onExtraLife.AddListener(() => InstantiateObject(prefabElements[1]));
    }

    public Vector2 RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;
        Vector2 position;

        do
        {
            float x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
            float y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));
            position = new Vector2(x, y);
        } 
        while (snake.Occupies((int)position.x, (int)position.y));

        return position;
    }
    public void InstantiateObject(GameObject gamerObject)
    {
        Instantiate(gamerObject, RandomizePosition(), Quaternion.identity);
    }

}
