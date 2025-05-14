using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOxygen : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private float maxOxygen = 100f;
    [SerializeField] private float depletionSpeed = 1f;
    [SerializeField] private float restorationSpeed = 10f;
    [SerializeField] private CanvasGroup interfaceGroup;
    [SerializeField] private CanvasGroup deathInterface;

    private bool alive;
    private TerritryManager territryManager;
    private float oxygen;
    private bool state;

    private void Start()
    {
        territryManager = FindFirstObjectByType<TerritryManager>();

        oxygen = maxOxygen;
        state = true;
        alive = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(Application.loadedLevel);

        if (Input.GetKeyDown(KeyCode.Q))
            Application.Quit();

        if (!alive)
            return;

        bool inZone = territryManager.IsPointInZone(transform.position);

        if (inZone != state)
        {
            state = inZone;

            interfaceGroup.DOKill();

            if (state)
                interfaceGroup.DOFade(0f, 1f);
        }

        oxygen = Mathf.Clamp(oxygen + (inZone ? restorationSpeed : -depletionSpeed) * Time.deltaTime, 0, maxOxygen);

        if (!state)
            interfaceGroup.alpha = 1 - oxygen / maxOxygen;

        slider.value = oxygen / maxOxygen;

        if (oxygen == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        alive = false;
        FindFirstObjectByType<FirstPersonController>().SetPause(true);
        FindFirstObjectByType<CraftController>().enabled = false;
        deathInterface.DOFade(1f, 1f);
    }
}
