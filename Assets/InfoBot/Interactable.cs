using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    private Outline outline;
    public string message;

    public UnityEvent onInteraction;

    [Header("NPC Settings")]
    public Animator npcAnimator;
    public string idleAnimation = "Idle";
    public string talkingAnimation = "Talking";
    public float transitionDuration = 0.5f;
    public AudioSource audioSource;
    public AudioClip talkingClip;

    [Header("Material Changer Settings")]
    public Renderer targetRenderer;
    public Material[] materials;
    private int currentMaterialIndex = 0;
    private bool isChangingMaterial = false;
    private float materialChangeCooldown = 0.2f; // Cooldown between changes
    private float lastMaterialChangeTime = -1f;

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();

        if (targetRenderer != null && materials.Length > 0)
        {
            targetRenderer.material = materials[0]; // Set the initial material
        }
    }

    public void Interact()
    {
        Debug.Log("Interact called");

        onInteraction.Invoke();

        if (npcAnimator != null)
        {
            npcAnimator.CrossFade(talkingAnimation, transitionDuration);
            Invoke("PlayAudioWithDelay", 1f);
        }

        if (!isChangingMaterial && Time.time >= lastMaterialChangeTime + materialChangeCooldown)
        {
            StartCoroutine(ChangeMaterialWithDelay(0.1f));
            lastMaterialChangeTime = Time.time; // Update the last change time
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
            Invoke("SwitchBackToIdle", talkingClip.length);
        }
    }

    private void SwitchBackToIdle()
    {
        if (npcAnimator != null)
        {
            npcAnimator.CrossFade(idleAnimation, transitionDuration);
        }
    }

    private IEnumerator ChangeMaterialWithDelay(float delay)
    {
        isChangingMaterial = true;

        yield return new WaitForSeconds(delay);

        if (materials.Length > 0)
        {
            currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;
            targetRenderer.material = materials[currentMaterialIndex]; // Set the material explicitly

            Debug.Log($"Material changed to: {materials[currentMaterialIndex].name} (Index: {currentMaterialIndex})");
        }

        isChangingMaterial = false;
    }
}

