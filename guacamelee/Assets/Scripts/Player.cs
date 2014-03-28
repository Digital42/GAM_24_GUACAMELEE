using UnityEngine;

using System.Collections; 

[RequireComponent(typeof(CharacterController))] 

public class Player : MonoBehaviour 
	
{ 
	public Transform GunPosition = null; 
	public GameObject playerProjectileObject;
	public float playerSpeed = 6.0f; 
	public float playerJumpSpeed = 8.0f; 
	public float gravity = 20.0f; 
	public float firingInterval = 0.5f;
	public bool facingRight = true;
	private bool isAlive = true;
	private Vector3 directionVector = Vector3.zero;
	CharacterController controller;
	public float YApexOffset = 5.0f;
	float YApex = 0.0f;
	bool flagHitApex = false;
	
	public enum e_ActionState {GROUNDED, WALLSLIDE};
	public e_ActionState currentState = e_ActionState.GROUNDED; 
	
	void Awake()
	{   
		controller = GetComponent<CharacterController>();
	}
	// Use this for initialization
	void Start () 
	{
		InitializePlayerObject();
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawRay(transform.position, transform.forward * 20);
	}
	
	void Update()
	{
		Debug.Log(controller.isGrounded);
		if (isAlive)
		{
			PlayerActions();
		}
	}

	void InitializePlayerObject()
	{
		transform.rotation = Quaternion.LookRotation(Vector3.right);
		facingRight = true;
	}
	
	void PlayerActions()
	{
		PlayerMove();
		PlayerJump();
		PlayerFire();
		Vector3 currentMovementOffset = new Vector3(directionVector.x, directionVector.y, 0);
		controller.Move(currentMovementOffset * Time.deltaTime);
	}

	void PlayerMove()
	{   
		float movementDirection = Input.GetAxis("Horizontal");
		{
			if(Mathf.Abs(movementDirection) > 0.1)
			{
				Debug.Log("Walking...");
			}
			else 
			{
				Debug.Log("Idle...");
			}
			directionVector = new Vector3(movementDirection * playerSpeed, 0, 0);
			if(Mathf.Abs(movementDirection) > 0.1f)
			{
				transform.rotation = Quaternion.LookRotation(directionVector);
			}
		}
	}
	void PlayerJump()
	{
		//apply gravity
		directionVector.y = controller.velocity.y - gravity * Time.deltaTime;
		float curY = transform.position.y;
		//are we jumping?
		if (Input.GetKey(KeyCode.Space) && controller.isGrounded )
		{
			directionVector.y = playerJumpSpeed * gravity * Time.deltaTime;
			YApex = curY + YApexOffset;
			flagHitApex = false;
		}
		
		else if (!flagHitApex && Input.GetKey(KeyCode.Space) && !controller.isGrounded )
		{
			directionVector.y = playerJumpSpeed * gravity * Time.deltaTime;
			if (curY >= YApex - 0.1f)
				flagHitApex = true;
		}
		
		else if (!controller.isGrounded  && Input.GetKey(KeyCode.Space))
		{
			flagHitApex = true;
		}
	}
	void PlayerFire()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Instantiate(playerProjectileObject, GunPosition.position, transform.localRotation);
		}
	}
}