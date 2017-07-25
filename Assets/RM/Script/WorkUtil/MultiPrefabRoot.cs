using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	public class MultiPrefabRoot : MonoBehaviour
	{
#if UNITY_EDITOR
		public Object _Folder;

		[Button("ApplyOrCreate")]
		public int _ApplyOrCreate;

		void ApplyOrCreate()
		{
			int len = transform.childCount;

			for (int i = 0; i < len; i++)
			{
				if (transform.GetChild(i).gameObject.IsPrefab())
				{
					transform.GetChild(i).gameObject.SetActive(true);
					transform.GetChild(i).gameObject.ApplyPrefab();
				}
				else
				{
					transform.GetChild(i).gameObject.SetActive(true);
					transform.GetChild(i).gameObject.CreatePrefab(_Folder.GetAssetPath());
				}
			}
		}
#endif
	}
}