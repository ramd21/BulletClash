using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM
{
	static public class TransformExtension
	{
		static public Transform[] FindAllRecurcive(this Transform aThis, string aName, bool aInclude = false)
		{
			Transform[] traArr = aThis.GetComponentsInChildren<Transform>(true);
			List<Transform> found = new List<Transform>();

			for (int i = 0; i < traArr.Length; i++)
			{
				if (aInclude)
				{
					if (traArr[i].name.Contains(aName))
					{
						found.Add(traArr[i]);
					}
				}
				else
				{
					if (traArr[i].name == aName)
					{
						found.Add(traArr[i]);
					}
				}
			}
			return found.ToArray();
		}

		static public T[] FindAllRecurcive<T>(this Transform aThis, string aName, bool aInclude = false) where T : Component
		{
			Transform[] traArr = aThis.GetComponentsInChildren<Transform>(true);
			List<T> found = new List<T>();
			T t;
			for (int i = 0; i < traArr.Length; i++)
			{
				if (aInclude)
				{
					if (traArr[i].name.Contains(aName))
					{
						t = traArr[i].GetComponent<T>();
						found.Add(t);
					}
				}
				else
				{
					if (traArr[i].name == aName)
					{
						t = traArr[i].GetComponent<T>();
						found.Add(t);
					}
				}
			}
			return found.ToArray();
		}

		static public Transform FindRecurcive(this Transform aThis, string aName, bool aInclude = false)
		{
			Transform[] traArr = aThis.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < traArr.Length; i++)
			{
				if (aInclude)
				{
					if (traArr[i].name.Contains(aName))
						return traArr[i];
				}
				else
				{
					if (traArr[i].name == aName)
						return traArr[i];
				}
			}
			return null;
		}

		static public T FindRecurcive<T>(this Transform aThis, string aName, bool aInclude = false) where T : Component
		{
			Transform[] traArr = aThis.GetComponentsInChildren<Transform>(true);
			T t;
			for (int i = 0; i < traArr.Length; i++)
			{
				if (aInclude)
				{
					if (traArr[i].name.Contains(aName))
					{
						t = traArr[i].GetComponent<T>();
						if (t)
							return t;
					}
				}
				else
				{
					if (traArr[i].name == aName)
					{
						t = traArr[i].GetComponent<T>();
						if (t)
							return t;
					}
				}
			}
			return null;
		}

		static public bool IsOffspringOf(this Transform aThis, Transform aFind)
		{
			Transform paremtTmp = aThis.parent;

			while (true)
			{
				if (!paremtTmp)
					return false;

				if (paremtTmp == aFind)
					return true;

				paremtTmp = paremtTmp.parent;
			}
		}

		static public void DeactivateSiblings(this Transform aThis)
		{
			for (int i = 0; i < aThis.parent.childCount; i++)
			{
				Transform tra = aThis.parent.GetChild(i);
				tra.gameObject.SetActive(aThis == tra);
			}
		}

		static Vector3 gV3;

		static Vector3 GetLocalWorldScaleRatio(this Transform aThis)
		{
			return aThis.localScale.Divide(aThis.lossyScale);
		}

		//reset>>
		public static void ResetTransform(this Transform aThis)
		{
			aThis.ResetPosition();
			aThis.ResetEulerAngles();
			aThis.ResetScale();
		}

		public static void ResetLocalTransform(this Transform aThis)
		{
			aThis.ResetLocalPosition();
			aThis.ResetLocalEulerAngles();
			aThis.ResetLocalScale();
		}

		public static void ResetPosition(this Transform aThis)
		{
			aThis.position = Vector3.zero;
		}

		public static void ResetLocalPosition(this Transform aThis)
		{
			aThis.localPosition = Vector3.zero;
		}

		public static void ResetEulerAngles(this Transform aThis)
		{
			aThis.eulerAngles = Vector3.zero;
		}

		public static void ResetLocalEulerAngles(this Transform aThis)
		{
			aThis.localEulerAngles = Vector3.zero;
		}

		public static void ResetScale(this Transform aThis)
		{
			aThis.SetScale(Vector3.one);
		}

		public static void ResetLocalScale(this Transform aThis)
		{
			aThis.localScale= Vector3.one;
		}
		//reset<<


		//set xyz>>
		public static void SetPosition(this Transform aThis, Vector3 aV3)
		{
			aThis.position = aV3;
		}

		public static void SetPosition(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.position = gV3;
		}

		public static void SetLocalPosition(this Transform aThis, Vector3 aV3)
		{
			aThis.localPosition = aV3;
		}

		public static void SetLocalPosition(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.localPosition = gV3;
		}

		public static void SetEulerAngles(this Transform aThis, Vector3 aV3)
		{
			aThis.eulerAngles = aV3;
		}

		public static void SetEulerAngles(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.eulerAngles = gV3;
		}

		public static void SetLocalEulerAngles(this Transform aThis, Vector3 aV3)
		{
			aThis.localEulerAngles = aV3;
		}

		public static void SetLocalEulerAngles(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.localEulerAngles = gV3;
		}

		public static void SetScale(this Transform aThis, Vector3 aV3)
		{
			aThis.SetLocalScale(aThis.GetLocalWorldScaleRatio().Multiply(aV3));
		}

		public static void SetScale(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.SetLocalScale(aThis.GetLocalWorldScaleRatio().Multiply(gV3));
		}

		public static void SetLocalScale(this Transform aThis, Vector3 aV3)
		{
			aThis.localScale = aV3;
		}

		public static void SetLocalScale(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.localScale = gV3;
		}
		//set xyz<<

		//set x>>
		public static void SetPositionX(this Transform aThis, float aX)
		{
			gV3 = aThis.position;
			gV3.x = aX;
			aThis.position = gV3;
		}

		public static void SetLocalPositionX(this Transform aThis, float aX)
		{
			gV3 = aThis.localPosition;
			gV3.x = aX;
			aThis.localPosition = gV3;
		}

		public static void SetEulerAnglesX(this Transform aThis, float aX)
		{
			gV3 = aThis.eulerAngles;
			gV3.x = aX;
			aThis.eulerAngles = gV3;
		}

		public static void SetLocalEulerAnglesX(this Transform aThis, float aX)
		{
			gV3 = aThis.localEulerAngles;
			gV3.x = aX;
			aThis.localEulerAngles = gV3;
		}

		public static void SetScaleX(this Transform aThis, float aX)
		{
			aThis.SetLocalScaleX((aThis.GetLocalWorldScaleRatio() * aX).x);
		}

		public static void SetLocalScaleX(this Transform aThis, float aX)
		{
			gV3 = aThis.localScale;
			gV3.x = aX;
			aThis.localScale = gV3;
		}
		//set x<<

		//set y>>
		public static void SetPositionY(this Transform aThis, float aY)
		{
			gV3 = aThis.position;
			gV3.y = aY;
			aThis.position = gV3;
		}

		public static void SetLocalPositionY(this Transform aThis, float aY)
		{
			gV3 = aThis.localPosition;
			gV3.y = aY;
			aThis.localPosition = gV3;
		}

		public static void SetEulerAnglesY(this Transform aThis, float aY)
		{
			gV3 = aThis.eulerAngles;
			gV3.y = aY;
			aThis.eulerAngles = gV3;
		}

		public static void SetLocalEulerAnglesY(this Transform aThis, float aY)
		{
			gV3 = aThis.localEulerAngles;
			gV3.y = aY;
			aThis.localEulerAngles = gV3;
		}

		public static void SetScaleY(this Transform aThis, float aY)
		{
			aThis.SetLocalScaleY((aThis.GetLocalWorldScaleRatio() * aY).y);
		}

		public static void SetLocalScaleY(this Transform aThis, float aY)
		{
			gV3 = aThis.localScale;
			gV3.y = aY;
			aThis.localScale = gV3;
		}
		//set y<<

		//set z>>
		public static void SetPositionZ(this Transform aThis, float aZ)
		{
			gV3 = aThis.position;
			gV3.z = aZ;
			aThis.position = gV3;
		}

		public static void SetLocalPositionZ(this Transform aThis, float aZ)
		{
			gV3 = aThis.localPosition;
			gV3.z = aZ;
			aThis.localPosition = gV3;
		}

		public static void SetEulerAnglesZ(this Transform aThis, float aZ)
		{
			gV3 = aThis.eulerAngles;
			gV3.z = aZ;
			aThis.eulerAngles = gV3;
		}

		public static void SetLocalEulerAnglesZ(this Transform aThis, float aZ)
		{
			gV3 = aThis.localEulerAngles;
			gV3.z = aZ;
			aThis.localEulerAngles = gV3;
		}

		public static void SetScaleZ(this Transform aThis, float aZ)
		{
			aThis.SetLocalScaleZ((aThis.GetLocalWorldScaleRatio() * aZ).z);
		}

		public static void SetLocalScaleZ(this Transform aThis, float aZ)
		{
			gV3 = aThis.localScale;
			gV3.z = aZ;
			aThis.localScale = gV3;
		}
		//set z<<


		//add xyz>>
		public static void AddPosition(this Transform aThis, Vector3 aV3)
		{
			aThis.position += aV3;
		}

		public static void AddPosition(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.position += gV3;
		}

		public static void AddLocalPosition(this Transform aThis, Vector3 aV3)
		{
			aThis.localPosition += aV3;
		}

		public static void AddLocalPosition(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.localPosition += gV3;
		}

		public static void AddEulerAngles(this Transform aThis, Vector3 aV3)
		{
			aThis.eulerAngles += aV3;
		}

		public static void AddEulerAngles(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.eulerAngles += gV3;
		}

		public static void AddLocalEulerAngles(this Transform aThis, Vector3 aV3)
		{
			aThis.localEulerAngles += aV3;
		}

		public static void AddLocalEulerAngles(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.localEulerAngles += gV3;
		}

		public static void AddScale(this Transform aThis, Vector3 aV3)
		{
			aThis.SetScale(aThis.lossyScale + aV3);
		}

		public static void AddScale(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.SetScale(aThis.lossyScale + gV3);
		}

		public static void AddLocalScale(this Transform aThis, Vector3 aV3)
		{
			aThis.localScale += aV3;
		}

		public static void AddLocalScale(this Transform aThis, float aX, float aY, float aZ)
		{
			gV3.Set(aX, aY, aZ);
			aThis.localScale += gV3;
		}
		//add xyz<<

		//add x>>
		public static void AddPositionX(this Transform aThis, float aX)
		{
			gV3 = aThis.position;
			gV3.x += aX;
			aThis.position = gV3;
		}

		public static void AddLocalPositionX(this Transform aThis, float aX)
		{
			gV3 = aThis.localPosition;
			gV3.x += aX;
			aThis.localPosition = gV3;
		}

		public static void AddEulerAnglesX(this Transform aThis, float aX)
		{
			gV3 = aThis.eulerAngles;
			gV3.x += aX;
			aThis.eulerAngles = gV3;
		}

		public static void AddLocalEulerAnglesX(this Transform aThis, float aX)
		{
			gV3 = aThis.localEulerAngles;
			gV3.x += aX;
			aThis.localEulerAngles = gV3;
		}

		public static void AddScaleX(this Transform aThis, float aX)
		{
			aThis.SetScaleX(aThis.lossyScale.x + aX);
		}

		public static void AddLocalScaleX(this Transform aThis, float aX)
		{
			gV3 = aThis.localScale;
			gV3.x += aX;
			aThis.localScale = gV3;
		}
		//add x<<

		//add y>>
		public static void AddPositionY(this Transform aThis, float aY)
		{
			gV3 = aThis.position;
			gV3.y += aY;
			aThis.position = gV3;
		}

		public static void AddLocalPositionY(this Transform aThis, float aY)
		{
			gV3 = aThis.localPosition;
			gV3.y += aY;
			aThis.localPosition = gV3;
		}

		public static void AddEulerAnglesY(this Transform aThis, float aY)
		{
			gV3 = aThis.eulerAngles;
			gV3.y += aY;
			aThis.eulerAngles = gV3;
		}

		public static void AddLocalEulerAnglesY(this Transform aThis, float aY)
		{
			gV3 = aThis.localEulerAngles;
			gV3.y += aY;
			aThis.localEulerAngles = gV3;
		}

		public static void AddScaleY(this Transform aThis, float aY)
		{
			aThis.SetScaleY(aThis.lossyScale.y + aY);
		}

		public static void AddLocalScaleY(this Transform aThis, float aY)
		{
			gV3 = aThis.localScale;
			gV3.y += aY;
			aThis.localScale = gV3;
		}
		//add y<<

		//add z>>
		public static void AddPositionZ(this Transform aThis, float aZ)
		{
			gV3 = aThis.position;
			gV3.z += aZ;
			aThis.position = gV3;
		}

		public static void AddLocalPositionZ(this Transform aThis, float aZ)
		{
			gV3 = aThis.localPosition;
			gV3.z += aZ;
			aThis.localPosition = gV3;
		}

		public static void AddEulerAnglesZ(this Transform aThis, float aZ)
		{
			gV3 = aThis.eulerAngles;
			gV3.z += aZ;
			aThis.eulerAngles = gV3;
		}

		public static void AddLocalEulerAnglesZ(this Transform aThis, float aZ)
		{
			gV3 = aThis.localEulerAngles;
			gV3.z += aZ;
			aThis.localEulerAngles = gV3;
		}

		public static void AddScaleZ(this Transform aThis, float aZ)
		{
			aThis.SetScaleZ(aThis.lossyScale.z + aZ);
		}

		public static void AddLocalScaleZ(this Transform aThis, float aZ)
		{
			gV3 = aThis.localScale;
			gV3.z += aZ;
			aThis.localScale = gV3;
		}
		//add z<<
	}
}




