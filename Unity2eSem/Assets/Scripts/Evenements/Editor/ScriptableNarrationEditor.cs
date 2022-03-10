﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Evenements.Editor
{
    public abstract class ScriptableNarrationEditor : UnityEditor.Editor
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
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            Color couleurFondDefaut = GUI.backgroundColor;
            GUI.backgroundColor = Color.black;
            if (GUILayout.Button("Sauvegarder", options))
            {
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
            }
            GUI.backgroundColor = couleurFondDefaut;
            GUILayout.EndHorizontal();
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
                throw new ArgumentOutOfRangeException(
                    typeScrNarration.Name + " n'est pas dérivé de ScriptableNarration");
            }

            return cheminRetour;
        }

        protected static T CreerAssetNarration<T>() where T : ScriptableNarration
        {
            if (CreateInstance(typeof(T)) is not T nvScriptNarration) return null;

            string chemin = RecupChemin<T>();
            int indexNom = 0;

            AssetExisteDeja<T>(typeof(T).Name + indexNom);

            //Debug.Log(AssetDatabase.FindAssets("name:"+typeof(T).Name + indexNom, new[] {chemin}).Length);
            while (AssetExisteDeja<T>(typeof(T).Name + indexNom))
            {
                indexNom++;
            }

            string nomNvChoix = typeof(T).Name + indexNom;
            AssetDatabase.CreateAsset(nvScriptNarration, chemin + '/' + nomNvChoix + ".asset");
            nvScriptNarration.name = nomNvChoix;
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(nvScriptNarration);
            return nvScriptNarration;

        }

        protected static void RenomerAssetNarration(ScriptableNarration assetNarration, string nvNom)
        {
            //EditorUtility.SaveFilePanel("Renomer " + assetNarration.name, )
            if (!NomEstValide(assetNarration.GetType(), nvNom))
            {
                EditorUtility.DisplayDialog("Nom invalide", "Le nom choisi est soit trop court, soit existe déjà",
                    "Mince");
            }
            else
            {
                //Debug.Log(ListeLieux.Instance.Lieux[0].nom);
                AssetDatabase.RenameAsset(RecupChemin(assetNarration.GetType()) + '/' +
                                          assetNarration.name + ".asset", nvNom);
                assetNarration.name = nvNom;
                //Debug.Log(ListeLieux.Instance.Lieux[0].nom);
            }
        }

        protected static bool NomEstValide(Type type, string nvNom)
        {
            string chemin = RecupChemin(type);

            // Debug.Log(type.Name);
            // Debug.Log(chemin + '/' + nvNom + ".asset");
            // Debug.Log(AssetDatabase.FindAssets(nvNom, new []{chemin}).Length);

            return !(nvNom.Length < 1 || AssetExisteDeja(type, nvNom));
        }

        protected static bool AssetExisteDeja<T>(string nom) where T : ScriptableNarration
        {
            string chemin = RecupChemin<T>();
            return AssetExisteDeja(chemin, nom);
        }

        protected static bool AssetExisteDeja(Type type, string nom)
        {
            string chemin = RecupChemin(type);
            return AssetExisteDeja(chemin, nom);
        }

        protected static bool AssetExisteDeja(string chemin, string nom)
        {
            List<string> assets = new List<string>(AssetDatabase.FindAssets(nom, new[] {chemin}));

            if (!nom.Contains(".asset"))
            {
                nom += ".asset";
            }

            for (int i = 0; i < assets.Count; i++)
            {
                assets[i] = AssetDatabase.GUIDToAssetPath(assets[i]);
                assets[i] = assets[i].Remove(0, chemin.Length + 1);
                // Debug.Log(assets[i]);
                // Debug.Log(nom);
            }

            return assets.Contains(nom);
        }

        protected static void SupprimerAssetNarration(ScriptableNarration asset, bool passerBoitDialogue = false)
        {
            if (passerBoitDialogue || EditorUtility.DisplayDialog("Supprimer " + asset.GetType().Name, 
                "T sûr de vouloir surpprimer " + asset.name + " ?" +
                "\n(L'action ne peut pas être annulée)", "oui", "annuler"))
            {
                if (asset is Evenement evenement)
                {
                    foreach (var _choix in evenement.listeChoix)
                    {
                        SupprimerAssetNarration(_choix, true);
                    }
                }

                if (asset is Choix choix)
                {
                    if (choix.evenementSuivant &&
                        EditorUtility.DisplayDialog("Supprimer Evenement Suivant",
                            "Tu veux supprimer aussi l'événement suivant du choix " + choix.name + " ?" +
                            "\n(Evenement : "+ choix.evenementSuivant.name +')', 
                            "oui, supprime", "nan, j'en ai besoin"))
                    {
                        SupprimerAssetNarration(choix.evenementSuivant, true);
                    }
                }
                
                AssetDatabase.DeleteAsset(RecupChemin(asset.GetType()) + '/' + asset.name + ".asset");
                DestroyImmediate(asset, true);
            }
        }
    }
}
