using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI HP_UI;
    [SerializeField] private List<TextMeshProUGUI> uiElements; // List of UI elements
    private GameManager gm;
    private Snake snk;

    private void Start()
    {
        gm = GameManager.Instance;
        snk = Snake.Instance;
        SetListeners();
        SetActiveUIElements(uiElements,false);

    }
    private void SetActiveUIElements(List<TextMeshProUGUI> uiElements, bool isActive){
        foreach (var uiElement in uiElements)
        {
            uiElement.gameObject.SetActive(isActive);
        }
    }
    private void SetListeners()
    {
        gm.onReverseControl.AddListener(() => ShowText(uiElements[0]));
        gm.onExtraLife.AddListener(() => ShowText(uiElements[1]));
    }
    private void OnGUI()
    {
        scoreUI.text = gm.FormatScore();
        HP_UI.text = "HP: " + snk.HP.ToString();
    }
    public void ShowText(TextMeshProUGUI uiElement)
    {
        StartCoroutine(ShowAndHide(uiElement, 8f));
    }

    IEnumerator ShowAndHide(TextMeshProUGUI uiElement, float delay)
    {
        uiElement.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        uiElement.gameObject.SetActive(false);
    }

}
