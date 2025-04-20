using UnityEngine;

public class ShipView : MonoBehaviour
{
	[SerializeField] private Transform _sail;

	[SerializeField] private float _rotateStep;

	[SerializeField] private ShipEngine _engine;
	
	private void FixedUpdate()
	{
		transform.position = _engine.Position - Vector3.up * 0.5f;
		
		transform.rotation = Quaternion.RotateTowards(transform.rotation, _engine.BodyOrientation.rotation, _rotateStep * Time.deltaTime);

		_sail.rotation = Quaternion.RotateTowards(_sail.rotation, _engine.SailOrientation.rotation, _rotateStep * Time.deltaTime);
	}
}
