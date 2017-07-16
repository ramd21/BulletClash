using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

public class UnitRotate : EditorUpdateBehaviour
{
	public float _Spd;

	public override void EditorUpdate()
	{
		transform.AddLocalEulerAnglesY(_Spd);
	}
}
