using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TerritryManager : MonoBehaviour
{
    public List<TerritoryBubble> territories = new List<TerritoryBubble>();
    public int trees;
    [SerializeField] private Transform[] greens;
    [SerializeField] private int fullFormationArea = 21;
    [SerializeField] private int fullFormationTree = 30;
    [SerializeField] private CanvasGroup victoryInterface;

    [Space]
    [SerializeField] private TextMeshProUGUI terraformationText;
    [SerializeField] private Image areaFill, treeFill;
    [SerializeField] private TextMeshProUGUI treePercent, areaPercent;

    private void Start()
    {
        foreach (var green in greens)
        {
            green.localScale = Vector3.zero;
        }

        UpdateTerraformation();
    }

    public void AddTerritory(TerritoryBubble territory)
    {
        if (!territories.Contains(territory))
        {
            territories.Add(territory);
        }

        UpdateTerraformation();
    }

    public void RemoveTerritory(TerritoryBubble territory)
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

    public void UpdateTerraformation()
    {
        float areaValue = (float)territories.Count / fullFormationArea;
        float treeValue = (float)trees / fullFormationTree + (areaValue * .2f);

        treeFill.DOFillAmount(treeValue, .1f);
        areaFill.DOFillAmount(areaValue, .1f);
        treePercent.text = (int)(treeValue * 100) + "%";
        areaPercent.text = (int)(areaValue * 100) + "%";
        terraformationText.text = $"Terraformation ({(int)( (treeValue + areaValue) * 100 )}%)";

        if (territories.Count >= fullFormationArea && trees >= fullFormationTree)
        {
            victoryInterface.DOFade(1f, 1f);

            FindFirstObjectByType<FirstPersonController>().SetPause(true);
            FindFirstObjectByType<CraftController>().enabled = false;
        }
    }
}
