using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CraftCard : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private ResourceRequirement resourceRequirementPrefab;
    [SerializeField] private Transform requirementListParent;

    private CraftObject craftObject;
    private List<ResourceRequirement> resourceRequirements;

    public void Set(CraftObject craftObject)
    {
        this.craftObject = craftObject;

        nameText.text = craftObject.Name;
        image.sprite = craftObject.Sprite;

        InitializeResourceRequirement();
    }

    private void InitializeResourceRequirement()
    {
        resourceRequirements = new List<ResourceRequirement>();
        MineralManager mineralManager = FindFirstObjectByType<MineralManager>();

        foreach (var requirement in craftObject.Requirements)
        {
            ResourceRequirement requirementObject = Instantiate(resourceRequirementPrefab, requirementListParent);
            requirementObject.Set(requirement.Type, requirement.Number, mineralManager);
            resourceRequirements.Add(requirementObject);
        }
    }

    public void UpdateState()
    {
        bool available = true;

        foreach (var requirement in resourceRequirements)
        {
            requirement.UpdateState();

            if (!requirement.IsAvailable)
            {
                available = false;
            }
        }

        canvasGroup.interactable = available;
    }

    public void RequestCraft()
    {
        FindFirstObjectByType<CraftController>().RequestCraft(craftObject);
    }
}
