using System.Collections.Generic;
using Plan;
using UnityEngine;

namespace Evenements.Editor
{
    using UnityEditor;
    
    [CustomEditor(typeof(Evenement))]
    public class EvenementEditor : ScriptableNarrationEditor
    {
        public override void OnInspectorGUI()
        {
             
            
            Evenement evenement = target as Evenement;
            if(!evenement) return;

            DessinerInspecteur(evenement);
        }

        
        
        public static void DessinerInspecteur(Evenement evenement)
        {
            DessinerSauvegarde(evenement);
            GUILayout.Space(15);
            DessinerChoixLieu(evenement);
            GUILayout.Space(15);
            DessinerDescritpions(evenement);
            GUILayout.Space(30);
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
            
            GUILayout.Label("Illustration spécifique \n(par défaut, c'est l'illu du lieu qui est utilisée)");
            evenement.imageOverride = EditorGUILayout.ObjectField(evenement.imageOverride, typeof(Sprite), false) as Sprite;
        }

        private static void DessinerListeChoix(Evenement evenement)
        {
            Color couleurDefaut = GUI.backgroundColor;
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("LES CHOIX");
            GUI.backgroundColor = Color.green;
            
            if (GUILayout.Button("Ajouter Choix"))
            {
                evenement.listeChoix.Add(CreerAssetNarration<Choix>());
            }
            GUI.backgroundColor = couleurDefaut;
            
            GUILayout.EndHorizontal();
            
            
            for (int i = 0; i < evenement.listeChoix.Count; i++)
            {
                ChoixEditor.DessinerEmbedInspecteur(evenement.listeChoix[i], 
                    ref evenement.ChoixDeployes[i], "Choix" + i);
                
                GUILayout.Space(10);

                if (i < evenement.listeChoix.Count - 1)
                {
                    GUILayout.Button("", GUILayout.Height(2));
                }

                GUILayout.Space(10);
            }
        }

        public static Evenement DessinerEmbedInspector(Evenement evenement, ref bool estDeploye, string label = "Evenement")
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            GUILayout.BeginVertical();
            
            GUI.backgroundColor = Color.magenta;
            GUILayout.Button("", GUILayout.Height(15));
            GUI.backgroundColor = couleurFondDefaut;
            
            if (evenement == null)
            {
                GUILayout.BeginHorizontal();
                evenement = EditorGUILayout.ObjectField(label, evenement,
                    typeof(Evenement), false) as Evenement;
                
                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Creer"))
                {
                    evenement = CreerAssetNarration<Evenement>();
                }
                GUI.backgroundColor = couleurFondDefaut;
                
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal();
                
                estDeploye =
                    EditorGUILayout.Foldout(estDeploye, label + " - " + evenement.name);
                
                GUI.backgroundColor = new Color32(255, 180, 0, 255);

                GUILayoutOption[] optionsBoutons =
                {
                    GUILayout.Width(80)
                };
                
                if (GUILayout.Button("Retirer", optionsBoutons))
                {
                    evenement = null;
                    return null;
                }
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Supprimer", optionsBoutons))
                {   
                    SupprimerAssetNarration(evenement);
                    return null;
                }

                GUI.backgroundColor = couleurFondDefaut;
                GUILayout.EndHorizontal();

                if (estDeploye)
                {
                    
                    GUILayout.BeginHorizontal();
                    if (evenement.nomTemporaire == "")
                        evenement.nomTemporaire = evenement.name;
                    
                    evenement.nomTemporaire = 
                        GUILayout.TextField(evenement.nomTemporaire, 
                            GUILayout.Height(20));
                    
                    if (GUILayout.Button("Renomer", GUILayout.Height(20), GUILayout.Width(130)))
                    {
                        RenomerAssetNarration(evenement, evenement.nomTemporaire);
                    }
                    
                    GUILayout.EndHorizontal();
                    GUILayout.Space(15);
                    DessinerInspecteur(evenement);   
                }
            }
            
            GUI.backgroundColor = Color.magenta;
            GUILayout.Button("", GUILayout.Height(5));
            GUI.backgroundColor = couleurFondDefaut;
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            return evenement;
        }
    }
}
