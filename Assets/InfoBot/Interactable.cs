using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    private Outline outline;
    public string message;

    public UnityEvent onInteraction;

    [Header("NPC Settings")]
    public Animator npcAnimator; // Reference to the NPC's Animator
    public string idleAnimation = "Idle"; // Name of the idle animation state
    public string talkingAnimation = "Talking"; // Name of the talking animation state
    public float transitionDuration = 0.5f; // Duration for smooth transition between animations
    public AudioSource audioSource; // Reference to the AudioSource for playing the clip
    public AudioClip talkingClip; // The audio clip to play

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        onInteraction.Invoke();

        if (npcAnimator != null)
        {
            npcAnimator.CrossFade(talkingAnimation, transitionDuration); // Smooth transition to talking animation
            Invoke("PlayAudioWithDelay", 1f); // Call the audio function after a 1-second delay
        }
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }

    private void PlayAudioWithDelay()
    {
        if (audioSource != null && talkingClip != null)
        {
            audioSource.clip = talkingClip;
            audioSource.Play();
            Invoke("SwitchBackToIdle", talkingClip.length); // Switch back to idle after the clip finishes
        }
    }

    private void SwitchBackToIdle()
    {
        if (npcAnimator != null)
        {
            npcAnimator.CrossFade(idleAnimation, transitionDuration); // Smooth transition back to idle animation
        }
    }
}
