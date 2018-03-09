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

			var toC = EditorAnimatorController.CreateAnimatorControllerAtPath (savePath);

			toC.layers = fromC.layers;
			toC.parameters = fromC.parameters;

			return toC;
		}

		public static void CopyResourceToAnimatorController(string fromResourcePath, EditorAnimatorController toC)
		{
			var fromC = Resources.Load<EditorAnimatorController>(fromResourcePath);
			toC.layers = fromC.layers;
			toC.parameters = fromC.parameters;
		}

		public static void Copy(EditorAnimatorController fromC, EditorAnimatorController toC)
		{
			toC.layers = fromC.layers;
			toC.parameters = fromC.parameters;
		}
	}

}
#endif
