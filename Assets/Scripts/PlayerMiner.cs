using Unity.VisualScripting;
using UnityEngine;

public class PlayerMiner : MonoBehaviour
{
    [SerializeField] private Animator pickaxeAnimator;

    [Space]
    [SerializeField] private LayerMask mineralLayer;
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private float mineCooldown = 0.5f;

    private float cooldownTimer = 0f;

    private void Update()
    {
        // Check cooldown timer
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // If mouse button is clicked and cooldown has passed
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0f)
        {
            // Play the pickaxe animation
            pickaxeAnimator.Play("Mine");

            // Perform the raycast
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, mineralLayer))
            {
                if (hit.transform.TryGetComponent(out Mineral mineral))
                {
                    mineral.Mine(); // Call the Mine method on the Mineral
                }
                else if (hit.transform.TryGetComponent(out ResourceGenerator resourceGenerator))
                {
                    resourceGenerator.Take();
                }
            }

            // Reset the cooldown timer
            cooldownTimer = mineCooldown;
        }
    }
}