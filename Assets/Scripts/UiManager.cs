using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI eventUI;
    GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance; 
        gm.onEvent.AddListener(ShowText);
        eventUI.gameObject.SetActive(false);
    }
    private void OnGUI()
    {
        scoreUI.text = gm.FormatScore();
    }
    public void ShowText(){
        StartCoroutine(ShowAndHide(eventUI, 5f)); // 5 seconds
    }

    IEnumerator ShowAndHide(TextMeshProUGUI uiElement, float delay)
    {
        eventUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        eventUI.gameObject.SetActive(false);
    }
}
