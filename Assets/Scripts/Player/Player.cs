using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;
    public PlayerState state;

    [SerializeField]
    private CharacterController characterController;

    private Animator animator;

    private float horizontal, vertical;
    [SerializeField]
    private BaseInteractionObject _interactionObject;

    public BaseInteractionObject InteractionObject { set => _interactionObject = value; }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }



    void HandleInput()
    {
        if (state == PlayerState.Dead) return;

        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Vertical")));

        horizontal = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime * 100;
        vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime * 100;

        transform.Rotate(new Vector3(0, horizontal, 0));

        characterController.SimpleMove(transform.TransformDirection(Vector3.forward) * vertical);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_interactionObject != null)
            {
                _interactionObject.Interact();
            }
        }

    }

    public void Die()
    {
        state = PlayerState.Dead;
        Destroy(gameObject);

    }

}
