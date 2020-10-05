using System.Collections.Generic;
using UnityEngine;

public class InteractibleCannon : MonoBehaviour, IInteractable
{
    private RagdollCharacter _ragdollCharacter;

    [SerializeField]
    private InteractionColliderDetectioType _interactionColliderDetectioType;

    [SerializeField]
    private string[] _interactionTags;

    [SerializeField]
    private CollisionListener _collisitonListener;

    [SerializeField]
    private AudioSource audioSource_aim;

    [SerializeField]
    private AudioSource audioSource_shoot;

    [System.Serializable]
    private struct PhysicsProperties
    {
        [SerializeField]
        private ForceMode _forceMode;

        [SerializeField]
        private Vector3 _dir;

        [SerializeField]
        private float _force;

        public ForceMode ForceMode { get => _forceMode; }
        public Vector3 Dir { get => _dir; }
        public float Force { get => _force; }
    }
    [SerializeField]
    private PhysicsProperties _physicsProperties;

    [SerializeField]
    private Transform _ragDollAnchor;

    private Animator _animator;

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
        _animator = GetComponent<Animator>();
    }

    public void CancelInteraction()
    {

    }
    public void AddRagdoll(GameObject gameObject)
    {

        var rag = gameObject.GetComponentInParent<RagdollCharacter>();

        if (rag != null)
        {
            if (_ragdollCharacter != null && rag != _ragdollCharacter)
            {
                //Destroy(_ragdollCharacter.gameObject);
            }
            _ragdollCharacter = rag;
        }
        else
        {
            Debug.LogWarning("Could not find RagdollCharacter component at any parent of", gameObject);

        }
    }

    public void Interact()
    {
        audioSource_aim.Play();
        _animator.SetTrigger("OnActionPress");
        if (_ragdollCharacter)
        {
            _ragdollCharacter.EnableRagDoll(false);
        }
    }
    private void ThrowRagdoll()
    {
        if (_ragdollCharacter)
        {
            audioSource_shoot.Play();
            _ragdollCharacter.root.transform.position = _ragDollAnchor.position;
            _ragdollCharacter.EnableRagDoll(true);
            var torso = _ragdollCharacter.Torso;
            torso.AddForce(_physicsProperties.Force * _physicsProperties.Dir, _physicsProperties.ForceMode);
            
        }
    }

}
