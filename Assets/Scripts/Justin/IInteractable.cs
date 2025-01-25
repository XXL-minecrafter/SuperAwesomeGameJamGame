using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Add [field: SerializeField] when implemented.
    /// </summary>
    public GameObject InteractionBox { get; set; }
    public void Interact();
    public void ShowInteractionBox();
    public void DisableInteractionBox();
}
