using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInteractionObject : MonoBehaviour
{
    [Header("Base Class Attributes")]
    [SerializeField]
    private GameObject[] _interactibleGameobjects;

    [Header("Interaction Info")]
    [SerializeField]
    private InteractionColliderComponent _interactionColliderComponent;

    [SerializeField]
    private InteractionColliderDetectioType _interactionColliderDetectioType;
    [SerializeField]
    private string[] _interactionTags;

    protected IInteractable[] _interactibleObjs;

    private Player _player;

    [SerializeField]
    private UnityEvent _onPlayerAllowedToInteract;
    [SerializeField]
    private UnityEvent _onPlayerNotAllowedToInteract;
    [SerializeField]
    private UnityEvent _onPlayerInteract;
    [SerializeField]
    private UnityEvent _onPlayerCancelInteract;

    public UnityEvent OnPlayerAllowedToInteract { get => _onPlayerAllowedToInteract; }
    public UnityEvent OnPlayerNotAllowedToInteract { get => _onPlayerNotAllowedToInteract; }
    public UnityEvent OnPlayerInteract { get => _onPlayerInteract; }
    public UnityEvent OnPlayerCancelInteract { get => _onPlayerCancelInteract; }

    protected virtual void Init()
    {
        _interactibleObjs = new IInteractable[_interactibleGameobjects.Length];
        for (int i = 0; i < _interactibleGameobjects.Length; i++)
        {
            var iinter = _interactibleGameobjects[i].GetComponent<IInteractable>();
            if (iinter != null)
            {
                _interactibleObjs[i] = iinter;
            }
            else
            {
                Debug.LogError("There is no IInteractable component at", _interactibleGameobjects[i]);
            }
        }

        if (_interactionColliderComponent != null)
        {
            if (_interactionTags == null || _interactionTags.Length == 0)
            {
                Debug.LogError("Interaction Tags requires at least one tag", gameObject);
            }
            _interactionColliderComponent.InteractionColliderInit(_interactionColliderDetectioType, _interactionTags, this);

            _interactionColliderComponent.OnObjectEnter += OnPlayerReachsInteractionZone;
            _interactionColliderComponent.OnObjectLeave += delegate { OnPlayerLeavesInteractionZone(); };
            GameController.Instance.OnPlayerDies += delegate { OnPlayerLeavesInteractionZone(); };

            _onPlayerAllowedToInteract.AddListener(ShowInteraction);
            OnPlayerNotAllowedToInteract.AddListener(DisableInteraction);

        }
    }

    protected void OnPlayerReachsInteractionZone(GameObject gameObject)
    {
        // Use inference to avoid use GetComponent ?
        //GameController.Instance.currentPlayer = _player;

        _player = gameObject.GetComponent<Player>();
        _player.InteractionObject = this;
        ShowInteraction();
    }
    protected void OnPlayerLeavesInteractionZone()
    {
        if (_player)
        {
            _player.InteractionObject = null;
            _player = null;
            DisableInteraction();
        }
    }
    public virtual void Interact()
    {
        foreach (var item in _interactibleObjs)
        {
            item.Interact();
        }
        OnPlayerInteract?.Invoke();
        
    }
    public virtual void CancelInteract()
    {
        foreach (var item in _interactibleObjs)
        {
            item.CancelInteraction();
        }
        OnPlayerCancelInteract?.Invoke();
    }
    protected virtual void ShowInteraction()
    {
        Debug.Log("CAN INTERACT", gameObject);
    }
    protected virtual void DisableInteraction()
    {
        Debug.Log("CANNOT INTERACT", gameObject);
    }
}

public enum InteractionColliderDetectioType
{
    OnTrigger,
    OnCollisition,
    Both
}