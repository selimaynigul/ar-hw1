using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    public GameObject ball; // The ball GameObject to track
    public ImageTargetSequence taskManager; // Reference to the main task manager script

    private bool goalCompleted = false;

    void OnTriggerEnter(Collider other)
    {
        // Check if the ball has entered the goal area
        if (other.gameObject == ball && !goalCompleted)
        {
            goalCompleted = true;

            // Notify the task manager that the third task is completed
            if (taskManager != null)
            {
                taskManager.CompleteGoalTask();
            }
        }
    }
}
