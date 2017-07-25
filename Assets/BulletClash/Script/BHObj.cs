using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;

namespace BC
{
	[RequireComponent(typeof(BCTra))]
	public abstract class BHObj : RMBehaviour
	{
		public int _Id;
		public ActiveState _State;
		public BCTra _Tra;

		protected virtual void Awake()
		{
			_Tra = GetComponent<BCTra>();
		}

		public virtual void ActivateReq()
		{
		}

		public virtual void DeactivateReq()
		{
		}

		public virtual void OnActivate()
		{
		}

		public virtual void OnDeactivate()
		{
		}

		public virtual void UpdateView()
		{
			transform.position = (_Tra._Pos + FieldMan.i._Offset).ToVector3XZ() / GameMan.cDistDiv;
		}
#if UNITY_EDITOR

#endif
	}
}


