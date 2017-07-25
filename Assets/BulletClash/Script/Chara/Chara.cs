using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	[RequireComponent(typeof(BCTra))]
	public abstract class Chara : BHObj
	{
		public int _PlayerId;
		public int _VSPlayerId;
		public CharaType _Type;

#if UNITY_EDITOR
		
#endif
	}
}


