using System.Collections.Generic;
using UnityEngine;

public class InteractibleCannon : MonoBehaviour, IInteractable
{
    private HashSet<RagdollCharacter> _ragdollCharacters = new HashSet<RagdollCharacter>();

    [SerializeField]
    private InteractionColliderDetectioType _interactionColliderDetectioType;

    [SerializeField]
    private string[] _interactionTags;

    [SerializeField]
    private CollisionListener _collisitonListener;

    private void Awake()
    {
        if (_interactionTags == null || _interactionTags.Length == 0)
        {
            Debug.LogError("Interaction Tags requires at least one tag", gameObject);
        }
        if (_collisitonListener != null)
        {
            _collisitonListener.Init(_interactionTags, _interactionColliderDetectioType);
            _collisitonListener.OnObjectEnter += AddRagdoll;
        }
        else
        {
            Debug.LogError("InteractibleCannon Requires one collisitonListener", gameObject);
        }
    }

    public void CancelInteraction()
    {

    }
    public void AddRagdoll(GameObject gameObject)
    {
        
        var rag = gameObject.GetComponentInParent<RagdollCharacter>();

        if (rag != null)
        {            
            _ragdollCharacters.Add(rag);
        }
        else
        {
            Debug.LogWarning("Could not find RagdollCharacter component at any parent of", gameObject);

        }
    }

    public void Interact()
    {
        print("Interacted");
        foreach (var item in _ragdollCharacters)
        {
            print("Kabum");
        }
        _ragdollCharacters.Clear();
    }

}
