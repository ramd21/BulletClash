using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using RM;

namespace RM
{
	public interface IManagedUpdate
	{
		void ManagedUpdate();
		void ManagedLateUpdate();
	}


	public class ManagedUpdateMan : Singleton<ManagedUpdateMan> 
	{
		public List<IManagedUpdate> _ManagedList = new List<IManagedUpdate>();
		public List<IManagedUpdate> _RemoveList = new List<IManagedUpdate>();


		void Update()
		{
			for (int i = 0; i < _ManagedList.Count; i++)
			{
				if (_ManagedList[i] != null)
					_ManagedList[i].ManagedUpdate();
			}
		}

		void LateUpdate()
		{
			int len;

			len = _ManagedList.Count;
			for (int i = 0; i < len; i++)
			{
				if (_ManagedList[i] != null)
					_ManagedList[i].ManagedLateUpdate();
			}

			len = _RemoveList.Count;
			for (int i = 0; i < len; i++)
			{
				if (_RemoveList[i] != null)
					_ManagedList.Remove(_RemoveList[i]);
			}

			_RemoveList.Clear();
		}
	}

#if UNITY_EDITOR
#endif
}