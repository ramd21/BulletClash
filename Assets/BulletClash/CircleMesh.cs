using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleMesh : CreateMeshBase 
{
	public void Init(float aRadius, float aVertxDegInter, float aUVRadius, Vector3 aUp, Material aMat = null)
	{
		if (aVertxDegInter == 0)
			return;

		_Mesh = new Mesh();
		_MeshFilter.mesh = _Mesh;

		List<Vector3> v3List = new List<Vector3>();
		List<Vector2> v2UVList = new List<Vector2>();

		v3List.Add(Vector3.zero);
		v2UVList.Add(new Vector2(0.5f, 0.5f));

		float cnt = 360 / aVertxDegInter;

		for (int i = 0; i < cnt + 1; i++)
		{
			float deg = aVertxDegInter * i;
			if (deg > 360)
				deg = 360;


			Quaternion qt = Quaternion.identity;
			qt.SetLookRotation(aUp);
			Vector3 right = qt * Vector3.right;
			Vector3 up = qt * Vector3.up;

			Vector3 vtxPos;
			vtxPos = right * Mathf.Sin(deg * Mathf.Deg2Rad) * aRadius + up * Mathf.Cos(deg * Mathf.Deg2Rad) * aRadius;
			v3List.Add(vtxPos);

			float sinX, cosY;

			sinX = Mathf.Sin(deg * Mathf.Deg2Rad);
			cosY = Mathf.Cos(deg * Mathf.Deg2Rad);

			float uvX, uvY;
			uvX = sinX * aUVRadius;
			uvY = cosY * aUVRadius;

			uvX = 0.5f + uvX * 0.5f;
			uvY = 0.5f + uvY * 0.5f;

			v2UVList.Add(new Vector2(uvX, uvY));
		}
		_Mesh.vertices = v3List.ToArray();
		_Mesh.uv = v2UVList.ToArray();

		List<int> vtxList = new List<int>();
		for (int i = 0; i < cnt; i++)
		{
			vtxList.Add(0);
			vtxList.Add(i + 1);
			if(i == cnt - 1)
				vtxList.Add(1);
			else
				vtxList.Add(i + 2);
		}
		_Mesh.triangles = vtxList.ToArray();

		if (aMat)
			_MeshRenderer.sharedMaterial = aMat;
	}
}
