using System.Collections.Generic;

using UnityEngine;

public abstract class AbstractOnTriggerEnterAction : MonoBehaviour
{
    public bool doActionOnlyWhenEnabled = true;

    public float actionDelay = 0.0f;

    [SerializeField] private List<string> detectedTags;
    [SerializeField] private List<string> ignoredTags;

    /* TODO: Usar Invoke com passagem de parâmetro para não ocorrer problema de chamada do DoAction
     *       com o collider de um trigger posterior
     */ 
    private Collider otherCollider;

    void Awake()
    {
        if( detectedTags == null )
        {
            detectedTags = new List<string>();
        }
        else
        {
            detectedTags.Sort();
        }

        if( ignoredTags == null )
        {
            ignoredTags = new List<string>();
        }
        else
        {
            ignoredTags.Sort();
        }
    }

    void OnTriggerEnter( Collider other )
    {
        if( ignoredTags.Count > 0 && ( ignoredTags.BinarySearch( other.tag ) >= 0 ) )
        {
            return;
        }

        if( detectedTags.Count > 0 && ( detectedTags.BinarySearch( other.tag ) < 0 ) )
        {
            return;
        }

        otherCollider = other;

        if( doActionOnlyWhenEnabled )
        {
            if( enabled )
            {
                if( actionDelay > 0.0f )
                {
                    Invoke( "CallAction", actionDelay );
                }
                else
                {
                    DoAction( otherCollider );
                }
            }
        }
        else
        {
            if( actionDelay > 0.0f )
            {
                Invoke( "CallAction", actionDelay );
            }
            else
            {
                DoAction( otherCollider );
            }
        }
    }

    protected abstract void DoAction( Collider other );

    private void CallAction()
    {
        DoAction( otherCollider );
    }
}
