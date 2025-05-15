using UnityEngine;
using DG.Tweening; // For DOTween
using UnityEngine.Events;
using UnityEngine.UI; // For event handling

public class Health : MonoBehaviour
{
    [SerializeField] private CanvasGroup healthBarCanvasGroup; // The CanvasGroup that holds the health bar
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float healthBarVisibilityDuration = 1f; // Duration for health bar to be visible

    private float currentHealth;
    private float maxHealth = 100f; // Maximum health of the mineral

    // Event triggered when health is updated
    public UnityAction<float> OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBarCanvasGroup.alpha = 0f; // Health bar starts invisible

        OnHealthChanged += (float value) => healthSlider.DOValue(value / maxHealth, .1f);
    }

    public void TakeDamage(float amount)
    {
        // Reduce health
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f); // Prevent health from going below 0

        // Trigger health change event
        OnHealthChanged?.Invoke(currentHealth);

        // Show and fade the health bar
        ShowHealthBar();
    }

    private void ShowHealthBar()
    {
        // Show the health bar instantly
        healthBarCanvasGroup.DOKill(); // Kill any previous tweens
        healthBarCanvasGroup.DOFade(1f, 0f); // Instant fade-in

        // Fade out the health bar after the duration
        healthBarCanvasGroup.DOFade(1f, healthBarVisibilityDuration)
            .OnComplete(() => healthBarCanvasGroup.DOFade(0, .2f));
    }

    // Optionally, you can implement a function to check the current health if needed
    public float GetCurrentHealth() => currentHealth;

    // Optionally, you can expose the max health as well
    public float GetMaxHealth() => maxHealth;

    public void Refill()
    {
        currentHealth = maxHealth;

        OnHealthChanged.Invoke(currentHealth);
    }
}
