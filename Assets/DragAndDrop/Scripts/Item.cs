using UnityEngine;

public class Item : MonoBehaviour, IGrabable, IDamageable
{
	private const float UpForce = 8;
	private const float SideForce = 3;

	private Rigidbody _rigidbody;
	private Collider _collider;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
	}

	public void OnGrab()
	{
		transform.localScale *= 1.2f;
		_collider.enabled = false;
		_rigidbody.isKinematic = true;
	}

	public void OnRelease()
	{
		transform.localScale /= 1.2f;
		_collider.enabled = true;
		_rigidbody.isKinematic = false;
	}

	public void OnSwiping(Vector3 point)
	{
		transform.position = point;
	}

	public void TakeDamage(Vector3 point)
	{
		Vector3 direction = (_rigidbody.position - point).normalized;

		_rigidbody.AddForce(Vector3.up * UpForce, ForceMode.VelocityChange);
		_rigidbody.AddForce(direction * SideForce, ForceMode.VelocityChange);
	}
}
