using UnityEngine;

public class Flag : MonoBehaviour
{
	[SerializeField] private Wind _wind;
	[SerializeField] private float _rotateStep;

	private void Update()
	{
		float yRotation = _wind.WindDirection.eulerAngles.y;
		//transform.localRotation = Quaternion.RotateTowards(transform.rotation, _wind.WindDirection, _rotateStep * Time.deltaTime);
		
		transform.rotation = Quaternion.Euler(transform.rotation.x, - yRotation, transform.rotation.z);
	}
}
