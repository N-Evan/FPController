using Outsiders.FPController.Core;
using Outsiders.SuperiorInteraction.Core;
using Unity.Cinemachine;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerInteractor : MonoBehaviour
{
	public CinemachineCamera CinemachineCam;
	public LayerMask InteractionLayer;

	private NativeArray<RaycastHit> _rastcastHits;
	private JobHandle _raycastHandle;
	private bool _isInteracting;
	private float _interactionDistance;

	private void OnEnable()
	{
		InputHandler.Instance.OnInteractionEvent += RegisterInteractionInput;
	}

	private void OnDisable()
	{
		InputHandler.Instance.OnInteractionEvent -= RegisterInteractionInput;
	}

	private void Awake()
	{
		_rastcastHits = new NativeArray<RaycastHit>(1, Allocator.Persistent);
	}

	private void Update()
	{
		if (_isInteracting)
			return;
		var queryParams = new QueryParameters
		{
			layerMask = InteractionLayer,
			hitBackfaces = false,
			hitMultipleFaces = false,
			hitTriggers = QueryTriggerInteraction.UseGlobal
		};
		RaycastCommand rayCommand = new RaycastCommand(Camera.main.transform.position, Camera.main.transform.forward, queryParams, _interactionDistance);
	}

	public void RegisterInteractionInput()
	{
		if (_raycastHandle.IsCompleted)
		{
			_raycastHandle.Complete();
			if (_rastcastHits[0].collider != null)
			{
				Observable targetObservable = _rastcastHits[0].collider.GetComponent<Observable>();
				if (targetObservable != null)
				{
					BeginInteraction(targetObservable);
				}
			}
		}
	}

	private void BeginInteraction(Observable targetObservable)
	{
		_isInteracting = true;
		PlayerStateMaster.Instance.SetPlayerState(PlayerStateMaster.PlayerStates.Interacting);

		CinemachineCam.transform.position = targetObservable.transform.position;
		CinemachineCam.transform.LookAt(targetObservable.transform);

		//Show UI or Popup data
		// UIManager.ShowPopup(targetObservable.GetPopupData());
	}

	private void OnDestroy()
	{
		if (_rastcastHits.IsCreated)
			_rastcastHits.Dispose();
	}
}