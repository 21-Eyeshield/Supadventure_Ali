namespace Supinfo.Internals.Game
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	/// <summary>
	/// Ensures that the GameObject this script is attached to is not destroyed
	/// when a new scene is loaded.
	/// </summary>
	public sealed class DontDestroyOnPlayHandler : MonoBehaviour
	{
		#region Methods
		private void Awake()
		{
			List<GameObject> dontDestroyOnLoadObjects = GetDontDestroyOnLoadObjects();

			foreach (GameObject dontDestroyOnLoadObject in dontDestroyOnLoadObjects)
			{
				if (dontDestroyOnLoadObject.name == gameObject.name)
				{
					Destroy(gameObject);
					return;
				}
			}

			DontDestroyOnLoad(gameObject);
		}

		private static List<GameObject> GetDontDestroyOnLoadObjects()
		{
			List<GameObject> result = new List<GameObject>();

			List<GameObject> rootGameObjectsExceptDontDestroyOnLoad = new List<GameObject>();

			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				rootGameObjectsExceptDontDestroyOnLoad.AddRange(SceneManager.GetSceneAt(i).GetRootGameObjects());
			}

			List<GameObject> rootGameObjects = new List<GameObject>();
			Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();

			for (int i = 0; i < allTransforms.Length; i++)
			{
				Transform root = allTransforms[i].root;

				if (root.hideFlags == HideFlags.None
				    && !rootGameObjects.Contains(root.gameObject))
				{
					rootGameObjects.Add(root.gameObject);
				}
			}

			for (int i = 0; i < rootGameObjects.Count; i++)
			{
				if (!rootGameObjectsExceptDontDestroyOnLoad.Contains(rootGameObjects[i]))
					result.Add(rootGameObjects[i]);
			}

			return result;
		}
		#endregion Methods
	}
}