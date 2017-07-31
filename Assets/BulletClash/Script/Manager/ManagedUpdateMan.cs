using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using RM;

namespace RM
{
	public class ManagedUpdateMan : Singleton<ManagedUpdateMan> 
	{
		public List<ManagedBehaviour> _ManagedList = new List<ManagedBehaviour>();
		public List<ManagedBehaviour> _RemoveList = new List<ManagedBehaviour>();


		void Update()
		{
			for (int i = 0; i < _ManagedList.Count; i++)
			{
				if (_ManagedList[i])
					_ManagedList[i].ManagedUpdate();
			}
		}

		void LateUpdate()
		{
			int len;

			len = _ManagedList.Count;
			for (int i = 0; i < len; i++)
			{
				if (_ManagedList[i])
					_ManagedList[i].ManagedLateUpdate();
			}

			len = _RemoveList.Count;
			for (int i = 0; i < len; i++)
			{
				if (_RemoveList[i])
					_ManagedList.Remove(_RemoveList[i]);
			}

			_RemoveList.Clear();
		}
	}

#if UNITY_EDITOR
#endif
}