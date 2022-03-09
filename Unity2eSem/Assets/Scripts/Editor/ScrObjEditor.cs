
     using System;
     using Evenements;
     using UnityEngine;
     using Object = System.Object;

     namespace Editor
    {
        using UnityEditor;
        public abstract class ScrObjEditor : Editor
        {
            public static string cheminChoix = "Assets/Narration/Choix";
            public static string cheminEvenements = "Assets/Narration/Evenements";
            public static string cheminConditions = "Assets/Narration/Conditions";
            public static string cheminSemaines = "Assets/Narration/Semaines";
            public static string cheminSemestres = "Assets/Narration/Semestres";
            
            protected static void DessinerSauvegarde(ScriptableObject target)
            {
                GUILayoutOption[] options =
                {
                    GUILayout.Height(25),
                    GUILayout.Width(120)
                };
                
                if(GUILayout.Button("Sauvegarder", options))
                {
                    EditorUtility.SetDirty(target);
                    AssetDatabase.SaveAssets();
                }
            }

            protected static string RecupChemin<T>() where T : ScriptableNarration
            {
                string cheminRetour;

                if (typeof(T) == typeof(Choix))
                {
                    cheminRetour = cheminChoix;
                }
                else if (typeof(T) == typeof(Evenement))
                {
                    cheminRetour = cheminEvenements;
                }
                else if (typeof(T) == typeof(ListeConditions))
                {
                    cheminRetour = cheminConditions;
                }
                else if (typeof(T) == typeof(Semaine))
                {
                    cheminRetour = cheminSemaines;
                }
                else if (typeof(T) == typeof(Semestre))
                {
                    cheminRetour = cheminSemestres;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(typeof(T).Name + " n'est pas dérivé de ScriptableNarration");
                }

                return cheminRetour;
            }
            
            protected static string RecupChemin(Type typeScrNarration)
            {
                string cheminRetour;

                if (typeScrNarration == typeof(Choix))
                {
                    cheminRetour = cheminChoix;
                }
                else if (typeScrNarration == typeof(Evenement))
                {
                    cheminRetour = cheminEvenements;
                }
                else if (typeScrNarration == typeof(ListeConditions))
                {
                    cheminRetour = cheminConditions;
                }
                else if (typeScrNarration == typeof(Semaine))
                {
                    cheminRetour = cheminSemaines;
                }
                else if (typeScrNarration == typeof(Semestre))
                {
                    cheminRetour = cheminSemestres;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(typeScrNarration.Name + " n'est pas dérivé de ScriptableNarration");
                }

                return cheminRetour;
            }
            
            protected static T CreerAssetNarration<T>() where T : ScriptableNarration
            {
                if (CreateInstance(typeof(T)) is not T nvScriptNarration) return null;
                
                string chemin = RecupChemin<T>();
                int indexNom = 0;
                while (AssetDatabase.FindAssets("Choix" + indexNom, new[] {chemin}).Length > 0)
                {
                    indexNom++;
                }
                string nomNvChoix = typeof(T).Name+indexNom;
                AssetDatabase.CreateAsset(nvScriptNarration, chemin + '/' + nomNvChoix + ".asset");
                nvScriptNarration.name = nomNvChoix;
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                return nvScriptNarration;

            }

            protected static void RenomerAssetNarration(ScriptableNarration assetNarration, string nvNom)
            {
                //EditorUtility.SaveFilePanel("Renomer " + assetNarration.name, )
                if (!EstNomValide(assetNarration.GetType(), nvNom))
                {
                    EditorUtility.DisplayDialog("Nom invalide", "Le nom choisi est soit trop court, soit existe déjà",
                        "Mince");
                }
                else
                {
                    AssetDatabase.RenameAsset(RecupChemin(assetNarration.GetType()) + '/' + 
                                              assetNarration.name + ".asset", nvNom);
                    assetNarration.name = nvNom;
                }
            }

            protected static bool EstNomValide(Type type, string nvNom)
            {
                string chemin = RecupChemin(type);

                return !(nvNom.Length < 1 || AssetDatabase.FindAssets(chemin + '/' + nvNom + ".asset").Length > 0);
            }
        }
    }
