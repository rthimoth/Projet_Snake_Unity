using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsGameButton : MonoBehaviour
{
    public void OnOptionButtonClicked()
        {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadOptionMenu();
        }
        else
        {
            Debug.LogError("GameManager instance is not set.");
        }
    }
}
