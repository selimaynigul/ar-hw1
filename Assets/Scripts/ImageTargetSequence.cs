using UnityEngine;
using TMPro; // For TextMeshPro support
using Vuforia;

public class ImageTargetSequence : MonoBehaviour
{
    // Define the targets in the order you want them to be found
    public GameObject target1;
    public GameObject target2;
    public GameObject ball; // The ball object to be moved to the goal
    public GameObject goalArea; // The goal area GameObject

    public TextMeshProUGUI statusText; // Text component to provide feedback
    public SceneChanger sceneChanger;  // Reference to the SceneChanger script
    private int currentTargetIndex = 0;

    // Flags to keep track of detected targets and goal completion
    private bool target1Found = false;
    private bool target2Found = false;
    private bool goalCompleted = false;

    void Start()
    {
        // Register the event handlers for the first two ImageTargets
        RegisterTargetHandler(target1);
        RegisterTargetHandler(target2);

        // Initialize the status text
        if (statusText != null)
        {
            statusText.text = "1) Find Messi with Ballond'or";
        }

        // Ensure the goal area has a trigger collider for detecting the ball
        if (goalArea != null)
        {
            Collider goalCollider = goalArea.GetComponent<Collider>();
            if (goalCollider != null && !goalCollider.isTrigger)
            {
                goalCollider.isTrigger = true;
            }
        }
    }

    // Register the event handler for each target
    private void RegisterTargetHandler(GameObject target)
    {
        var trackable = target.GetComponent<ObserverBehaviour>();

        if (trackable != null)
        {
            // Register the correct delegate with ObserverBehaviour
            trackable.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }


    // Correct method signature for OnTargetStatusChanged
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        // Check if the target is tracked
        if (targetStatus.Status == Status.TRACKED)
        {
            // Lock the target if it's detected in the correct order
            if (CheckTargetOrder(behaviour))
            {
                UpdateStatusText();

                // Move to the next target in the sequence
                currentTargetIndex++;

                // Lock each target once it's found
                if (behaviour == target1.GetComponent<ObserverBehaviour>())
                {
                    target1Found = true;
                }
                else if (behaviour == target2.GetComponent<ObserverBehaviour>())
                {
                    target2Found = true;
                }

                // If the first two targets are found, instruct the user to move the ball to the goal
                if (currentTargetIndex == 2 && !goalCompleted)
                {
                    statusText.text = "3) Found! Find Messi with World Cup and move the ball to the Goal!";
                }
            }
        }
    }

    // Check if the currently detected target is the expected one
    private bool CheckTargetOrder(ObserverBehaviour behaviour)
    {
        if (currentTargetIndex == 0 && behaviour == target1.GetComponent<ObserverBehaviour>() && !target1Found)
        {
            return true;
        }
        else if (currentTargetIndex == 1 && behaviour == target2.GetComponent<ObserverBehaviour>() && !target2Found)
        {
            return true;
        }
        return false;
    }

    // Update the status text based on progress
    private void UpdateStatusText()
    {
        if (statusText != null)
        {
            switch (currentTargetIndex)
            {
                case 0:
                    statusText.text = "2) Found! Find Messi with shirt.";
                    break;
                case 1:
                    statusText.text = "3) Found! Find Messi with World Cup and move the ball to the Goal!";
                    break;
            }
        }
    }

    // Complete the goal task (called when the ball reaches the goal)
    public void CompleteGoalTask()
    {
        // Only proceed if both targets have been found in the correct order
        if (target1Found && target2Found && !goalCompleted)
        {
            goalCompleted = true;
            currentTargetIndex++;

            if (statusText != null)
            {
                statusText.text = "Ball Goal Completed!";
            }

            // Notify the SceneChanger that all tasks are completed
            if (sceneChanger != null)
            {
                sceneChanger.SetTasksCompleted(true);
            }
        }
    }

}
