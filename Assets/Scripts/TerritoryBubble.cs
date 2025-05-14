using EasyBuildSystem.Features.Runtime.Buildings.Part;
using UnityEngine;

public class TerritoryBubble : MonoBehaviour
{
    [SerializeField] private Transform circle;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float scale = 5f; // radius in world units
    [SerializeField] private BuildingPart buildingPart;

    private TerritryManager territryManager;

    public float Scale => scale;

    private void OnValidate()
    {
        if (circle != null)
            circle.localScale = Vector3.one * scale;
    }

    // In TerritoryScript.cs
    private void Awake()
    {
        territryManager = FindFirstObjectByType<TerritryManager>();

        if (buildingPart)
            buildingPart.OnChangedStateEvent.AddListener((BuildingPart.StateType state) =>
            {
                transform.rotation = Quaternion.identity;
                Debug.Log(state);
                switch (state)
                {
                    case BuildingPart.StateType.PLACED:
                        territryManager.Add(this);
                        break;
                    case BuildingPart.StateType.DESTROY:
                        territryManager.Remove(this);
                        break;
                    case BuildingPart.StateType.PREVIEW:
                        territryManager.Remove(this);
                        break;
                }
            });
    }


    private void Update()
    {
        Apply();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, scale);
    }

    private void Apply()
    {
        if (circle != null)
            circle.localScale = Vector3.one * scale;


        if (_renderer != null && _renderer.sharedMaterial != null)
        {
            float size = scale; // Diameter
            Vector2 textureScale = new Vector2(size, size);
            Vector2 worldPos = new Vector2(transform.position.x, transform.position.z) / 2;
            Vector2 offset = -worldPos + new Vector2(-scale * .5f, -scale * .5f);

            Material mat = _renderer.material; // This creates a unique instance per object
            mat.mainTextureScale = textureScale;
            mat.mainTextureOffset = offset;
        }
    }
}
