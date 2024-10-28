using Outsiders.Utility;
using UnityEngine;

namespace Outsiders.FPController.Core
{
	[DefaultExecutionOrder(-1)]

	public class PlayerController : SingletonPersistent<PlayerController>
	{
		public CharacterController CharacterController;
		public LocomotionController LocomotionController;
		public FPCamController FPCamController;
		private InputHandler _inputs;

		[SerializeField] private float _inputBlendSpeed = 4f;

		[SerializeField] private Transform _cameraHolder;
		private float _verticalRotation;
		public Vector3 CurrentMoveValue { get; private set; } = Vector3.zero;

		private void Awake()
		{
			base.Awake();
			FetchComponents();
		}

		private void OnEnable()
		{
			_inputs.OnSprintButton += LocomotionController.RegisterSprintButton;
		}

		private void OnDisable()
		{
			_inputs.OnSprintButton -= LocomotionController.RegisterSprintButton;
		}

		private void FetchComponents()
		{
			CharacterController = GetComponent<CharacterController>();
			_inputs = InputHandler.Instance;
		}

		private void Update()
		{
			CurrentMoveValue = GetLerpedMoveInput();
			var movementDirection = CalculateDirection();
			LocomotionController.HandleGravity();
			LocomotionController.HandleMovement(movementDirection);
		}

		private Vector3 GetLerpedMoveInput()
		{
			return Vector3.Lerp(CurrentMoveValue, _inputs.MoveInput, Time.deltaTime * _inputBlendSpeed);
		}

		private Vector3 CalculateDirection()
		{
			Vector3 cameraForwardXZ =
				new Vector3(_cameraHolder.transform.forward.x, 0f, _cameraHolder.transform.forward.z)
					.normalized;
			Vector3 cameraRightXZ =
				new Vector3(_cameraHolder.transform.right.x, 0f, _cameraHolder.transform.right.z).normalized;
			Vector3 movementDirection = cameraRightXZ * CurrentMoveValue.x + cameraForwardXZ * CurrentMoveValue.y;
			return movementDirection;
		}
	}
}
