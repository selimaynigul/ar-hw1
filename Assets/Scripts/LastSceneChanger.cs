using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshPro support

public class LastSceneChanger : MonoBehaviour
{
    // Method to load a scene by name with task completion check
    public void LoadScene()
    {
      
        SceneManager.LoadScene("SecondScene");
        
    }
}

