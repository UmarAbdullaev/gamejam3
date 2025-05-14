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

    private int number;
    private float timer;

    private void Start()
    {
        number = 0;

        UpdateInterface();
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
    }

    public void Take()
    {
        if (number > 0)
            FindFirstObjectByType<MineralManager>().Add(type, number, transform.position);

        number = 0;
        UpdateInterface();
    }
}
