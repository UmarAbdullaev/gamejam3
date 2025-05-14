using System;
using System.Collections.Generic;
using UnityEngine;

public class MineralManager : MonoBehaviour
{
    [SerializeField] private ResourceCollectEffect collectEffectPrefab;
    [SerializeField] private Canvas uiCanvas;
    private Dictionary<Type, ResourceDisplay> uiDisplays = new();


    public enum Type
    {
        Iron,
        Gold
    }

    [Serializable]
    public class Resource
    {
        [SerializeField] private Type type;
        [SerializeField] private Sprite sprite;

        public Type Type => type;
        public Sprite Sprite => sprite;
    }

    [SerializeField] private ResourceDisplay resourceDisplayPrefab;
    [SerializeField] private Transform resourceListParent;
    [SerializeField] private Resource[] resources;

    private Dictionary<Type, int> minerals = new Dictionary<Type, int>();

    private void Start()
    {
        foreach (var resource in resources)
        {
            minerals.Add(resource.Type, 0);
        }
    }

    public void Add(Type type, int number = 1, Vector3? worldPosition = null)
    {
        minerals[type] += number;
        UpdateDisplay();

        if (worldPosition.HasValue && uiDisplays.TryGetValue(type, out var targetDisplay))
        {
            Vector3 startScreen = Camera.main.WorldToScreenPoint(worldPosition.Value);
            Vector3 endScreen = targetDisplay.transform.position;

            var fx = Instantiate(collectEffectPrefab, uiCanvas.transform);
            fx.Init(GetPreference(type).Sprite, startScreen, endScreen);
        }
    }



    private void UpdateDisplay()
    {
        foreach (Transform child in resourceListParent)
            Destroy(child.gameObject);

        uiDisplays.Clear();

        foreach (var resource in resources)
        {
            var disp = Instantiate(resourceDisplayPrefab, resourceListParent);
            disp.Setup(resource.Sprite, GetNumber(resource.Type));

            uiDisplays.Add(resource.Type, disp);
        }
    }



    public Resource GetPreference(Type type)
    {
        foreach (var resource in resources)
        {
            if (resource.Type == type)
            {
                return resource;
            }
        }

        return null;
    }

    public int GetNumber(Type type)
    {
        return minerals[type];
    }

    public bool Spend(CraftObject.Requirement[] requirements)
    {
        if (CanSpend(requirements))
            foreach (var req in requirements)
            {
                if (!Spend(req))
                {
                    return false;
                }
            }
        else
            return false;

        return true;
    }

    public bool Spend(CraftObject.Requirement requirement)
    {
        return Spend(requirement.Type, requirement.Number);
    }

    public bool Spend(Type type, int number)
    {
        if (minerals[type] >= number)
        {
            minerals[type] -= number;

            UpdateDisplay();

            return true;
        }

        return false;
    }

    public bool CanSpend(CraftObject.Requirement[] requirements)
    {
        foreach (var req in requirements)
        {
            if (!CanSpend(req))
            {
                return false;
            }
        }

        return true;
    }

    public bool CanSpend(CraftObject.Requirement requirement)
    {
        return CanSpend(requirement.Type, requirement.Number);
    }

    public bool CanSpend(Type type, int number) => minerals[type] >= number;
}
