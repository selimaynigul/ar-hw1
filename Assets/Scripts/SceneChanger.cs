using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshPro support

public class SceneChanger : MonoBehaviour
{
    public TextMeshProUGUI statusText; // Reference to the status text for feedback
    private bool tasksCompleted = false; // Flag to track if tasks are completed
    private float messageDuration = 3f; // Duration to show the "Finish tasks first" message
    private float messageTimer = 0f;

    private string originalText = ""; // Store the original text
    private Color originalColor; // Store the original color

    void Start()
    {
        // Store the initial text and color to restore later
        if (statusText != null)
        {
            originalText = statusText.text;
            originalColor = statusText.color;
        }
    }

    void LateUpdate() // Use LateUpdate to ensure UI updates happen after rendering
    {
        // If a warning message is active, update the timer and reset after duration
        if (messageTimer > 0)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0 && statusText != null)
            {
                // Restore the original message and color
                statusText.text = originalText;
                statusText.color = originalColor;
            }
        }
    }

    // Method to set the task completion status from ImageTargetSequence
    public void SetTasksCompleted(bool completed)
    {
        tasksCompleted = completed;

        // Show completion text in green
        if (tasksCompleted && statusText != null)
        {
            statusText.text = "Tasks Completed!";
            statusText.color = Color.green; // Set text color to green
        }
    }

    // Method to load a scene by name with task completion check
    public void LoadSceneByName(string sceneName)
    {
        if (tasksCompleted)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            // Show warning message for 3 seconds in red
            if (statusText != null)
            {
                originalText = statusText.text; // Store the current text
                originalColor = statusText.color; // Store the current color
                statusText.text = "Finish tasks first!";
                statusText.color = Color.red; // Set text color to red
                messageTimer = messageDuration;
            }
        }
    }
}
