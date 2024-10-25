using UnityEngine;

public class LocomotionController : MonoBehaviour
{
	private CharacterController _charController;
	
	private bool _isSprinting;
	private float _walkSpeed = 3.0f;
	private float _sprintMultiplier = 2.0f;
	private float _runAcceleration = 50f;
	public float Drag = 0.1f;
	private Vector3 _velocity;

	private float _mouseSensitivity = 2.0f;
	private float _clampRange = 80f;
	private float _runSpeed = 4f;

	private void Awake()
	{
		_charController = GetComponent<CharacterController>();
	}

	public void RegisterSprintButton(bool state)
	{
		
	}

	public void HandleMovement(Vector3 moveDir)
	{
		Vector3 movementDelta = moveDir * _runAcceleration * Time.deltaTime;
		Vector3 newVelocity = _charController.velocity + movementDelta;
		Vector3 currentDrag = newVelocity.normalized * Drag * Time.deltaTime;

		newVelocity = (newVelocity.magnitude > Drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
		newVelocity = Vector3.ClampMagnitude(newVelocity, _runSpeed);
		_charController.Move(newVelocity * Time.deltaTime);
	}
}
