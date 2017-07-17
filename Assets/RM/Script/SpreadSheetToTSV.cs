#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace RM
{
	public class SpreadSheetToTSV : EditorUpdateBehaviour
	{
		public UnityEngine.Object _OutFolder;
		public string _Url;

		public string _FileName;

		string _SpreadSheetId;
		string _SheetId;
		WWW _WWW;
		bool _UpdateFileName;

		[Button("DownLoad")]
		public int _DownLoad;
		void DownLoad()
		{
			string url = "https://docs.google.com/spreadsheets/d/" + _SpreadSheetId + "/export?format=tsv&id=" + _SpreadSheetId + "&gid=" + _SheetId;
			Debug.Log(url);
			_WWW = new WWW(url);
		}

		void OnValidate()
		{
			if (_Url.IsNotNullOrEmpty())
			{
				_SpreadSheetId = _Url.Remove("https://docs.google.com/spreadsheets/d/");
				_SpreadSheetId = _SpreadSheetId.Split('/')[0];

				_SheetId = _Url.Remove("https://docs.google.com/spreadsheets/d/");
				_SheetId = _SheetId.Remove(_SpreadSheetId);
				_SheetId = _SheetId.Remove("/edit#gid=");

				_UpdateFileName = true;
				DownLoad();
			}
		}

		public override void EditorUpdate()
		{
			if (_WWW != null)
			{
				if (_WWW.isDone)
				{
					_FileName = _WWW.responseHeaders["Content-Disposition"].Remove("attachment; filename=");
					_FileName = _FileName.Split(';')[0];
					_FileName = _FileName.Remove("\"");

					if (_UpdateFileName)
					{
						_UpdateFileName = false;
					}
					else
					{
						Debug.Log(_WWW.responseHeaders["Content-Disposition"]);
						Debug.Log(_FileName);
						File.WriteAllBytes(_OutFolder.GetAssetFullPath() + "/" + _FileName + ".txt", _WWW.bytes);
						AssetDatabase.Refresh();
					}
					_WWW = null;
				}
			}
		}
	}
}
#endif
