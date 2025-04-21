using UnityEngine;

public interface IGrabable
{
	void OnGrab();

	void OnRelease();

	void OnSwiping(Vector3 point);
}
