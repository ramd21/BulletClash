using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class DragCamera : ManagedBehaviour
{
	public float _SlowDown;

	public bool _LockX;
	public bool _LockY;
	public bool _LockZ;

	Vector3 _Start;
	Vector3 _Cur;
	Vector3 _Delta;
	Vector3 _Hit;


	public override void ManagedUpdate()
	{
		Ray r = _camera.ScreenPointToRay(Input.mousePosition);

		RaycastHit rh;
		if (Physics.Raycast(r, out rh))
			_Hit = rh.point;

		if (Input.GetMouseButtonDown(0))
		{
			_Start = _Hit;
		}
		else
		{
			if (Input.GetMouseButton(0))
			{
				_Cur = _Hit;
				_Delta = _Cur - _Start;
			}
			else
			{
				_Delta = Vector3.MoveTowards(_Delta, Vector3.zero, _SlowDown);
			}
		}

		if (_LockX)
			_Delta.x = 0;

		if (_LockY)
			_Delta.y = 0;

		if (_LockZ)
			_Delta.z = 0;

		_camera.transform.position -= _Delta;
	}
}
}


