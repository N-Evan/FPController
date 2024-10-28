using UnityEngine;

namespace Outsiders.FPController.Core
{
	public class LocomotionController : MonoBehaviour
	{
		private CharacterController _charController;

		[field: SerializeField] private bool _isSprinting;
		[SerializeField] private float _walkSpeed = 3.0f;
		[SerializeField] private float _walkAccelaration;
		[SerializeField] private float _sprintSpeed;
		[SerializeField] private float _sprintAccelaration;
		public float Drag = 0.1f;
		private Vector3 _velocity;
		private float _verticalVelocity = 0f;

		[SerializeField] private float _gravity;

		private float _mouseSensitivity = 2.0f;
		private float _clampRange = 80f;
		[SerializeField] private LayerMask _groundLayers;

		private void Awake()
		{
			_charController = GetComponent<CharacterController>();
		}

		public void RegisterSprintButton(bool state)
		{
			_isSprinting = state;
		}

		public void HandleGravity()
		{
			var isGrounded = IsGrounded();
			if (isGrounded && _verticalVelocity < 0f)
			{
				_verticalVelocity = 0f;
			}
			_verticalVelocity -= _gravity * Time.deltaTime;
			
		}

		private bool IsGrounded()
		{
			bool isGrounded = IsGroundedWhileGrounded() || IsGroundedWhileAirborne();
			return isGrounded;
		}

		public void HandleMovement(Vector3 moveDir)
		{
			float accelaration = _isSprinting ? _sprintAccelaration : _walkAccelaration;
			float clampMagnitutde = _isSprinting ? _sprintSpeed : _walkSpeed;

			Vector3 movementDelta = moveDir * accelaration * Time.deltaTime;
			Vector3 newVelocity = _charController.velocity + movementDelta;
			Vector3 currentDrag = newVelocity.normalized * Drag * Time.deltaTime;

			newVelocity = (newVelocity.magnitude > Drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
			newVelocity = Vector3.ClampMagnitude(newVelocity, clampMagnitutde);
			newVelocity.y += _verticalVelocity;
			_charController.Move(newVelocity * Time.deltaTime);
		}

		private bool IsGroundedWhileGrounded()
		{
			Vector3 overlapPos = new Vector3(transform.position.x, transform.position.y - _charController.radius,
				transform.position.z);
			bool grounded = Physics.CheckSphere(overlapPos, _charController.radius, _groundLayers,
				QueryTriggerInteraction.Ignore);
			return grounded;
		}

		private bool IsGroundedWhileAirborne()
		{
			return _charController.isGrounded;
		}
	}
}