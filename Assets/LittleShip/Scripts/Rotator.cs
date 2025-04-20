using UnityEngine;

public class Rotator : MonoBehaviour
{
	public void RotateTo(float side, float rotationSpeed)
	{
		transform.Rotate(Vector3.up * side * rotationSpeed * Time.deltaTime, Space.Self);
	}

	public void RotateTo(float side, float rotationSpeed, float maxAngle)
	{
		transform.Rotate(Vector3.up * side * rotationSpeed * Time.deltaTime, Space.Self);

		if (Mathf.Abs(transform.localRotation.y) >= Quaternion.Euler(0, maxAngle, 0).y)
			transform.localRotation = Quaternion.Euler(0, maxAngle * side, 0);
	}
}
