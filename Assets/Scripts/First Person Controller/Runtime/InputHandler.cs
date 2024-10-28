using Outsiders.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Outsiders.FPController.Core
{
	[DefaultExecutionOrder(-2)]

	public class InputHandler : SingletonPersistent<InputHandler>
	{
		public PlayerInput PlayerInputComponent;

		public delegate void OnActionEvent();

		public delegate void OnVector2Event(Vector2 dir);

		public delegate void OnIntEvent(int value);

		public delegate void OnBoolEvent(bool state);

		public delegate void OnFloatEvent(int value);

		public OnVector2Event OnMovement;
		public OnVector2Event OnLook;
		public OnBoolEvent OnSprintButton;
		public OnActionEvent OnPauseEvent;

		[SerializeField] private InputMaps _currentMap;
		public Vector2 MoveInput;
		public Vector2 LookInput;

		private void Awake()
		{
			base.Awake();
			PlayerInputComponent = GetComponent<PlayerInput>();
		}

		public void ChangeInputMap(InputMaps targetMap)
		{
			PlayerInputComponent.SwitchCurrentActionMap(targetMap.ToString());
			Debug.Log($"Input map switched to {PlayerInputComponent.currentActionMap}");
			_currentMap = targetMap;
		}

		public void GetMovementInput(InputAction.CallbackContext context)
		{
			MoveInput = context.ReadValue<Vector2>();
		}

		public void GetLookInput(InputAction.CallbackContext context)
		{
			LookInput = context.ReadValue<Vector2>();
		}

		public void OnSprintAction(InputAction.CallbackContext context)
		{
			if (context.performed)
				OnSprintButton?.Invoke(true);
			else if (context.canceled)
				OnSprintButton?.Invoke(false);
		}

		public void OnEnterPauseUI(InputAction.CallbackContext context)
		{
			if (context.performed)
				OnPauseEvent?.Invoke();
		}

		public enum InputMaps
		{
			Movement,
			Interaction,
			UI
		}
	}
}