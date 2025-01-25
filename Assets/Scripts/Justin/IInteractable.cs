using UnityEngine;

public interface IInteractable
{
    public GameObject InteractionBox { get; set; }
    public void Interact();

    public void LoadInteractionBox();
    public void ShowInteractionBox();
}
