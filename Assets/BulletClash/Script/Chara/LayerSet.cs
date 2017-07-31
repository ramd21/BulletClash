using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using RM;

namespace RM
{
	public class LayerSet : RMBehaviour 
	{
		public bool _Lit;



		public void Set(bool aUI)
		{
			if (_Lit)
			{
				if (aUI)
				{
					gameObject.layer = LayerMask.NameToLayer("battle_ui_lit");
				}
				else
				{
					gameObject.layer = LayerMask.NameToLayer("battle_lit");
				}

			}
			else
			{
				if (aUI)
				{
					gameObject.layer = LayerMask.NameToLayer("battle_ui_unlit");
				}
				else
				{
					gameObject.layer = LayerMask.NameToLayer("battle_unlit");
				}
			}
		}
	}

#if UNITY_EDITOR
#endif
}