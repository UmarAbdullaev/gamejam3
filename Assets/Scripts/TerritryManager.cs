using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class TerritryManager : MonoBehaviour
{
    public List<TerritoryBubble> territories = new List<TerritoryBubble>();
    [SerializeField] private Transform[] greens;
    [SerializeField] private Slider terraformationSlider;
    [SerializeField] private int fullFormation = 36;
    [SerializeField] private CanvasGroup victoryInterface;
    [SerializeField] private TextMeshProUGUI terraformationText;

    private void Start()
    {
        foreach (var green in greens)
        {
            green.localScale = Vector3.zero;
        }

        UpdateTerraformation();
    }

    public void Add(TerritoryBubble territory)
    {
        if (!territories.Contains(territory))
        {
            territories.Add(territory);
        }

        UpdateTerraformation();
    }

    public void Remove(TerritoryBubble territory)
    {
        if (territories.Contains(territory))
        {
            territories.Remove(territory);
        }

        UpdateTerraformation();
    }

    public bool IsPointInZone(Vector3 position)
    {
        foreach (var territory in territories)
        {
            if (territory)
            {
                if (Vector3.Distance(territory.transform.position, position) < territory.Scale)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void Update()
    {
        foreach (var green in greens)
        {
            if (IsPointInZone(green.transform.position) && green.transform.localScale == Vector3.zero)
            {
                green.DOScale(new Vector3(.75f, 1.25f, .75f), .2f)
                    .OnComplete(() => green.DOScale(Vector3.one, .2f));
            }
        }
    }

    private void UpdateTerraformation()
    {
        terraformationSlider.DOValue((float)territories.Count / fullFormation, .1f);
        terraformationText.text = $"{(int)((float)territories.Count / fullFormation * 100)}% Habitable";

        if (territories.Count >= fullFormation)
        {
            victoryInterface.DOFade(1f, 1f);

            FindFirstObjectByType<FirstPersonController>().SetPause(true);
            FindFirstObjectByType<CraftController>().enabled = false;
        }
    }
}
