using Studio23.SS2.InteractionSystem.Abstract;
using Studio23.SS2.InteractionSystem.Core;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerController : MonoBehaviour
{
	public CharacterController CharacterController;
	public LocomotionController LocomotionController;
	public PlayerInteractionFinder InteractionFinder;
	public FPCamController FPCamController;
	private InputHandler _inputs;

	[SerializeField] private Camera _mainCamera;
	private float _verticalRotation;
	private Vector3 _currentMoveValue = Vector3.zero;


	private void Awake()
	{
		FetchComponents();
	}

	private void OnEnable()
	{
		_inputs.OnSprintButton += LocomotionController.RegisterSprintButton;
		//InteractionManager.Instance.OnInteractionChainStarted += HandleInteractionChainStart;
		//InteractionManager.Instance.OnInteractionChainEnded += HandleInteractionChainEnd;
	}

	private void OnDisable()
	{
		_inputs.OnSprintButton -= LocomotionController.RegisterSprintButton;
		//InteractionManager.Instance.OnInteractionChainStarted -= HandleInteractionChainStart;
		//InteractionManager.Instance.OnInteractionChainEnded -= HandleInteractionChainEnd;
	}

	private void HandleInteractionChainEnd(InteractableBase @base)
	{
	}

	private void HandleInteractionChainStart(InteractableBase @base)
	{
	}

	private void FetchComponents()
	{
		CharacterController = GetComponent<CharacterController>();
		_inputs = InputHandler.Instance;
		//InteractionFinder = GetComponent<PlayerInteractionFinder>();
	}

	private void LookForInteractables()
	{
		var interactables = InteractionFinder.FindInteractables();
		InteractionManager.Instance.ShowNewInteractables(interactables);
	}

	private void Update()
	{
		var movementDirection = CalculateDirection();
		LocomotionController.HandleMovement(movementDirection);
	}

	private Vector3 CalculateDirection()
	{
		Vector3 cameraForwardXZ = new Vector3(_mainCamera.transform.forward.x, 0f, _mainCamera.transform.forward.z)
			.normalized;
		Vector3 cameraRightXZ =
			new Vector3(_mainCamera.transform.right.x, 0f, _mainCamera.transform.right.z).normalized;
		Vector3 movementDirection = cameraRightXZ * _inputs.MoveInput.x + cameraForwardXZ * _inputs.MoveInput.y;
		return movementDirection;
	}
}
