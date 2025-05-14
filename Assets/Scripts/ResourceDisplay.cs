using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI valueText; // or TMPro.TextMeshProUGUI if you're using TextMesh Pro

    public void Setup(Sprite icon, int value)
    {
        iconImage.sprite = icon;
        UpdateValue(value);
    }

    public void UpdateValue(int newValue)
    {
        valueText.text = newValue.ToString();

        gameObject.SetActive(newValue > 0);
    }
}
