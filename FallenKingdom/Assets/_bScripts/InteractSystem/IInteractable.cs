

public interface IInteractable
{

    public string InteractionText { get; }

    public bool IsInteractable { get; }

    public bool TalkToPlayer { get; }
    void Interact();

}
