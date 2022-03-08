using System.Collections.Generic;
using Editor.FenetresUtilitaires;
using Plan;
using UnityEngine;

namespace Evenements.Editor
{
    using UnityEditor;
    
    [CustomEditor(typeof(Evenement))]
    public class EvementEditor : Editor
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
            evenement.intro = GUILayout.TextArea(evenement.intro,3);
            
            GUILayout.Label("Description");
            evenement.description = GUILayout.TextArea(evenement.description,30);
        }

        private static void DessinerListeChoix(Evenement evenement)
        {
            List<int> choixASupprimer = new List<int>();
            Color couleurDefaut = GUI.backgroundColor;
            
            GUILayout.Label("LES CHOIX");
            for (int i = 0; i < evenement.listeChoix.Count; i++)
            {
                bool estFocus = evenement.FocusChoix == i;
                Choix choix = evenement.listeChoix[i];
                
                GUILayout.BeginHorizontal();

                GUILayoutOption[] optionsBouton = new[]
                {
                    GUILayout.Height(25),
                    GUILayout.Width(25)
                };

                if (GUILayout.Button(estFocus ? "^" : "ˇ", optionsBouton))
                {
                    evenement.FocusChoix = estFocus ? -1 : i;
                }

                choix.name = GUILayout.TextField(choix.name, GUILayout.Height(25));
            
                
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("X", optionsBouton))
                {
                    
                  SupprimerChoix(choix, evenement); 
                }
                GUI.backgroundColor = couleurDefaut;
                
                GUILayout.EndHorizontal();
            }

            foreach (var index in choixASupprimer)
            {
                Destroy(evenement.listeChoix[index]);
                evenement.listeChoix.RemoveAt(index);
            }
            
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Ajouter Choix"))
            {
                AjouterChoix(evenement);
            }
            GUI.backgroundColor = couleurDefaut;
        }
        
        private static Choix AjouterChoix(Evenement evenement)
        {
            if (CreateInstance(typeof(Choix)) is Choix nvChoix)
            {
                AssetDatabase.CreateAsset(nvChoix, Choix.pathFichiers + "/Choix" + evenement.listeChoix.Count + ".asset");
                AssetDatabase.SaveAssets();
                evenement.listeChoix.Add(nvChoix);
            }

            return null;
        }

        private static void SupprimerChoix(Choix choix, Evenement evenement)
        {
            if (EditorUtility.DisplayDialog("Supprimer Choix", "T sûr de vouloir surpprimer" + choix.name + " ?", "oui",
                "non"))
            {
                evenement.listeChoix.Remove(choix);
                DestroyImmediate(choix);
            }
        }
    }
}
