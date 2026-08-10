// Author : Louis Hoe Zheng Sheng, Carolyn Ong
// i find it kinda stupid that i need a script to define an interface but i guess this is how unity works
// Date: 27/07/2026

public interface IInteractable
{
    string PromptText { get; }
    void OnRaycastEnter();
    void OnRaycastExit();
    void OnInteract();
}