using DG.Tweening;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    [SerializeField] private MineralManager.Type type;
    [SerializeField] private Health health; // Reference to the health script
    [SerializeField] private Transform pivot;
    [SerializeField] private GameObject mineParticle;

    [SerializeField] private float miningDamage = 25f; // Damage to apply on mining

    private void Awake()
    {
        if (health == null)
        {
            health = GetComponent<Health>(); // Find the Health component if not assigned
        }
    }

    // Called when the mineral is mined
    public void Mine()
    {
        if (!enabled)
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
            enabled = false;
            pivot.DOScale(0, .2f);
            FindFirstObjectByType<MineralManager>().Add(type, 1, transform.position);
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            pivot.DOShakePosition(.1f, .1f);
        }
    }
}
