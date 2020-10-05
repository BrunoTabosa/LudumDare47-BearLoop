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

    private bool _isInteracting;

    public BaseInteractionObject InteractionObject { set => _interactionObject = value; }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInput();
    }



    void HandleInput()
    {
        if (state == PlayerState.Dead || _isInteracting == true) return;

        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Vertical")));

        horizontal = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime * 100;
        vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime * 100;

        transform.Rotate(new Vector3(0, horizontal, 0));

        characterController.SimpleMove(transform.TransformDirection(Vector3.forward) * vertical);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_interactionObject != null)
            {
                animator.SetTrigger("isInteracting");
                transform.LookAt(_interactionObject.transform);
            }
        }

    }

    private void SetInteract()
    {
        _isInteracting = !_isInteracting;
        if (_isInteracting == false)
        {
            _interactionObject.Interact();
        }
    }

    public void Die()
    {
        state = PlayerState.Dead;
        Destroy(gameObject);

    }

}
