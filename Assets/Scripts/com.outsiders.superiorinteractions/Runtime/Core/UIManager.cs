using UnityEngine;

namespace Outsiders.SuperiorInteraction.Core
{
	public class UIManager : MonoBehaviour
	{
		public GameObject popupUI;

		public void ShowPopup(string data)
		{
			popupUI.SetActive(true);
			// Update UI with data
		}

		public void HidePopup()
		{
			popupUI.SetActive(false);
		}

		public void UpdatePopup(string newData)
		{
			// Update existing UI popup with new data
		}
	}
}
