using UnityEngine;

namespace Outsiders.SuperiorInteraction.Core
{
	public class Interactable : MonoBehaviour
	{
		public Transform CameraPositionTransform;

		public Vector3 GetCameraPosition() => CameraPositionTransform.position;

		public Quaternion GetCameraRotation() => CameraPositionTransform.rotation;

		public string GetPopupData()
		{
			// Return some data about this interactable (e.g., name, description)
			return "Interactable Details: " + gameObject.name;
		}
	}
}