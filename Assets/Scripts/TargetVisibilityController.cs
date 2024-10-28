using UnityEngine;
using Vuforia;

public class TargetVisibilityController : MonoBehaviour
{
    private ObserverBehaviour observerBehaviour;
    public GameObject targetObject; // The object to show/hide

    void Start()
    {
        // Get the ObserverBehaviour component attached to the ImageTarget
        observerBehaviour = GetComponent<ObserverBehaviour>();

        if (observerBehaviour != null)
        {
            // Register the OnTargetStatusChanged event
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        // Make sure the object is hidden at the start
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }

    // Event handler to manage visibility based on tracking status
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetObject != null)
        {
            // Make the object visible when the target is tracked
            if (targetStatus.Status == Status.TRACKED)
            {
                targetObject.SetActive(true);
            }
            else
            {
                // Hide the object when the target is lost
                targetObject.SetActive(false);
            }
        }
    }
}
