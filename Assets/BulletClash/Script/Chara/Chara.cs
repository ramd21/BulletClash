using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	[RequireComponent(typeof(BCTra))]
	public abstract class Chara : EditorUpdateBehaviour
	{
		public int _PlayerId;
		public ActiveState _State;
		public BCTra _Tra;

		public abstract void UpdateView();
#if UNITY_EDITOR
		
#endif
	}
}


