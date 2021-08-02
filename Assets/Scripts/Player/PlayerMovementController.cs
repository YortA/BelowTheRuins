using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private PlayerInputsActions playerInputActions;
    private InputAction movement;
    private Vector2 moveVector;


    public Rigidbody2D rb2d;
    public Animator animator;

    private float speed = 5f;       // serialize later



    private void Awake()
	{
        playerInputActions = new PlayerInputsActions();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

	private void OnEnable()
	{
        // map our movement to our playerinputactions object
        movement = playerInputActions.Player.Movement;
        movement.Enable();
    }

	private void OnDisable()
	{
        movement.Disable(); // ends our event
	}

	// Update is called once per frame
	void Update()
    {

    }

    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
    // this is designed to execute physics related executions, we want to call this function so
    // that our physics-related functions don't break or don't work how they're supposed to.
    private void FixedUpdate()
	{
        moveVector = movement.ReadValue<Vector2>();
        moveVector = rb2d.velocity = moveVector * speed;
        //Debug.Log("Movement Value: " + movement.ReadValue<Vector2>());
	}


}
