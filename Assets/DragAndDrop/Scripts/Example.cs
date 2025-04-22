using UnityEngine;

public class Example : MonoBehaviour
{
	[SerializeField] private ParticleSystem _explosion;

	private const int LeftMouseButton = 0;
	private const int RightMouseButton = 1;

	private Shooter _shooter;
	private DragDropHandler _dragDropHandler;

	private Camera _camera;

	private Vector3 _position;

	private void Awake()
	{
		_camera = Camera.main;

		_shooter = new Shooter(Instantiate(_explosion));
		_dragDropHandler = new DragDropHandler(_camera);
	}

	private void Update()
	{
		_position = Input.mousePosition;

		ProcessShoot();

		ProcessClickDown();
		ProcessClickUp();
		ProcessMove();
	}

	private void ProcessShoot()
	{
		if (Input.GetMouseButtonDown(RightMouseButton) == false)
			return;

		Ray ray = _camera.ScreenPointToRay(_position);
		_shooter.Shoot(ray);
	}

	private void ProcessClickDown()
	{
		if (Input.GetMouseButtonDown(LeftMouseButton) == false)
			return;

		_dragDropHandler.Grab(_position);
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
			_dragDropHandler.Swiping(_position);
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.red;

			Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(_position);
			Gizmos.DrawSphere(mouseWorldPosition, 1);
			Gizmos.DrawRay(mouseWorldPosition, _camera.transform.forward * 100);
		}
	}
}
