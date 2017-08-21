using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using System;

namespace BC
{
	public class MasterMan : Singleton<MasterMan>
	{
		public UnitParamMaster _UnitParam;
		public BulletParamMaster _BulletParam;
		public SeParamMaster _SeParam;
	}
}


