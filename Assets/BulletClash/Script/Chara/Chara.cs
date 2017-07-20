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
		public Coll[] _CollArr;

		protected void UpdateCollReq()
		{
			for (int i = 0; i < _CollArr.Length; i++)
			{
				_CollArr[i]._Update = true;
			}
		}

		public abstract void UpdateView();
#if UNITY_EDITOR
		public override void EditorUpdate()
		{
			_CollArr = GetComponents<Coll>();
			_Tra = GetComponent<BCTra>();
		}
#endif
	}
}


