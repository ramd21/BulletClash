using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class Unit : Chara
	{
		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, 1f);
		}
	}
}


