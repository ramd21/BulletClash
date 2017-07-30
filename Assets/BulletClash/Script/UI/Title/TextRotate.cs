using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	public class TextRotate : RMBehaviour
	{
		void OnEnable()
		{
			float rot = 0;

			this.DoUntil(() =>
			{
				return rot == 360;
			},
			() =>
			{
				transform.SetLocalEulerAnglesY(rot);
				rot += 10;
			});
		}
	}
}


