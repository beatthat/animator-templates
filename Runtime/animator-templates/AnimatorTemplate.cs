#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEditor;
using System.IO;
using System;

namespace BeatThat.AnimatorTemplates
{
	using EditorAnimatorController = UnityEditor.Animations.AnimatorController;

	public class AnimatorTemplate : MonoBehaviour 
	{
		public static EditorAnimatorController CreateAnimatorController(string fromResourcePath, string fileName = "ShowHidePanelView")
		{
			var fromC = Resources.Load<EditorAnimatorController>(fromResourcePath);
			if (fromC == null) {
				return null;
			}
			var templateAssetPath = AssetDatabase.GetAssetPath (fromC);
			var savePath = EditorUtility.SaveFilePanel (
				"Save AnimatorController", 
				Application.dataPath, 
				fileName, 
				"controller");
			if (string.IsNullOrEmpty (savePath)) {
				return null;
			}
			var cut = savePath.IndexOf ("Assets");
			savePath = (cut != -1) ? savePath.Substring (cut) : Path.Combine("Assets", DateTime.Now.Ticks.ToString());
			savePath = savePath.EndsWith(".controller")? savePath: savePath + ".controller";
			if (!AssetDatabase.CopyAsset (templateAssetPath, savePath)) {
				Debug.LogWarning ("[" + Time.frameCount + "] failed to create template for resource '" + fromResourcePath
					+ "' (asset path '" + templateAssetPath + "', save path '" + savePath + "'");
				return null;
			}
			return AssetDatabase.LoadAssetAtPath<EditorAnimatorController>(savePath);
		}
	}

}
#endif


