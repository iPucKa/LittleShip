using UnityEngine;

public class Shooter
{
	private readonly ParticleSystem _explosion;

	public Shooter(ParticleSystem explosion)
	{
		_explosion = explosion;
	}

	public void Shoot(Ray ray)
	{
		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			ExplosionEffect effect = new ExplosionEffect(5, _explosion);
			effect.Execute(hit.point);
		}
	}
}
