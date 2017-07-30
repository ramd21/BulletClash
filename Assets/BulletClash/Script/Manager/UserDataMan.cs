using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;


namespace BC
{
	public class UserDataMan : Singleton<UserDataMan>
	{
		public UserData _UserData;
	}


	[System.Serializable]
	public struct UserData
	{
		public string _Name;
		public int _Lv;
		public int _Gold;
	} 
}


