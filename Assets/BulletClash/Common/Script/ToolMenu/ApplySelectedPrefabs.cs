#if UNITY_EDITOR
using UnityEditor;

namespace RM
{
	public class ApplySelectedPrefabs : EditorWindow
	{
		[MenuItem("Tools/ApplySelectedPrefabs")]
		static void RunMenu()
		{
			for (int i = 0; i < Selection.gameObjects.Length; i++)
			{
				Selection.gameObjects[i].ApplyPrefab();
			}
		}
	}
}
#endif
