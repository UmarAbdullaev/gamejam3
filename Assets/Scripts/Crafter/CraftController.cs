using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using EasyBuildSystem.Features.Runtime.Buildings.Placer;

public class CraftController : MonoBehaviour
{
    [SerializeField] private CraftObject[] crafts;
    [SerializeField] private CraftCard cardPrefab;
    [SerializeField] private Transform craftListPivot;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private FirstPersonController characterController;

    private List<CraftCard> cards;
    private MineralManager mineralManager;

    private void Start()
    {
        mineralManager = FindFirstObjectByType<MineralManager>();

        Popup(false);

        InitializeCrafts();
    }

    private void InitializeCrafts()
    {
        cards = new List<CraftCard>();

        foreach (var craft in crafts)
        {
            CraftCard card = Instantiate(cardPrefab, craftListPivot);

            card.Set(craft);

            cards.Add(card);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Popup(!canvasGroup.interactable);
        }
    }

    private void Popup(bool value)
    {
        canvasGroup.DOFade(value ? 1 : 0, .2f);
        canvasGroup.interactable = value;

        if (value)
        {
            foreach (var card in cards)
            {
                card.UpdateState();
            }
        }

        characterController.SetPause(value);
    }

    public void RequestCraft(CraftObject craftObject)
    {
        if (BuildingPlacer.Instance.GetBuildMode != BuildingPlacer.BuildMode.PLACE)
        {
            if (mineralManager.CanSpend(craftObject.Requirements))
            {
                BuildingPlacer.Instance.ChangeBuildMode(BuildingPlacer.BuildMode.PLACE);
                BuildingPlacer.Instance.SelectBuildingPart(craftObject.Prefab);
            }
        }

        Popup(false);
    }
}
