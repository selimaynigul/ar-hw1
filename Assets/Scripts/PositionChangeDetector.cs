using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionChangeDetector : MonoBehaviour
{
    public Transform referencePoint;  // Assign ReferencePoint in the Inspector
    private Vector3 lastLocalPosition;
    public string sceneToLoad;

    private void Start()
    {
        // Store the initial local position relative to the reference point
        lastLocalPosition = referencePoint.InverseTransformPoint(transform.position);
    }

    private void Update()
    {
        // Calculate the current local position relative to the reference point
        Vector3 currentLocalPosition = referencePoint.InverseTransformPoint(transform.position);

        // Check if the local position has changed
        if (currentLocalPosition != lastLocalPosition)
        {
            Debug.Log($"{gameObject.name} has moved relative to reference point. Loading {sceneToLoad}");

            // Load the designated scene
            SceneManager.LoadScene(sceneToLoad);

            // Update the last known local position
            lastLocalPosition = currentLocalPosition;
        }
    }
}
