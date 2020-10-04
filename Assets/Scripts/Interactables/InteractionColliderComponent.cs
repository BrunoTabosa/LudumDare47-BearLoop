using UnityEngine;


public class InteractionColliderComponent : CollisionListener
{

    private BaseInteractionObject _interactionObject;

    public BaseInteractionObject InteractionObject { get => _interactionObject; }

    public void InteractionColliderInit(InteractionColliderDetectioType interactionColliderDetectioType, string[] interactionTags, BaseInteractionObject interactionObject)
    {        
        _interactionObject = interactionObject;
        Init(interactionTags, interactionColliderDetectioType);
    }
}
