using UnityEngine;

namespace Outsiders.SuperiorInteraction.Core
{
	public class PointOfInterest : MonoBehaviour
	{
		public Transform interestCameraPosition;
		public string details;

		public Vector3 GetInterestPosition() => interestCameraPosition.position;

		public string GetDetails() => details;
	}
}
