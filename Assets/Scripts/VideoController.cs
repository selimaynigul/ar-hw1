using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the Video Player component
    public ObserverBehaviour targetObserver; // Reference to the AR target's ObserverBehaviour

    void Start()
    {
        // Ensure the video is paused initially
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
        }

        // Register for Vuforia target detection events
        if (targetObserver != null)
        {
            targetObserver.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    // Event handler for target detection changes
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        // Check if the target is tracked
        if (targetStatus.Status == Status.TRACKED)
        {
            PlayVideo();
        }
        else
        {
            PauseVideo();
        }
    }

    // Method to start playing the video
    public void PlayVideo()
    {
        if (videoPlayer != null && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    // Method to pause the video
    public void PauseVideo()
    {
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
    }

    void OnDestroy()
    {
        // Unregister from the Vuforia target detection events
        if (targetObserver != null)
        {
            targetObserver.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}
