using UnityEngine;

public class Mover : MonoBehaviour
{
	private Rigidbody _body;

	private void Awake()
	{
		_body = GetComponent<Rigidbody>();
	}

	public void MoveTo(float moveSpeed) => _body.AddForce(transform.forward * moveSpeed, ForceMode.Acceleration);	
}
