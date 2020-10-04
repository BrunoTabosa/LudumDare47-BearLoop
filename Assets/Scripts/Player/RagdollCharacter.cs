using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCharacter : MonoBehaviour
{
    public Rigidbody head;
    public Rigidbody root;

    //private void Start()
    //{
    //    AddForceOnRoot(Vector3.down * 20);
    //    transform.Translate(Vector3.up * 1);
    //}
    public void Update()
    {
        //FOR TESTS ONLY
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    AddForceOnRoot(Vector3.down * 20);
        //}

        if(root.transform.position.y <= -15)
        {
            Destroy(gameObject);
        }
    }

    public void AddForceOnRigidbody(Rigidbody rigidbody, Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
