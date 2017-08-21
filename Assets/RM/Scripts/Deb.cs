using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

namespace RM
{
	public class Deb
	{
		static public void MethodLog(string aLog = "")
		{
			StackTrace st = new StackTrace();
			StackFrame sf = st.GetFrame(1);
			MethodBase mb = sf.GetMethod();

			UnityEngine.Debug.Log(mb.ReflectedType + "." + mb.Name + "() at " + Time.frameCount + " " + aLog);
		}
	}
}


