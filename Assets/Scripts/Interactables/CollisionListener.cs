using UnityEngine;


public class CollisionListener : MonoBehaviour
{
    public delegate void OnObjectEnterEvent(GameObject gameObject);
    public OnObjectEnterEvent OnObjectEnter;

    public delegate void OnObjectLeaveEvent(GameObject gameObject);
    public OnObjectLeaveEvent OnObjectLeave;

    protected Collider _collider;
    protected string[] _interactionTags;

    protected InteractionColliderDetectioType _interactionColliderDetectioType;

    public virtual void Init(string[] interactionTags, InteractionColliderDetectioType interactionColliderDetectioType)
    {
        var col = GetComponent<Collider>();
        if (col != null)
        {
            _collider = col;
        }
        else
        {
            Debug.LogError("Could not find Collider Component at", gameObject);
        }
        _collider = col;
        _interactionTags = interactionTags;
        interactionColliderDetectioType = _interactionColliderDetectioType;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (HaveAnyDesiredTag(other.gameObject) && (_interactionColliderDetectioType == InteractionColliderDetectioType.OnTrigger || _interactionColliderDetectioType == InteractionColliderDetectioType.Both))
        {
            OnObjectEnter?.Invoke(other.gameObject);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (HaveAnyDesiredTag(other.gameObject) && (_interactionColliderDetectioType == InteractionColliderDetectioType.OnTrigger || _interactionColliderDetectioType == InteractionColliderDetectioType.Both))
        {
            OnObjectLeave?.Invoke(other.gameObject);
        }
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (HaveAnyDesiredTag(collision.gameObject) && (_interactionColliderDetectioType == InteractionColliderDetectioType.OnCollisition || _interactionColliderDetectioType == InteractionColliderDetectioType.Both))
        {
            OnObjectEnter?.Invoke(collision.gameObject);
        }

    }
    protected virtual void OnCollisionExit(Collision collision)
    {
        if (HaveAnyDesiredTag(collision.gameObject) && (_interactionColliderDetectioType == InteractionColliderDetectioType.OnCollisition || _interactionColliderDetectioType == InteractionColliderDetectioType.Both))
        {
            OnObjectLeave?.Invoke(collision.gameObject);
        }
    }
    protected bool HaveAnyDesiredTag(GameObject other)
    {
        bool state = false;
        foreach (var tag in _interactionTags)
        {
            if (other.CompareTag(tag))
            {
                state = true;
                break;
            }
        }
        return state;
    }
}