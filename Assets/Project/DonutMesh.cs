using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DonutMesh : CreateMeshBase 
{
	public void Init(float aRadiusL, float aRadiusS, float aVertxDegInter, float aStartDeg, float aEndDeg, float aUVRadiusMax, float aUVRadiusMin, Vector3 aUp, Vector3 aForward, Material aMat = null)
	{
		if (aVertxDegInter == 0)
			return;

		_Mesh = new Mesh();
		_MeshFilter.mesh = _Mesh;

		List<Vector3> v3List = new List<Vector3>();
		List<Vector2> v2UVList = new List<Vector2>();

		float degDiff = aEndDeg - aStartDeg;
		float cnt = degDiff / aVertxDegInter;

		for (int i = 0; i < cnt + 1; i++)
		{
			float deg = aStartDeg + aVertxDegInter * i;
			if (deg > aEndDeg)
				deg = aEndDeg;

			Quaternion qt = Quaternion.identity;
			qt.SetLookRotation(aForward, aUp);
			Vector3 right = qt * Vector3.right;
			Vector3 forward = qt * Vector3.forward;

			Vector3 vtxPos;
			vtxPos = right * Mathf.Sin(deg * Mathf.Deg2Rad) * aRadiusS + forward * Mathf.Cos(deg * Mathf.Deg2Rad) * aRadiusS;
			v3List.Add(vtxPos);

			vtxPos = right * Mathf.Sin(deg * Mathf.Deg2Rad) * aRadiusL + forward * Mathf.Cos(deg * Mathf.Deg2Rad) * aRadiusL;
			v3List.Add(vtxPos);

			float uvX, uvY;
			float sinX, cosY;
			sinX = Mathf.Sin(deg * Mathf.Deg2Rad);
			cosY = Mathf.Cos(deg * Mathf.Deg2Rad);

			uvX = sinX * aUVRadiusMin;
			uvY = cosY * aUVRadiusMin;
			uvX = 0.5f + uvX * 0.5f;
			uvY = 0.5f + uvY * 0.5f;

			v2UVList.Add(new Vector2(uvX, uvY));

			uvX = sinX * aUVRadiusMax;
			uvY = cosY * aUVRadiusMax;
			uvX = 0.5f + uvX * 0.5f;
			uvY = 0.5f + uvY * 0.5f;
			v2UVList.Add(new Vector2(uvX, uvY));
		}

		_Mesh.vertices = v3List.ToArray();
		_Mesh.uv = v2UVList.ToArray();


		List<int> vtxList = new List<int>();
		for (int i = 0; i < cnt; i++)
		{
			vtxList.Add(i * 2);
			vtxList.Add(i * 2 + 1);
			vtxList.Add(i * 2 + 2);

			vtxList.Add(i * 2 + 3);
			vtxList.Add(i * 2 + 2);
			vtxList.Add(i * 2 + 1);
		}

		_Mesh.triangles = vtxList.ToArray();

		if (aMat)
			_MeshRenderer.sharedMaterial = aMat;
	}
}
