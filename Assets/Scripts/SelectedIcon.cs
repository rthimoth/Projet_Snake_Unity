using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedIcon : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gm;
    [SerializeField] public RectTransform selected_icon;
    private void Start()
    {
        gm = GameManager.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        if (selected_icon != null)
        {
            switch (gm.level)
            {
                case 1:
                    selected_icon.anchorMin = new Vector2(0, 0.5f);
                    selected_icon.anchorMax = new Vector2(0, 0.5f);
                    selected_icon.anchoredPosition = new Vector2(235, -200);
                    break;
                case 2:
                    selected_icon.anchorMin = new Vector2(0.5f, 0.5f);
                    selected_icon.anchorMax = new Vector2(0.5f, 0.5f);
                    selected_icon.anchoredPosition = new Vector2(0, -200);
                    break;
                case 3:
                    selected_icon.anchorMin = new Vector2(1, 0.5f);
                    selected_icon.anchorMax = new Vector2(1, 0.5f);
                    selected_icon.anchoredPosition = new Vector2(-235, -200);
                    break;
                default:
                    Debug.LogError("Invalid level selected");
                    break;
            }
        }
        else
        {
            Debug.LogError("No RectTransform component found on the GameObject.");
        }
    }
}
