using TMPro;
using UnityEngine;

public class ShipEngine : MonoBehaviour
{
	[SerializeField] private float _rotationSpeed;
	[SerializeField] private float _maxSpeed;
	
	[SerializeField] private TMP_Text _speedText;

	[SerializeField] private Transform _sail;
	[SerializeField] private Wind _wind;

	private const KeyCode RotateSailLeftKey = KeyCode.A;
	private const KeyCode RotateSailRightKey = KeyCode.S;
	private const KeyCode RotateShipLeftKey = KeyCode.Q;
	private const KeyCode RotateShipRightKey = KeyCode.W;

	private Rigidbody _movable;
	private Mover _mover;
	private Rotator _shipRotator;
	private Rotator _sailRotator;

	private const int LeftSide = -1;
	private const int RightSide = 1;
	
	private const float MaxSailRotationAngle = 90;

	public Vector3 Position => _movable.position;
	public Transform BodyOrientation => transform;
	public Transform SailOrientation => _sail;

	private void Awake()
	{
		_movable = GetComponent<Rigidbody>();

		_mover = GetComponent<Mover>();
		_shipRotator = GetComponent<Rotator>();
		_sailRotator = _sail.GetComponent<Rotator>();

		_movable.maxLinearVelocity = _maxSpeed;
	}

	private void Update()
	{
		if (Input.GetKey(RotateShipLeftKey))		
			_shipRotator.RotateTo(LeftSide, _rotationSpeed);

		if (Input.GetKey(RotateShipRightKey))
			_shipRotator.RotateTo(RightSide, _rotationSpeed);

		if (Input.GetKey(RotateSailLeftKey))		
			_sailRotator.RotateTo(LeftSide, _rotationSpeed, MaxSailRotationAngle);		

		if (Input.GetKey(RotateSailRightKey))		
			_sailRotator.RotateTo(RightSide, _rotationSpeed, MaxSailRotationAngle);				
	}

	private void FixedUpdate()
	{		
		if (CanSail() == false)
		{
			Debug.Log("Надо что-то делать. Ветер дует не туда");
			return;
		}

		float sailForce = WindToSailForce();

		_mover.MoveTo(MoveSpeed(sailForce));
	}

	private bool CanSail()
	{
		Vector3 windDirection = _wind.CurrentWindDirection;

		float dotProduct = Vector3.Dot(_sail.forward, windDirection);

		float cos = dotProduct / (windDirection.magnitude * _sail.forward.magnitude);

		if (cos <= 0)
		{
			ShowShipSpeed(0);
			return false;
		}

		return true;
	}

	private float WindToSailForce()
	{
		Vector3 windDirection = _wind.CurrentWindDirection;

		return Vector3.Dot(_sail.forward, windDirection);
	}

	private float MoveSpeed(float force)
	{
		float speed = Vector3.Dot(transform.forward, _sail.forward) * force * _maxSpeed;

		ShowShipSpeed(speed);

		return speed;
	}

	private void ShowShipSpeed(float speed)
	{
		_speedText.text = "Скорость корабля: " + speed.ToString("0.0") + " км/ч";
	}
}
