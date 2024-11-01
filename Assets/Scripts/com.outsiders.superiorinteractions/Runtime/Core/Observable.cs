using UnityEngine;

namespace Outsiders.SuperiorInteraction.Core
{
	public class Observable : MonoBehaviour
	{
		public EquipmentDetails EquipmentContainer;

		private void OnEnable()
		{
			gameObject.layer = LayerMask.NameToLayer("Interactable");
		}

		public string GetPopupData()
		{
			return EquipmentContainer.PopupData;
		}
	}
}
