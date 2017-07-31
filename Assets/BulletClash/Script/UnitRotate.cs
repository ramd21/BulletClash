using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

public class UnitRotate : ManagedBehaviour
{
	public float _Spd;

	public Vector3 _LossyScale;

	public override void ManagedUpdate()
	{
		transform.AddLocalEulerAnglesY(_Spd);

		_LossyScale = transform.lossyScale;
	}
}
