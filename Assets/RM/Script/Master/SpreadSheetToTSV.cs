
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RM
{
	public class SpreadSheetToTSV : EditorUpdateBehaviour
	{
#if UNITY_EDITOR
		public UnityEngine.Object _OutFolder;
		public string _Url;
		public string _FileName;

		[SerializeField, HideInInspector]
		string _UrlLast;

		string _SpreadSheetId;
		string _SheetId;
		WWW _WWW;
		bool _UpdateFileName;

		[Button("DownLoad")]
		public int _DownLoad;
		void DownLoad()
		{
			SetId();
			string url = "https://docs.google.com/spreadsheets/d/" + _SpreadSheetId + "/export?format=tsv&id=" + _SpreadSheetId + "&gid=" + _SheetId;
			Debug.Log(url);
			_WWW = new WWW(url);
		}

		void SetId()
		{
			_SpreadSheetId = _Url.Remove("https://docs.google.com/spreadsheets/d/");
			_SpreadSheetId = _SpreadSheetId.Split('/')[0];
			_SheetId = _Url.Remove("https://docs.google.com/spreadsheets/d/");
			_SheetId = _SheetId.Remove(_SpreadSheetId);
			_SheetId = _SheetId.Remove("/edit#gid=");
		}


		void OnValidate()
		{
			if (_Url != _UrlLast)
			{
				if (_Url.IsNotNullOrEmpty())
				{
					_UpdateFileName = true;
					DownLoad();
					_UrlLast = _Url;
				}
			}
		}

		public override void EditorUpdate()
		{
			if (_WWW != null)
			{
				if (_WWW.isDone)
				{
					_FileName = _WWW.responseHeaders["Content-Disposition"].Split(';')[2];
					_FileName = _FileName.Remove(" filename*=UTF-8''");
					_FileName = Uri.UnescapeDataString(_FileName);
					_FileName = _FileName.Replace(" - ", "#");
					_FileName = _FileName.Split('#')[1];

					if (_UpdateFileName)
					{
						_UpdateFileName = false;
					}
					else
					{
						File.WriteAllBytes(_OutFolder.GetAssetFullPath() + "/" + _FileName + ".txt", _WWW.bytes);
						AssetDatabase.Refresh();
					}
					_WWW = null;
				}
			}
		}
#endif
	}
}
