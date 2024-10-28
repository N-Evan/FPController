using Unity.Cinemachine;
using UnityEngine;

namespace Outsiders.SuperiorInteraction.Core
{
	public class CameraController : MonoBehaviour
	{
		public CinemachineCamera CinemachineCamera;

		private Vector3 _originalPosition;
		private Quaternion _originalRotation;
		private Transform _cameraTransform;

		private void Awake()
		{
			_cameraTransform = CinemachineCamera.transform;
			_originalPosition = _cameraTransform.position;
			_originalRotation = _cameraTransform.rotation;
		}

		public void MoveToPosition(Vector3 targetPosition, Quaternion targetRotation)
		{
			_cameraTransform.position = targetPosition;
			_cameraTransform.rotation = targetRotation;
		}

		public void ReturnToOriginalPosition()
		{
			_cameraTransform.position = _originalPosition;
			_cameraTransform.rotation = _originalRotation;
		}
	}
}