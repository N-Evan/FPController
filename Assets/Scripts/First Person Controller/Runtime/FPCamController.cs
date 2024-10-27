using UnityEngine;

public class FPCamController : MonoBehaviour
{
	[SerializeField] private Camera _mainCamera;

	public float LookSensH = 0.1f;
	public float LookSensV = 0.1f;
	public float LookLimitV = 89f;

	private Vector2 _cameraRotation = Vector2.zero;
	private Vector2 _playerTargetRotation = Vector2.zero;
	private Vector2 _lookInputValue = Vector2.zero;

	private void LateUpdate()
	{
		_lookInputValue = InputHandler.Instance.LookInput;
		_cameraRotation.x += LookSensH * _lookInputValue.x;
		_cameraRotation.y = Mathf.Clamp(_cameraRotation.y - LookSensV * _lookInputValue.y, -LookLimitV, LookLimitV);

		_playerTargetRotation.x += transform.eulerAngles.x + LookSensH * _lookInputValue.x;
		transform.rotation = Quaternion.Euler(0f, _playerTargetRotation.x, 0f);

		_mainCamera.transform.rotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0f);
	}
}
