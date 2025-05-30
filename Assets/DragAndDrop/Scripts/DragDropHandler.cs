using UnityEngine;

public class DragDropHandler
{
	private readonly Camera _camera;

	private IGrabable _selectedItem;

	private Vector3 _previousPosition;

	private bool _isMoving;

	public DragDropHandler(Camera camera)
	{
		_camera = camera;
	}

	public void Grab(Vector3 position)
	{
		_isMoving = true;

		_previousPosition = position;

		if (Physics.Raycast(GetRay(_previousPosition), out RaycastHit hit))
			if (hit.collider.TryGetComponent(out IGrabable grabable))
			{
				_selectedItem = grabable;
				_selectedItem.OnGrab();
			}
	}

	public void Release()
	{
		if (_selectedItem == null)
			return;

		_selectedItem.OnRelease();

		_isMoving = false;
		_selectedItem = null;
	}
	public void Swiping(Vector3 position)
	{
		if (_isMoving == false)
			return;

		if (_previousPosition == position)
			return;

		if (_selectedItem == null)
			return;

		_previousPosition = position;

		Ray touchRay = GetRay(_previousPosition);
		Plane plane = new Plane(Vector3.up, new Vector3(0, 2, 0));

		if (plane.Raycast(GetRay(_previousPosition), out float distanse))
		{
			Vector3 point = touchRay.GetPoint(distanse);
			_selectedItem.OnSwiping(point);
		}
	}

	private Ray GetRay(Vector3 position) => _camera.ScreenPointToRay(position);
}
