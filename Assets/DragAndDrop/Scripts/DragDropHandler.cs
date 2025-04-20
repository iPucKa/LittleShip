using UnityEngine;

public class DragDropHandler : MonoBehaviour
{
	private const int LeftMouseButton = 0;
	
	private Camera _camera;
	private Vector3 _previousPosition;
	private bool _isMoving;

	private Item _selectedItem;

	private void Awake()
	{
		_camera = Camera.main;
	}

	private void Update()
	{
		ProcessClickDown();
		ProcessClickUp();
		ProcessMove();
	}

	private void ProcessClickDown()
	{
		if (Input.GetMouseButtonDown(LeftMouseButton) == false)
			return;

		_isMoving = true;
		_previousPosition = Input.mousePosition;		

		if (Physics.Raycast(GetRay(_previousPosition), out RaycastHit hit))
			if (hit.collider.TryGetComponent(out Item item))
			{ 
				_selectedItem = item;
				_selectedItem.Drag();
			}
	}

	private void ProcessClickUp()
	{
		if (Input.GetMouseButtonUp(LeftMouseButton) == false)
			return;

		if(_selectedItem == null) 
			return;

		_selectedItem.Drop();

		_selectedItem = null;
		_isMoving = false;
	}
	private void ProcessMove()
	{
		if (_isMoving == false)
			return;

		if (_previousPosition == Input.mousePosition)
			return;

		if (_selectedItem == null)
			return;

		_previousPosition = Input.mousePosition;

		Ray touchRay = GetRay(_previousPosition);
		Plane plane = new Plane(Vector3.up, new Vector3(0,2,0));

		if (plane.Raycast(GetRay(_previousPosition), out float distanse))
		{
			Vector3 point = touchRay.GetPoint(distanse);
			_selectedItem.transform.position = point;
		}
	}

	private Ray GetRay(Vector3 position) => _camera.ScreenPointToRay(position);

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
