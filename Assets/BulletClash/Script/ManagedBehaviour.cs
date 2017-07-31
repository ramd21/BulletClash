using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using RM;

namespace RM
{
	public class ManagedBehaviour : RMBehaviour 
	{
		public virtual void ManagedUpdate()
		{

		}

		public virtual void ManagedLateUpdate()
		{

		}

		protected virtual void OnEnable()
		{
			ManagedUpdateMan.i._ManagedList.Add(this);
		}

		protected virtual void OnDisable()
		{
			ManagedUpdateMan.i._RemoveList.Add(this);
		}

		protected virtual void OnDestroy()
		{
			ManagedUpdateMan.i._ManagedList.Remove(this);
		}
	}

#if UNITY_EDITOR
#endif
}