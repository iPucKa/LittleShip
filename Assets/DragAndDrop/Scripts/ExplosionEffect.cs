using UnityEngine;

public class ExplosionEffect : IShootEffect
{
	private const float UpForce = 8;
	private const float SideForce = 3;

	private readonly float _radius;
	private readonly ParticleSystem _explosionEffect;

	public ExplosionEffect(float radius, ParticleSystem explosionEffect)
	{
		_radius = radius;
		_explosionEffect = explosionEffect;
	}

	public void Execute(Vector3 point)
	{		
		_explosionEffect.transform.position = point;
		_explosionEffect.Play();

		Collider[] targets = Physics.OverlapSphere(point, _radius);

		foreach (Collider target in targets)
		{
			if (target.TryGetComponent(out IDamageable damageable))
			{
				if (target.TryGetComponent(out Rigidbody rigidbody))
				{
					Vector3 direction = (rigidbody.position - point).normalized;

					rigidbody.AddForce(Vector3.up * UpForce, ForceMode.VelocityChange);
					rigidbody.AddForce(direction * SideForce, ForceMode.VelocityChange);
				}
			}
		}
	}
}
