using UnityEngine;

public class ExplosionEffect : IShootEffect
{
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
				damageable.TakeDamage(point);
		}
	}
}
