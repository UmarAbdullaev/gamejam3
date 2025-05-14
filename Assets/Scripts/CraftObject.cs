using EasyBuildSystem.Features.Runtime.Buildings.Part;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Craft", menuName = "Developer/CraftObject")]
public class CraftObject : ScriptableObject
{
    [Serializable]
    public class Requirement
    {
        [SerializeField] private MineralManager.Type type;
        [SerializeField] private int number;

        public MineralManager.Type Type => type;
        public int Number => number;
    }

    [SerializeField] private string _name;
    [SerializeField] private Sprite sprite;
    [SerializeField] private BuildingPart prefab;

    [SerializeField] private Requirement[] requirements;

    public Requirement[] Requirements => requirements;
    public string Name => _name;
    public Sprite Sprite => sprite;
    public BuildingPart Prefab => prefab;
}
