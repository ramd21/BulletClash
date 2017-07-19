using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

[ExecuteInEditMode]
public class UnitRotate : RMBehaviour
{
	public float _Spd;

	public Vector3 _LossyScale;

	void Update()
	{
		transform.AddLocalEulerAnglesY(_Spd);

		_LossyScale = transform.lossyScale;
	}
}
