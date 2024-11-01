using Outsiders.Utility;
using UnityEngine;

namespace Outsiders.FPController.Core
{
	public class PlayerStateMaster : SingletonPersistent<PlayerStateMaster>
	{
		[field: SerializeField] public PlayerStates CurrentPlayerState { get; private set; } = PlayerStates.Disabled;

		public void SetPlayerState(PlayerStates newState)
		{
			CurrentPlayerState = newState;
		}

		public enum PlayerStates
		{
			Idle = 0,
			Walking = 1,
			Interacting = 2,
			Disabled = 3
		}
	}
}