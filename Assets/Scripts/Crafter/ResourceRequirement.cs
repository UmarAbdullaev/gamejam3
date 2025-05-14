using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceRequirement : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI numberText;

    private MineralManager mineralManager;
    private MineralManager.Type type;
    private int required;

    public void Set(MineralManager.Type type, int number, MineralManager manager)
    {
        mineralManager = manager;
        this.type = type;
        required = number;
    }

    public void UpdateState()
    {
        MineralManager.Resource resource = mineralManager.GetPreference(type);

        int minerals = mineralManager.GetNumber(type);

        image.sprite = resource.Sprite;
        numberText.text = $"{minerals}/{required}";
    }

    public bool IsAvailable => mineralManager.GetNumber(type) >= required;
}
