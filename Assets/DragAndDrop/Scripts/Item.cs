using UnityEngine;

public class Item : MonoBehaviour, IDamageable
{
	[SerializeField] private ParticleSystem _explodeEffect;
	public void Drag()
	{
		transform.localScale *= 1.2f;
		GetComponent<Collider>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
	}

	public void Drop()
	{
		transform.localScale /= 1.2f;
		GetComponent<Collider>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = false;
	}
}
