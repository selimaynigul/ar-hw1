using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;
using TMPro; // If you want to use TextMeshPro for displaying messages on the screen

public class LeanPillSelector : MonoBehaviour
{
    // The name of the scene to load when this pill is selected
    public string sceneToLoad;
    public TextMeshProUGUI statusText;

    void Update()
    {
        // Check if there is a touch on the screen
        if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch touch = Input.GetTouch(0);

            // Debugging line to check if a touch is detected
            Debug.Log("Touch detected: " + touch.phase);

            if (touch.phase == TouchPhase.Began)
            {
                if (statusText != null)
                {
                    statusText.text = "Screen touched!";
                }

                // Perform a raycast from the touch position
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Debugging line to check what was hit by the raycast
                    Debug.Log("Raycast hit: " + hit.transform.name);

                    // Check if the object hit is the Image Target
                    if (hit.transform == transform)
                    {
                        // Load the specified scene
                        LoadScene();
                    }
                }
            }
        }
    }

    // Load the specified scene
    private void LoadScene()
    {
        if (statusText != null)
        {
            statusText.text = "Load Scene!";
        }
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene to load is not specified for this pill object.");
        }
    }
}
