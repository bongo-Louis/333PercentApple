// Author : Louis Hoe Zheng Sheng
// i find it kinda stupid that i need a script to define an interface but i guess this is how unity works
public interface IInteractable
{
    string PromptText { get; }
    void OnRaycastEnter();
    void OnRaycastExit();
}