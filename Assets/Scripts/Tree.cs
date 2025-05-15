using UnityEngine;

public class Tree : MonoBehaviour
{
    private bool initialized = false;

    private void Start()
    {
        if (!initialized)
        {
            var territoryManager = FindFirstObjectByType<TerritryManager>();
            territoryManager.trees++;
            territoryManager.UpdateTerraformation();
        }

        initialized = true;
    }
}
