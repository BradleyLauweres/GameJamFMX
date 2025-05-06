using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomTipsController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject tipPanel;
    [SerializeField] private TextMeshProUGUI tipText;

    [Header("Tip Settings")]
    [SerializeField]
    private List<string> tips = new List<string>()
    {
        "Tip 1: Remember to save your game frequently!",
        "Tip 2: You can press 'M' to open the map.",
        "Tip 3: Enemies are weaker against their elemental opposites.",
        "Tip 4: Try combining different items to discover new recipes!"
    };

    [Header("Display Settings")]
    [SerializeField] private float displayDuration = 5f;
    [SerializeField] private float fadeInTime = 0.5f;
    [SerializeField] private float fadeOutTime = 0.5f;
    [SerializeField] private bool showOnStart = true;
    [SerializeField] private bool showRandomlyDuringGame = true;
    [SerializeField] private float minTimeBetweenTips = 60f;
    [SerializeField] private float maxTimeBetweenTips = 180f;
    [SerializeField] private bool resetAfterAllShown = true;

    private CanvasGroup panelCanvasGroup;
    private HashSet<int> shownTipIndices = new HashSet<int>();
    private Coroutine currentTipCoroutine;
    private bool isTipVisible = false;

    void Awake()
    {
        panelCanvasGroup = tipPanel.GetComponent<CanvasGroup>();
        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = tipPanel.AddComponent<CanvasGroup>();
        }

      
        panelCanvasGroup.alpha = 0f;
        tipPanel.SetActive(false);

        shownTipIndices = new HashSet<int>();
    }

    void Start()
    {
        if (showOnStart)
        {
            ShowRandomTip();
        }

        if (showRandomlyDuringGame)
        {
            StartCoroutine(ShowTipsRandomly());
        }
    }

    private IEnumerator ShowTipsRandomly()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenTips, maxTimeBetweenTips);
            yield return new WaitForSeconds(waitTime);

            if (!isTipVisible)
            {
                ShowRandomTip();
            }
        }
    }

    public void ShowRandomTip()
    {
        if (tips.Count == 0) return;

        if (shownTipIndices.Count >= tips.Count)
        {
            if (resetAfterAllShown)
            {
                shownTipIndices.Clear();
                Debug.Log("All tips have been shown. Resetting tracking.");
            }
            else
            {
                Debug.Log("All tips have been shown. No more tips will be displayed.");
                return;
            }
        }

        List<int> availableTipIndices = new List<int>();
        for (int i = 0; i < tips.Count; i++)
        {
            if (!shownTipIndices.Contains(i))
            {
                availableTipIndices.Add(i);
            }
        }

        int randomIndex = Random.Range(0, availableTipIndices.Count);
        int selectedTipIndex = availableTipIndices[randomIndex];

        shownTipIndices.Add(selectedTipIndex);

        ShowTip(tips[selectedTipIndex]);
    }

    public void ShowTip(string tip)
    {
        if (currentTipCoroutine != null)
        {
            StopCoroutine(currentTipCoroutine);
        }

        currentTipCoroutine = StartCoroutine(DisplayTipCoroutine(tip));
    }

    private IEnumerator DisplayTipCoroutine(string tip)
    {
        tipText.text = tip;
        tipPanel.SetActive(true);
        isTipVisible = true;

   
        yield return FadeCanvasGroup(panelCanvasGroup, 0f, 1f, fadeInTime);

   
        yield return new WaitForSeconds(displayDuration);

 
        yield return FadeCanvasGroup(panelCanvasGroup, 1f, 0f, fadeOutTime);

     
        tipPanel.SetActive(false);
        isTipVisible = false;
        currentTipCoroutine = null;
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        canvasGroup.alpha = startAlpha;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }

    public void AddTip(string newTip)
    {
        tips.Add(newTip);
    }

    public void ClearTips()
    {
        tips.Clear();
        shownTipIndices.Clear();
    }

    public void ResetShownTips()
    {
        shownTipIndices.Clear();
        Debug.Log("Reset tracking of shown tips.");
    }

    void Update()
    {
      
    }
}