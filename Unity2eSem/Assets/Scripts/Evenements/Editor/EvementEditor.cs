using System.Collections.Generic;
using Editor;
using Plan;
using UnityEngine;

namespace Evenements.Editor
{
    using UnityEditor;
    
    [CustomEditor(typeof(Evenement))]
    public class EvementEditor : ScrObjEditor
    {
        public override void OnInspectorGUI()
        {
             
            
            Evenement evenement = target as Evenement;
            if(!evenement) return;

            DessinerInspecteur(evenement);
        }

        
        
        public static void DessinerInspecteur(Evenement evenement)
        {
            DessinerChoixLieu(evenement);
            GUILayout.Space(15);
            DessinerDescritpions(evenement);
            GUILayout.Space(15);
            DessinerListeChoix(evenement);
            GUILayout.Space(15);
            DessinerSauvegarde(evenement);
        }

        private static void DessinerChoixLieu(Evenement evenement)
        {
            GUIStyle couleurTexteRouge = new(GUI.skin.label)
            {
                normal =
                {
                    textColor = Color.red
                }
            };
            
            
            if (ListeLieux.Instance)
            {
                if (ListeLieux.Instance.Lieux.Count == 0)
                {
                    GUILayout.Label("Aucun lieu assigné dans la liste de lieux", couleurTexteRouge);
                }
                else
                {
                    evenement.lieu ??= ListeLieux.Instance.Lieux[0];
                    
                    int indexSelectionLieu = ListeLieux.Instance.Lieux.LastIndexOf(evenement.lieu);
                    indexSelectionLieu = indexSelectionLieu < 0 ? 0 : indexSelectionLieu;
                    string[] optionsLieux = ListeLieux.Instance.RecupNomsLieux();
                    indexSelectionLieu = EditorGUILayout.Popup("Lieu", indexSelectionLieu, optionsLieux);
                    evenement.lieu = ListeLieux.Instance.Lieux[indexSelectionLieu];
                }
            }
            else
            {
                
                GUILayout.Label("Il n'a pas de liste de lieux assignée dans la scene", couleurTexteRouge);
            }
        }

        private static void DessinerDescritpions(Evenement evenement)
        {
            GUILayout.Label("Intro d'infobulle");
            evenement.intro = GUILayout.TextArea(evenement.intro);
            
            GUILayout.Label("Description");
            evenement.description = GUILayout.TextArea(evenement.description);
            
            GUILayout.Space(10);
            
            GUILayout.Label("Illustration spécifique (par défaut, c'est l'illu du lieu qui est utilisée)");
            evenement.imageOverride = EditorGUILayout.ObjectField(evenement.imageOverride, typeof(Sprite), false) as Sprite;
        }

        private static void DessinerListeChoix(Evenement evenement)
        {
            Color couleurDefaut = GUI.backgroundColor;
            
            GUILayout.Label("LES CHOIX");
            for (int i = 0; i < evenement.listeChoix.Count; i++)
            {
                bool estFocus = evenement.FocusChoix == i;
                Choix choix = evenement.listeChoix[i];
                
                GUILayout.BeginHorizontal();

                GUILayoutOption[] optionsBouton =
                {
                    GUILayout.Height(25),
                    GUILayout.Width(25)
                };

                if (GUILayout.Button(estFocus ? "^" : "ˇ", optionsBouton))
                {
                    evenement.FocusChoix = estFocus ? -1 : i;
                }

                string nvNomChoix = GUILayout.TextField(choix.name, GUILayout.Height(25));
                if (nvNomChoix != choix.name && AssetDatabase.FindAssets(RecupChemin<Choix>() + '/' + 
                                                                         nvNomChoix + ".asset").Length == 0)
                {
                    AssetDatabase.RenameAsset(RecupChemin<Choix>() + '/' + choix.name + ".asset", nvNomChoix);
                    choix.name = nvNomChoix;
                }
                
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("X", optionsBouton))
                {   
                    SupprimerChoix(choix, evenement); 
                }
                GUI.backgroundColor = couleurDefaut;
                
                GUILayout.EndHorizontal();
            }

            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Ajouter Choix"))
            {
                evenement.listeChoix.Add(CreerAssetNarration<Choix>());
            }
            GUI.backgroundColor = couleurDefaut;
        }
        
        private static void SupprimerChoix(Choix choix, Evenement evenement)
        {
            if (EditorUtility.DisplayDialog("Supprimer Choix", "T sûr de vouloir surpprimer " + choix.name + " ?", "oui",
                "annuler"))
            {
                evenement.listeChoix.Remove(choix);
                AssetDatabase.DeleteAsset(RecupChemin<Choix>() + '/' + choix.name + ".asset");
                DestroyImmediate(choix, true);
            }
        }
    }
}
