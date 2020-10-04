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

        horizontal = Input.GetAxis("Horizontal") * rotateSpeed;
        vertical = Input.GetAxis("Vertical") * movementSpeed;

        transform.Rotate(new Vector3(0, horizontal, 0));

        characterController.SimpleMove(transform.TransformDirection(Vector3.forward) * vertical);
        
    }

    public void Die()
    {
        //characterController.enabled = false;
        state = PlayerState.Dead;
        //GameController.Instance.OnPlayerDeath();
    }

}
