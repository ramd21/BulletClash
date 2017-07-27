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

		public virtual void OnFrameBegin()
		{
		}

		public virtual void ActivateReq(Vector2Int aPos)
		{
			_State = ActiveState.activate_req;
			gameObject.SetActive(false);
			_Tra._Pos = aPos;
		}

		public virtual void DeactivateReq()
		{
			_State = ActiveState.deactivate_req;
		}

		public virtual void OnActivate()
		{
		}

		public virtual void OnDeactivate()
		{
		}

		public virtual void OnFrameEnd()
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


