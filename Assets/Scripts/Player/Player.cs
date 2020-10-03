using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public PlayerState state;
    
    [SerializeField]
    private CharacterController characterController;

    private float horizontal, vertical;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    

    void HandleInput()
    {
        if (state == PlayerState.Dead) return;

        horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        vertical = Input.GetAxis("Vertical") * movementSpeed;

        characterController.SimpleMove(new Vector3(horizontal, 0, vertical));
    }

    public void Die()
    {
        //characterController.enabled = false;
        state = PlayerState.Dead;
    }
}
