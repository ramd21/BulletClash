using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	[RequireComponent(typeof(BCTra))]
	public abstract class Chara : EditorUpdateBehaviour
	{
		public int _Id;
		public int _PlayerId;
		public int _VSPlayerId;
		public ActiveState _State;
		public BCTra _Tra;
		public CharaType _Type;

		public virtual void UpdateView()
		{
			transform.position = (_Tra._Pos + FieldMan.i._Offset).ToVector3XZ() / GameMan.cDistDiv;
		}
#if UNITY_EDITOR
		
#endif
	}
}


