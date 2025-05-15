using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    [SerializeField] private MineralManager.Type type;
    [SerializeField] private Health health; // Reference to the health script
    [SerializeField] private Transform pivot;
    [SerializeField] private GameObject mineParticle;

    [Space]
    [SerializeField] private bool regenerative;
    [SerializeField] private float regenerationTime = 10f;

    [SerializeField] private float miningDamage = 25f; // Damage to apply on mining

    private bool available;

    private void Awake()
    {
        if (health == null)
        {
            health = GetComponent<Health>(); // Find the Health component if not assigned
        }

        available = true;
    }

    // Called when the mineral is mined
    public void Mine()
    {
        if (!available)
            return;

        if (health != null)
        {
            health.TakeDamage(miningDamage); // Apply damage to the mineral's health
        }

        pivot.DOKill();

        Instantiate(mineParticle, pivot.position, Quaternion.identity);

        // You can implement additional mining logic like destroying the mineral when health is 0
        if (health.GetCurrentHealth() <= 0f)
        {
            FindFirstObjectByType<MineralManager>().Add(type, 1, transform.position);
             
            Hide();
        }
        else
        {
            pivot.DOShakePosition(.1f, .1f);
        }
    }

    private void Hide()
    {
        available = false;
        pivot.DOScale(0, .2f);
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        if (regenerative)
            StartCoroutine(DelayedShow(regenerationTime));
    }

    private IEnumerator DelayedShow(float delay)
    {
        yield return new WaitForSeconds(delay);

        Show();
    }

    private void Show()
    {
        available = true;

        health.Refill();

        pivot.DOScale(1, .2f);
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
    }
}
