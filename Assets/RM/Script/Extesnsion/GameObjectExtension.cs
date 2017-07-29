using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace RM
{
	static public class GameObjectExtension
	{
		public static GameObject Instantiate(this GameObject aThis)
		{
			aThis = GameObject.Instantiate(aThis);
			return aThis;
		}

		public static T Instantiate<T>(this T aThis) where T : MonoBehaviour
		{
			aThis = GameObject.Instantiate<T>(aThis);
			return aThis;
		}

		public static void SetLayer(this GameObject aThis, string aLayer, bool aSetChild = true)
		{
			if (aThis == null)
			{
				return;
			}
			aThis.layer = LayerMask.NameToLayer(aLayer);

			//子に設定する必要がない場合はここで終了
			if (!aSetChild)
			{
				return;
			}

			//子のレイヤーにも設定する
			foreach (Transform childTransform in aThis.transform)
			{
				SetLayer(childTransform.gameObject, aLayer, aSetChild);
			}
		}


		public static void SetLayer(this GameObject aThis, int aLayer, bool aSetChild = true)
		{
			if (aThis == null)
			{
				return;
			}
			aThis.layer = aLayer;

			//子に設定する必要がない場合はここで終了
			if (!aSetChild)
			{
				return;
			}

			//子のレイヤーにも設定する
			foreach (Transform childTransform in aThis.transform)
			{
				SetLayer(childTransform.gameObject, aLayer, aSetChild);
			}
		}

		static public T GetOrAddComponent<T>(this GameObject aThis) where T : Component
		{
			T t = aThis.GetComponent<T>();
			if (!t)
				t = aThis.AddComponent<T>();
			return t;
		}

#if UNITY_EDITOR
		static public bool IsPrefab(this GameObject aThis)
		{
			PrefabType prefabType = PrefabUtility.GetPrefabType(aThis);
			return prefabType == PrefabType.PrefabInstance || prefabType == PrefabType.DisconnectedPrefabInstance;
		}

		static public void ApplyPrefab(this GameObject aThis)
		{
			PrefabType prefabType = PrefabUtility.GetPrefabType(aThis);
			if (prefabType == PrefabType.PrefabInstance || prefabType == PrefabType.DisconnectedPrefabInstance)
			{
				GameObject go = PrefabUtility.ReplacePrefab(aThis, PrefabUtility.GetPrefabParent(aThis));
				PrefabUtility.ConnectGameObjectToPrefab(aThis, go);
				Debug.Log(go.GetAssetPath());
				if (aThis.name != go.name)
				{
					Debug.Log("name change");
					AssetDatabase.RenameAsset(go.GetAssetPath(), aThis.name);
				}
			}
		}

		static public void CreatePrefab(this GameObject aThis, string aPath)
		{
			string path = aPath + "/" + aThis.name + ".prefab";
			Debug.Log(path);

			GameObject go = PrefabUtility.CreatePrefab(path, aThis);
			PrefabUtility.ConnectGameObjectToPrefab(aThis, go);

		}

		static public void LogAssetPath(this GameObject aThis)
		{
			Debug.Log(AssetDatabase.GetAssetPath(aThis));
		}
#endif
	}
}


