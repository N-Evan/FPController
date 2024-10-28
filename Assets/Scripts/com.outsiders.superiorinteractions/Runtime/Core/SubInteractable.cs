using UnityEngine;

namespace Outsiders.SuperiorInteraction.Core
{
	public class SubInteractable : MonoBehaviour
	{
		public void TriggerSubInteraction()
		{
			// Custom animation or flow logic for sub-interaction
			Debug.Log("Sub-interaction triggered on " + gameObject.name);
		}
	}
}