using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;
using System;
using System.Linq;

//dat.Type = strMultiArr[i, 0].ToEnum<UnitType>();
//dat.PrefabName = strMultiArr[i, 1];
//dat.Hp = strMultiArr[i, 2].ToInt();
//dat.SpdX = strMultiArr[i, 3].ToInt();
//dat.SpdZ = strMultiArr[i, 4].ToInt();

namespace RM
{
	public class TSVToMaster : RMBehaviour
	{
#if UNITY_EDITOR
		public UnityEngine.Object _OutFolder;
		public TextAsset[] _TSVArr;

		const string cEnumTemplate =
@"public enum <type_name>
{
<body>
}";
		const string cEnumBody = "	<enum_name>,";

		const string cStructTemplate =
@"using UnityEngine;
using RM;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct <type_name>
{
<body>

	static public <type_name>[] TSVToArr(string aTSV)
	{
		string[,] strMultiArr = aTSV.TSVToStrMultiArr();
		int len = strMultiArr.GetLength(0);
		<type_name>[] arr = new <type_name>[len - 2];

		for (int i = 0; i < len; i++)
		{
			if (i >= 2)
			{
				<type_name> dat = new <type_name>();
<body2>
				arr[i - 2] = dat;
			}
		}
		return arr;
	}

#if UNITY_EDITOR
	static public void CreateMasterScriptableObj(string aTSV, string aPath)
	{
		<type_name>[] arr = TSVToArr(aTSV);
		<type_name>Master mst = AssetDatabase.LoadAssetAtPath<<type_name>Master>(aPath);

		if (!mst)
		{
			mst = ScriptableObject.CreateInstance<<type_name>Master>();
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
}";
		const string cIntParce = "				dat.<field_name> = strMultiArr[i, <index>].ToInt();";
		const string cFloatParce = "			dat.<field_name> = strMultiArr[i, <index>].ToFloat();";
		const string cStrParce = "				dat.<field_name> = strMultiArr[i, <index>];";
		const string cEnumParce = "				dat.<field_name> = strMultiArr[i, <index>].ToEnum<<field_type>>();";



		const string cMasterTemplate =
@"using UnityEngine;

public class <type_name>Master : ScriptableObject
{
	public <type_name>[] _DatArr;
	public <type_name> this[int i] { get { return _DatArr[i]; } }
}";

		const string cStructBody = "	public <field_type> <field_name>;";


		[Button("GenerateMasterSource")]
		public int _GenerateMasterSource;
		void GenerateMasterSource()
		{
			string fileName;
			string typeName;
			string sourceType;
			string[,] strMultiArr;

			string strOut = "";
			string body = "";
			string body2 = "";

			int len;

			for (int i = 0; i < _TSVArr.Length; i++)
			{
				fileName = Path.GetFileNameWithoutExtension(_TSVArr[i].name);
				typeName = fileName.Split('@')[0];
				sourceType = fileName.Split('@')[1];

				strMultiArr = _TSVArr[i].text.TSVToStrMultiArr();

				if (sourceType == "enum")
				{
					body = "";
					strOut = cEnumTemplate;

					len = strMultiArr.GetLength(0);
					for (int j = 0; j < len; j++)
					{
						body += cEnumBody.Replace("<enum_name>", strMultiArr[j, 0]);

						if (j != len - 1)
							body += "\r\n";
					}
				}

				if (sourceType == "struct")
				{
					body = "";
					body2 = "";
					strOut = cStructTemplate;

					len = strMultiArr.GetLength(1);
					for (int j = 0; j < len; j++)
					{
						body += cStructBody.Replace("<field_type>", strMultiArr[0, j]).Replace("<field_name>", strMultiArr[1, j]);

						if (strMultiArr[0, j] == "int")
						{
							body2 += cIntParce.Replace("<field_name>", strMultiArr[1, j]).Replace("<index>", j.ToString());
						}
						else if (strMultiArr[0, j] == "float")
						{
							body2 += cFloatParce.Replace("<field_name>", strMultiArr[1, j]).Replace("<index>", j.ToString());
						}
						else if (strMultiArr[0, j] == "string")
						{
							body2 += cStrParce.Replace("<field_name>", strMultiArr[1, j]).Replace("<index>", j.ToString());
						}
						else
						{ 
							body2 += cEnumParce.Replace("<field_name>", strMultiArr[1, j]).Replace("<index>", j.ToString()).Replace("<field_type>", strMultiArr[0, j]);
						}

						if (j != len - 1)
							body += "\r\n";

						if (j != len - 1)
							body2 += "\r\n";
					}
				}

				strOut = strOut.Replace("<type_name>", typeName);
				strOut = strOut.Replace("<body>", body);
				strOut = strOut.Replace("<body2>", body2);

				File.WriteAllText(_OutFolder.GetAssetFullPath() + "/" + typeName + ".cs", strOut);

				if (sourceType == "struct")
				{
					strOut = cMasterTemplate;
					strOut = strOut.Replace("<type_name>", typeName);
					File.WriteAllText(_OutFolder.GetAssetFullPath() + "/" + typeName + "Master" + ".cs", strOut);
				}
			}
			AssetDatabase.Refresh();
		}

		[Button("GenerateMasterScriptableObj")]
		public int _GenerateMasterScriptableObj;
		void GenerateMasterScriptableObj()
		{
			string fileName;
			string typeName;
			for (int i = 0; i < _TSVArr.Length; i++)
			{
				fileName = Path.GetFileNameWithoutExtension(_TSVArr[i].name);
				typeName = fileName.Split('@')[0];

				Type type = Type.GetType(typeName);
				MethodInfo[] mArr = type.GetMethods();

				for (int j = 0; j < mArr.Length; j++)
				{
					Debug.Log(mArr[j].Name);
				}

				mArr = mArr.Where(a => a.IsStatic && a.Name == "CreateMasterScriptableObj").ToArray();
				if (mArr.Length != 0)
				{
					mArr[0].Invoke(type, new object[] { _TSVArr[i].text, _OutFolder.GetAssetPath() + "/" + typeName + "Master" + ".asset" });
				}

				//mArr = mArr.Where(a => a.IsStatic && a.Name == "CreateMasterScriptableObj").ToArray();
				//Debug.Log(mArr[0]);
			}
		}
#endif
	}
}
