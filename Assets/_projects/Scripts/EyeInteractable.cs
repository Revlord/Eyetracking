using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class EyeInteractable : MonoBehaviour
{
    private bool isHovered;
    public bool IsHovered
    {
        get { return isHovered; }
        set
        {
            if (isHovered != value)
            {
                isHovered = value;
                UpdateMaterial();
                if (isHovered)
                    OnObjectHover?.Invoke(gameObject);
            }
        }
    }

    [SerializeField]
    private UnityEvent<GameObject> OnObjectHover;

    [SerializeField]
    private Material OnHoverActiveMaterial;

    [SerializeField]
    private Material OnHoverInactiveMaterial;

    private MeshRenderer meshRenderer;
    
    void Start() => meshRenderer = GetComponent<MeshRenderer>();

    private void UpdateMaterial()
    {
        meshRenderer.material = isHovered ? OnHoverActiveMaterial : OnHoverInactiveMaterial;
    }
}
