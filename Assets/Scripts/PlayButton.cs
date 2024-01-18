using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void OnPlayButtonClicked()
        {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
        else
        {
            Debug.LogError("GameManager instance is not set.");
        }
    }
}
