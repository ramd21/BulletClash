using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RM;

namespace BC
{
	public class Init : MonoBehaviour
	{
		public int _ScreenW = 540;
		public int _ScreenH = 960;

		public string _Scene;

		void Start()
		{
			Screen.SetResolution(_ScreenH, _ScreenW, false);
			Application.targetFrameRate = 60;

			Deb.MethodLog();
			this.WaitForFrames(1, ()=> 
			{
				SceneManager.LoadScene(_Scene);
				Deb.MethodLog();
			});
		}
	}
}



