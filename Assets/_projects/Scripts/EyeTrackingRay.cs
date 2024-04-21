using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class EyeTrackingRay : MonoBehaviour
{
    [SerializeField]
    private float rayDistance = 10.0f;
    [SerializeField]
    private float rayWidth = 0.01f;
    [SerializeField]
    private LayerMask layersToInclude;

    [SerializeField]
    private Color rayColorDefaultState = Color.yellow;
    [SerializeField]
    private Color rayColorHoverState = Color.red;

    private LineRenderer lineRenderer;
    private EyeInteractable currentInteractable;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetupRay();
    }

    void SetupRay()
    {
        lineRenderer.startWidth = rayWidth;
        lineRenderer.endWidth = rayWidth;
        lineRenderer.startColor = rayColorDefaultState;
        lineRenderer.endColor = rayColorDefaultState;
    }

    void Update()
    {
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, layersToInclude);

        if (hitSomething)
        {
            var interactable = hit.collider.GetComponent<EyeInteractable>();
            if (interactable != currentInteractable)
            {
                if (currentInteractable != null)
                    currentInteractable.IsHovered = false;

                currentInteractable = interactable;
                if (currentInteractable != null)
                    currentInteractable.IsHovered = true;

                lineRenderer.startColor = rayColorHoverState;
                lineRenderer.endColor = rayColorHoverState;
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                currentInteractable.IsHovered = false;
                currentInteractable = null;
            }

            lineRenderer.startColor = rayColorDefaultState;
            lineRenderer.endColor = rayColorDefaultState;
        }
    }
}
