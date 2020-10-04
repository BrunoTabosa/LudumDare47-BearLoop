using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCharacter : MonoBehaviour
{
    public Rigidbody head;
    public Rigidbody root;
    [SerializeField]
    private Rigidbody _torso;

    public Rigidbody Torso { get => _torso; }

    private Rigidbody[] _rigidbodies;

    private void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();

    }
    public void EnableRagDoll(bool status)
    {
        foreach (var item in _rigidbodies)
        {
            item.isKinematic = !status;
        }
    }

    public void Update()
    {

        if (root.transform.position.y <= -15)
        {
            Destroy(gameObject);
        }
    }

    public void AddForceOnRigidbody(Rigidbody rigidbody, Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
