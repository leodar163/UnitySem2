using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Evenements.Editor
{
    public class EditeurNivoFenetre : EditorWindow
    {
        private static Semestre semestre;
        private bool semestreDeploye;
        private Vector2 scrollPosition;
        
        private void OnGUI()
        {
            if (GUILayout.Button("Sauvergarder"))
            {
                Sauvegarder();
            }

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            
            DessinerSemestre();
            
            GUILayout.EndScrollView();
            
        }

        private void DessinerSemestre()
        {
            semestre = SemestreEditor.DessinerEmbedInspector(semestre, ref semestreDeploye, 
                semestre ? semestre.name : "Semestre");
        }

        [MenuItem("Narration/Editeur de Nivo")]
        public static void OuvrirFenetre()
        {
            EditeurNivoFenetre fenetre = GetWindow<EditeurNivoFenetre>();
        }
        
        private static void Sauvegarder()
        {
            List<Object> assets = new List<Object>();
                
            assets.AddRange(LoadAllAssetsAtPath(ScriptableNarrationEditor.cheminChoix));
            assets.AddRange(LoadAllAssetsAtPath(ScriptableNarrationEditor.cheminEvenements));
            assets.AddRange(LoadAllAssetsAtPath(ScriptableNarrationEditor.cheminLieux));
            assets.AddRange(LoadAllAssetsAtPath(ScriptableNarrationEditor.cheminConditions));
            assets.AddRange(LoadAllAssetsAtPath(ScriptableNarrationEditor.cheminSemaines));
            assets.AddRange(LoadAllAssetsAtPath(ScriptableNarrationEditor.cheminSemestres));

            foreach (var asset in assets)
            {
                EditorUtility.SetDirty(asset);
            }
            AssetDatabase.SaveAssets();
            
            Debug.Log(assets.Count);
            Debug.Log("Assets de narration sauvegardés");
        }
        
        public static void DessinerCarre(Rect position, Color color) 
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0,0,color);
            texture.Apply();
            GUI.skin.box.normal.background = texture;
            GUI.Box(position, GUIContent.none);
        }


        public static List<Object> LoadAllAssetsAtPath (string path) 
        {
 
            List<Object> objects = new List<Object>();
            if (Directory.Exists(path)) 
            {
                string[] assets = Directory.GetFiles(path);
                
                foreach (string assetPath in assets) 
                {
                    if (assetPath.Contains(".asset") && !assetPath.Contains(".meta")) 
                    {
                        objects.Add(AssetDatabase.LoadMainAssetAtPath(assetPath));
                        Debug.Log("Loaded " + assetPath);
                    }
                }
            }
            return objects;
        }
        
        // public static bool BoutonEstEnfonce(KeyCode bouton)
        // {
        //     Event eventActuel = Event.current;
        //     return eventActuel is {isKey: true} && eventActuel.keyCode == bouton;
        // }
        //
        // public static bool BoutonsSontEnfonces(params KeyCode[] boutons)
        // {
        //     return boutons.All(BoutonEstEnfonce);
        // }
    }
}