using UnityEngine;
using RM;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct UnitParam
{
	public UnitType Type;
	public string PrefabName;
	public int Hp;
	public int SpdX;
	public int SpdZ;
	public int Cost;

	static public UnitParam[] TSVToArr(string aTSV)
	{
		string[,] strMultiArr = aTSV.TSVToStrMultiArr();
		int len = strMultiArr.GetLength(0);
		UnitParam[] arr = new UnitParam[len - 2];

		for (int i = 0; i < len; i++)
		{
			if (i >= 2)
			{
				UnitParam dat = new UnitParam();
				dat.Type = strMultiArr[i, 0].ToEnum<UnitType>();
				dat.PrefabName = strMultiArr[i, 1];
				dat.Hp = strMultiArr[i, 2].ToInt();
				dat.SpdX = strMultiArr[i, 3].ToInt();
				dat.SpdZ = strMultiArr[i, 4].ToInt();
				dat.Cost = strMultiArr[i, 5].ToInt();
				arr[i - 2] = dat;
			}
		}
		return arr;
	}

#if UNITY_EDITOR
	static public void CreateMasterScriptableObj(string aTSV, string aPath)
	{
		UnitParam[] arr = TSVToArr(aTSV);
		UnitParamMaster mst = AssetDatabase.LoadAssetAtPath<UnitParamMaster>(aPath);

		if (!mst)
		{
			mst = ScriptableObject.CreateInstance<UnitParamMaster>();
			mst._DatArr = arr;
			AssetDatabase.CreateAsset(mst, aPath);
			AssetDatabase.Refresh();
		}
		else
		{
			mst._DatArr = arr;
			AssetDatabase.SaveAssets();
		}
	}
#endif
}