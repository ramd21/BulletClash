using UnityEngine;
using RM;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct BulletParam
{
	public BulletType Type;
	public int Spd;
	public int Timer;

	static public BulletParam[] TSVToArr(string aTSV)
	{
		string[,] strMultiArr = aTSV.TSVToStrMultiArr();
		int len = strMultiArr.GetLength(0);
		BulletParam[] arr = new BulletParam[len - 2];

		for (int i = 0; i < len; i++)
		{
			if (i >= 2)
			{
				BulletParam dat = new BulletParam();
				dat.Type = strMultiArr[i, 0].ToEnum<BulletType>();
				dat.Spd = strMultiArr[i, 1].ToInt();
				dat.Timer = strMultiArr[i, 2].ToInt();
				arr[i - 2] = dat;
			}
		}
		return arr;
	}

#if UNITY_EDITOR
	static public void CreateMasterScriptableObj(string aTSV, string aPath)
	{
		BulletParam[] arr = TSVToArr(aTSV);
		BulletParamMaster mst = AssetDatabase.LoadAssetAtPath<BulletParamMaster>(aPath);

		if (!mst)
		{
			mst = ScriptableObject.CreateInstance<BulletParamMaster>();
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