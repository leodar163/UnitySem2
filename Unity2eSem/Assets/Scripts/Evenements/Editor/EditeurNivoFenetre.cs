using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Evenements.Editor
{
    public class EditeurNivoFenetre : EditorWindow
    {
        private void OnGUI()
        {
            if (GUILayout.Button("Sauvergarder"))
            {
                Sauvegarder();
            }
        }

        private static void Sauvegarder()
        {
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath("Assets/Narration");
            foreach (var asset in assets)
            {
                EditorUtility.SetDirty(asset);
            }
            AssetDatabase.SaveAssets();
        }
    }
}