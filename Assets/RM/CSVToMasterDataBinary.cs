//#if UNITY_EDITOR

//using UnityEngine;
//using System.Collections;
//using System.IO;
//using System.Reflection;
//using System;
//using System.Collections.Generic;
//using UnityEditor;
//using Thrift.Transport;
//using Thrift.Protocol;

//[RequireComponent(typeof(AutoName))]
//public class CSVToMasterDataBinary : MonoBehaviour
//{
//	public string _NameSpace;
//	public UnityEngine.Object _CSVFolder;
//	public UnityEngine.Object _OutFile;

//	[Button("Convert")]
//	public int _Convert;

//	//[Button("Read")]
//	//public int _Read;


//	//public MSD _MSD;

//	//void Read()
//	//{

//	//	var transportIn = new TMemoryBuffer(File.ReadAllBytes(_OutFile.GetAssetFullPath()));
//	//	var protocolIn = new TBinaryProtocol(transportIn);

//	//	// Readで、バイト配列からデータを復元
//	//	_MSD = new MSD();
//	//	_MSD.Read(protocolIn);
//	//}


//	void Convert()
//	{
//		string[] fileArr = Directory.GetFiles(_CSVFolder.GetAssetFullPath());

//		string msdName = Path.GetFileName(_CSVFolder.GetAssetFullPath()).LowerSnakeToUpperCamel();

//		Type msdType = Type.GetType(_NameSpace + "." + msdName);
//		object msd = Activator.CreateInstance(msdType);



//		for (int i = 0; i < fileArr.Length; i++)
//		{
//			if (fileArr[i].EndsWith(".meta"))
//				continue;

//			string fileName = Path.GetFileNameWithoutExtension(fileArr[i]);
//			if (fileName.StartsWith("c_"))
//			{
//				string className = fileName.Split('_')[1].LowerSnakeToUpperCamel();

//				string[] lineArr = File.ReadAllLines(fileArr[i]);
//				if (lineArr.Length > 2)
//				{
//					string[] memberArr = lineArr[0].Split(',');

//					Type dataType = Type.GetType(_NameSpace + "." + className);
//					Type listType = typeof(List<>).MakeGenericType(dataType);
//					object listObj = Activator.CreateInstance(listType);
//					//System.Convert.ChangeType();


//					for (int j = 2; j < lineArr.Length; j++)
//					{
//						string[] valArr = lineArr[j].Split(',');
//						object dataObj = Makedata(dataType, memberArr, valArr);
//						listType.GetMethod("Add").Invoke(listObj, new object[] { dataObj });
//					}


//					PropertyInfo listProp = msd.GetType().GetProperty(className + "List");
//					listProp.GetSetMethod().Invoke(msd, new object[] { listObj });
//				}
//			}
//		}

//		//Debug.Log(msd.__isset.BodyList);

//		var transportOut = new TMemoryBuffer();
//		var protocolOut = new TBinaryProtocol(transportOut);

//		msd.GetType().GetMethod("Write").Invoke(msd, new object[] { protocolOut });
//		byte[] bytes = transportOut.GetBuffer();
//		Debug.Log(bytes);


//		File.WriteAllBytes(_OutFile.GetAssetFullPath(), bytes);



//	}

//	object Makedata(Type aDataType, string[] aMemberArr, string[] aValArr)
//	{
//		object dataObj;
//		PropertyInfo dataProp;

//		dataObj = Activator.CreateInstance(aDataType);
//		dataObj = System.Convert.ChangeType(dataObj, aDataType);
//		for (int k = 0; k < aMemberArr.Length; k++)
//		{
//			string propName = aMemberArr[k].LowerSnakeToUpperCamel();
//			dataProp = aDataType.GetProperty(propName);

//			if (dataProp.PropertyType.IsEnum)
//				dataProp.SetValue(dataObj, Enum.Parse(dataProp.PropertyType, aValArr[k]), null);
//			else
//				dataProp.SetValue(dataObj, System.Convert.ChangeType(aValArr[k], dataProp.PropertyType), null);

//		}

//		return dataObj;
//	}
//}
//#endif
