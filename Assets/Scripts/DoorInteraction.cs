using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorInteractable : MonoBehaviour, IInteractable
{
    [Header("Requirements")]
    public bool requireKey = true;

    [Header("Animation/SFX (optional)")]
    public Animator animator;               
    public string openTrigger = "Open";     
    public AudioSource audioSource;
    public AudioClip openSfx;

    [Header("Completion (optional)")]
    public bool loadNextScene = false;
    public string nextSceneName = "";       
    public float loadDelay = 2f;

    private bool opened;

    private void Awake()
    {
        if (!animator) animator = GetComponentInChildren<Animator>();
        if (!audioSource) audioSource = GetComponentInChildren<AudioSource>();
    }

    public void Interact()
    {
        if (opened) return;

        var inv = FindObjectOfType<PlayerInventory>();
        bool hasKey = inv && inv.hasExitKey;

        if (requireKey && !hasKey)
        {
            UIManager.Instance?.ShowMessage("The exit is locked. Find the key!");
            return;
        }

        opened = true;

        
        if (animator) animator.SetTrigger(openTrigger);
        if (audioSource && openSfx) audioSource.PlayOneShot(openSfx);

        UIManager.Instance?.ShowMessage("Escape successful!");

        
        var col = GetComponent<Collider>();
        if (col) col.enabled = false;

        
        if (loadNextScene && !string.IsNullOrWhiteSpace(nextSceneName))
            Invoke(nameof(LoadNext), loadDelay);
    }

    private void LoadNext()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public string GetPrompt()
    {
        if (opened) return null;

        var inv = FindObjectOfType<PlayerInventory>();
        bool hasKey = inv && inv.hasExitKey;

        if (requireKey && !hasKey) return "Exit (Locked)";
        return "Open exit";
    }
}