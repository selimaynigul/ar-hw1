using UnityEngine;
using Vuforia;

public class ImageVisibilityController : MonoBehaviour
{
    public GameObject imageQuad; // The Quad that displays the JPG image
    public ObserverBehaviour imageTarget; // The Image Target's ObserverBehaviour component

    void Start()
    {
        // Initially hide the image
        if (imageQuad != null)
        {
            imageQuad.SetActive(false);
        }

        // Register the event handler for target detection
        if (imageTarget != null)
        {
            imageTarget.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    // Event handler for target detection changes
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        // Check if the target is tracked
        if (targetStatus.Status == Status.TRACKED)
        {
            // Show the image
            if (imageQuad != null)
            {
                imageQuad.SetActive(true);
            }
        }
        else
        {
            // Hide the image when the target is not detected
            if (imageQuad != null)
            {
                imageQuad.SetActive(false);
            }
        }
    }

    void OnDestroy()
    {
        // Unregister the event handler to avoid memory leaks
        if (imageTarget != null)
        {
            imageTarget.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}
