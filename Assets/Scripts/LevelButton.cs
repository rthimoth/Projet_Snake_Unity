using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelButton : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] public GameObject level_button;

    public TextMeshProUGUI level_button_text;
    private void Start()
    {
        if (GameManager.Instance != null)
        {
             gm = GameManager.Instance;
        }
        else
        {
            Debug.LogError("GameManager instance is not set.");
        }

        if (level_button != null)
        {
            level_button_text = level_button.GetComponentInChildren<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("No Button component found.");
        }

    }

    public void OnLevelButtonClicked(Button button)
    {
        
        if (level_button_text != null)
        {
            gm.SetLevels(level_button_text.text);
        }
        else
        {
            Debug.LogError("No Text component found on the button.");
        }
    }
}
