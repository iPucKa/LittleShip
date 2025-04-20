using UnityEngine;

public class Wind : MonoBehaviour
{
	[SerializeField] private float _windDirection;

	public Quaternion WindDirection => transform.rotation;

	public Vector3 CurrentWindDirection => transform.forward;

	private void Awake()
	{
		SetWindDirection();
	}
	private void Update()
	{
		SetWindDirection();
	}

	private void SetWindDirection()
	{
		transform.rotation = Quaternion.Euler(0, _windDirection, 0);
	}
}
