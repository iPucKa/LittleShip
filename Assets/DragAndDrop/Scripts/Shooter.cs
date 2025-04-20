using UnityEngine;

public class Shooter : MonoBehaviour
{
	[SerializeField] private ParticleSystem _explosion;

	private const int RightMouseButton = 1;
	private Camera _camera;

	private void Awake()
	{
		_camera = Camera.main;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(RightMouseButton))		
			Shoot();		
	}

	private void Shoot()
	{
		Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			ParticleSystem explosion = Instantiate(_explosion);

			ExplosionEffect effect = new ExplosionEffect(5, explosion);

			effect.Execute(hit.point);
		}
	}	
}
