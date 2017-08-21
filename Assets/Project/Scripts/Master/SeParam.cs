using UnityEngine;
using RM;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct SeParam
{
	public SeType type;
	public float vol;

	static public SeParam[] TSVToArr(string aTSV)
	{
		string[,] strMultiArr = aTSV.TSVToStrMultiArr();
		int len = strMultiArr.GetLength(0);
		SeParam[] arr = new SeParam[len - 2];

		for (int i = 0; i < len; i++)
		{
			if (i >= 2)
			{
				SeParam dat = new SeParam();
				dat.type = strMultiArr[i, 0].ToEnum<SeType>();
			dat.vol = strMultiArr[i, 1].ToFloat();
				arr[i - 2] = dat;
			}
		}
		return arr;
	}

#if UNITY_EDITOR
	static public void CreateMasterScriptableObj(string aTSV, string aPath)
	{
		SeParam[] arr = TSVToArr(aTSV);
		SeParamMaster mst = AssetDatabase.LoadAssetAtPath<SeParamMaster>(aPath);

		if (!mst)
		{
			mst = ScriptableObject.CreateInstance<SeParamMaster>();
			mst._DatArr = arr;
			AssetDatabase.CreateAsset(mst, aPath);
			AssetDatabase.Refresh();
		}
		else
		{
			mst._DatArr = arr;
			EditorUtility.SetDirty(mst);
			AssetDatabase.SaveAssets();
		}
	}
#endif
}