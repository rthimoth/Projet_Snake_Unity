using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    public Transform segmentPrefab;
    public Vector2Int direction = Vector2Int.right;
    public float speed = 20f;
    public float speedMultiplier = 1f;
    public int initialSize = 4;
    public bool moveThroughWalls = false;
    GameManager gm;
    Spawner spawner;
    private List<Transform> segments = new List<Transform>();
    private Vector2Int input;
    private float nextUpdate;
    private int HP = 0;
    private float time_reverse_control = 0;
    private float timeEvent = 10f;
    public bool isReverseControlActive = false; // Set this to true when the reverse control bonus is activated

    private void Start()
    {
        ResetState();
        gm = GameManager.Instance; 
        spawner = Spawner.Instance; 
        gm.onReverseControl.AddListener(ActiveReverseControle);
        
    }

    private void Update()
    {
        if (gm.isPlaying)
        {
            if (isReverseControlActive)
            {
                time_reverse_control += Time.deltaTime;
                // Debug.Log("time IS EVENT : + " + time + "");
                ReverseControl();
                // Debug.Log("ReverseControl IS AFTER : + " + isReverseControlActive + "");
            }
            else
            {
                NormalControle();
                // Debug.Log("time IS normal : + " + time + "");
            }
        }
    }

    private void FixedUpdate()
    {
        // Wait until the next update before proceeding
        if (Time.time < nextUpdate) { 
            return;
        }

        // Set the new direction based on the input
        if (input != Vector2Int.zero) {
            direction = input;
        }

        // Set each segment's position to be the same as the one it follows. We
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        int x = Mathf.RoundToInt(transform.position.x) + direction.x;
        int y = Mathf.RoundToInt(transform.position.y) + direction.y;
        transform.position = new Vector2(x, y);

        // Set the next update time based on the speed
        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    public void ResetState()
    {
        direction = Vector2Int.right;
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }

        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);

        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++) {
            Grow();
        }
    }

    public bool Occupies(int x, int y)
    {
        foreach (Transform segment in segments)
        {
            if (Mathf.RoundToInt(segment.position.x) == x &&
                Mathf.RoundToInt(segment.position.y) == y) {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Grow();
            Destroy(other.gameObject);
            spawner.InstantiateObject(spawner.prefabElements[0]);
        }
        else if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Player") || (other.gameObject.CompareTag("Wall") && moveThroughWalls == false))
        {
            HP -= 1;
            // Debug.Log("HP DEATH : " + HP + "");
            if (HP < 0)
            {
                Death();
            }

        }
        else if (other.gameObject.CompareTag("HP"))
        {
            HP += 1;
            // Debug.Log("HP : " + HP + "");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall") && moveThroughWalls)
        {
            Traverse(other.transform);
        }
    }
    private void Death()
    {
        ResetState();
        gm.GameOver();
    }

    private void Traverse(Transform wall)
    {
        Vector3 position = transform.position;

        if (direction.x != 0f) {
            position.x = Mathf.RoundToInt(-wall.position.x + direction.x);
        } else if (direction.y != 0f) {
            position.y = Mathf.RoundToInt(-wall.position.y + direction.y);
        }

        transform.position = position;
    }
    
    public void ReverseControl()
    {
        
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Vector2Int.down; // Reversed
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Vector2Int.up; // Reversed
            }
        }
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                input = Vector2Int.left; // Reversed
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                input = Vector2Int.right; // Reversed
            }
        }

        if (time_reverse_control >= timeEvent)
        {
            // Debug.Log("ReverseControl IS BEFORE "+ isReverseControlActive + "");
            isReverseControlActive = false;
            time_reverse_control = 0;
        }
    }

    public void NormalControle()
    {
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Vector2Int.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Vector2Int.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                input = Vector2Int.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                input = Vector2Int.left;
            }
        }
    }
    public void ActiveReverseControle()
    {
        isReverseControlActive = true;
    }
}
