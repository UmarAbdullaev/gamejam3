using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private MineralManager.Type type;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float generationTime = 1f;
    [SerializeField] private int max = 15;
    [SerializeField] private Image progress;
    [SerializeField] private Image mineralDisplay;

    private int number;
    private float timer;

    private void Start()
    {
        number = 0;

        UpdateInterface();

        mineralDisplay.sprite = FindFirstObjectByType<MineralManager>().GetPreference(type).Sprite;
    }

    private void UpdateInterface()
    {
        text.text = $"{number}/{max}";
    }

    private void Update()
    {
        if (number < max)
        {
            timer += Time.deltaTime;
            if (timer > generationTime)
            {
                number++;

                UpdateInterface();

                timer = 0f;
            }

            progress.fillAmount = timer / generationTime;
        }
        else
            progress.fillAmount = 1f;
    }

    public void Take()
    {
        if (number > 0)
            FindFirstObjectByType<MineralManager>().Add(type, number, transform.position + Vector3.up);

        number = 0;
        UpdateInterface();
    }
}
