using UnityEngine;

public class Example : MonoBehaviour
{
	[SerializeField] private ParticleSystem _explosion;

	private const int LeftMouseButton = 0;
	private const int RightMouseButton = 1;

	private Shooter _shooter;
	private DragDropHandler _dragDropHandler;

	private Camera _camera;

	private void Awake()
	{
		_camera = Camera.main;

		ParticleSystem explosion = Instantiate(_explosion);

		Shooter shooter = new Shooter(explosion, _camera);
		DragDropHandler dragDropHandler = new DragDropHandler(_camera);

		_shooter = shooter;
		_dragDropHandler = dragDropHandler;
	}

	private void Update()
	{
		ProcessShoot();

		ProcessClickDown();
		ProcessClickUp();
		ProcessMove();
	}	

	private void ProcessShoot()
	{
		if (Input.GetMouseButtonDown(RightMouseButton) == false)
			return;
		
		_shooter.Shoot();
	}

	private void ProcessClickDown()
	{
		if (Input.GetMouseButtonDown(LeftMouseButton) == false)
			return;

		_dragDropHandler.Grab();
	}

	public void ProcessClickUp()
	{
		if (Input.GetMouseButtonUp(LeftMouseButton) == false)
			return;

		_dragDropHandler.Release();
	}

	private void ProcessMove()
	{
		if (Input.GetMouseButton(LeftMouseButton))
			_dragDropHandler.Swiping();
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.red;

			Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
			Gizmos.DrawSphere(mouseWorldPosition, 1);
			Gizmos.DrawRay(mouseWorldPosition, _camera.transform.forward * 100);
		}
	}
}
