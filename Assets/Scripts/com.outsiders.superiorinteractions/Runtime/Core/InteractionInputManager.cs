using UnityEngine.InputSystem;
using UnityEngine;

namespace Outsiders.SuperiorInteraction.Core
{
	public class InteractionInputManager
	{
		private InputActionMap defaultActionMap;
		private InputActionMap interactionMap;

		private InputAction clickAction;
		private InteractionManager interactionManager;
		private void Awake()
		{
			//var inputActions = new InputActions(); // InputActions is an auto-generated class from Unity's Input System
			//defaultActionMap = inputActions.Default;
			//interactionMap = inputActions.InteractionMap;

			//interactionManager = GetComponent<InteractionManager>();
		}

		private void OnEnable()
		{
			SwitchActionMap("DefaultMap");
			clickAction = defaultActionMap["Click"];
			clickAction.performed += OnClick;
		}

		public void SwitchActionMap(string mapName)
		{
			if (mapName == "InteractionMap")
			{
				interactionMap.Enable();
				defaultActionMap.Disable();
			}
			else
			{
				defaultActionMap.Enable();
				interactionMap.Disable();
			}
		}

		private void OnClick(InputAction.CallbackContext context)
		{
			Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				var interactable = hit.collider.GetComponent<Interactable>();
				if (interactable != null)
				{
					interactionManager.StartInteraction(interactable);
				}
			}
		}

		private void OnDisable()
		{
			clickAction.performed -= OnClick;
		}
	}
}
