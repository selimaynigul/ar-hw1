using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger area is the BluePill
        if (other.gameObject.name == "Blue Pill")
        {
            // Load RonaldoScene
            SceneManager.LoadScene("RonaldoScene");
        }
        // Check if the object entering the trigger area is the RedPill
        else if (other.gameObject.name == "Red Pill")
        {
            // Load MessiScene
            SceneManager.LoadScene("MessiScene");
        }
    }
}
