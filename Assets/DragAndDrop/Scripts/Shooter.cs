using UnityEngine;

public class Shooter
{
	private readonly ParticleSystem _explosion;
	private readonly Camera _camera;

	public Shooter(ParticleSystem explosion, Camera camera)
	{
		_explosion = explosion;
		_camera = camera;
	}

	public void Shoot()
	{
		Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			ExplosionEffect effect = new ExplosionEffect(5, _explosion);
			effect.Execute(hit.point);
		}
	}
}
