namespace Outsiders.SuperiorInteraction.Core
{
	using UnityEngine;
	using Unity.Jobs;
	using Unity.Collections;
	using UnityEngine.InputSystem;

	public class InteractionManager : MonoBehaviour
	{
		public CameraController cameraController;
		public UIManager uiManager;
		public InteractionInputManager inputManager;

		private Interactable currentInteractable;
		private NativeArray<RaycastHit> raycastHits;

		private void Awake()
		{
			raycastHits = new NativeArray<RaycastHit>(1, Allocator.Persistent);
		}

		public void StartInteraction(Interactable interactable)
		{
			currentInteractable = interactable;

			// Move CinemachineCamera to target position and rotation
			cameraController.MoveToPosition(
				currentInteractable.GetCameraPosition(),
				currentInteractable.GetCameraRotation()
			);

			uiManager.ShowPopup(interactable.GetPopupData());
			inputManager.SwitchActionMap("InteractionMap");
		}


		public void EndInteraction()
		{
			uiManager.HidePopup();
			cameraController.ReturnToOriginalPosition();
			currentInteractable = null;
			inputManager.SwitchActionMap("DefaultMap");
		}

		private void OnDestroy()
		{
			if (raycastHits.IsCreated) raycastHits.Dispose();
		}
	}
}