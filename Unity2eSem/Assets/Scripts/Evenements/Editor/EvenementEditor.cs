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

            DessinerInspecteur(evenement, null, null, null, true);
        }

        
        
        public static void DessinerInspecteur(Evenement evenement, ListeConditions conditions, 
            List<Lieu> lieuxDispos, ListeLieux lieux, bool afficherSauvegarde = false)
        {
            if(afficherSauvegarde)
            {
                DessinerSauvegarde(evenement);
                GUILayout.Space(15);
            }
            DessinerChoixLieu(evenement, lieuxDispos);
            GUILayout.Space(15);
            DessinerConditions(evenement, conditions);
            GUILayout.Space(15);
            DessinerDescritpions(evenement);
            GUILayout.Space(30);
            DessinerListeChoix(evenement, lieux, conditions);
        }

        private static void DessinerConditions(Evenement evenement, ListeConditions conditions)
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            
            GUILayout.Label("Conditions");
            List<Condition> conditionsChoix =evenement.conditions;
                    
            string[] conditionsDispo = conditions.RecupNomsConditions(conditionsChoix);
                
            for (int j = 0; j < conditionsChoix.Count; j++)
            {
                GUILayout.BeginHorizontal();
                string[] conditionsDispoPlusUn = new string[conditionsDispo.Length + 1];
                conditionsDispoPlusUn[0] = conditionsChoix[j].nom;
                conditionsDispo.CopyTo(conditionsDispoPlusUn, 1);

                int indexSelecetion = 0;
                indexSelecetion = EditorGUILayout.Popup("condition " + j,
                    indexSelecetion, conditionsDispoPlusUn);
                conditionsChoix[j] = conditions.RecupCondition(conditionsDispoPlusUn[indexSelecetion]);

                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Supprimer"))
                {
                    conditionsChoix.RemoveAt(j);
                    break;
                }
                GUI.backgroundColor = couleurFondDefaut;
                    
                GUILayout.EndHorizontal();
            } 
                
            GUI.backgroundColor = Color.green;
            if (conditions.Conditions.Count > conditionsChoix.Count && 
                GUILayout.Button("Ajouter Condition"))
            {
                Condition conditionDefaut = conditions.RecupCondition(
                    conditions.RecupNomsConditions(conditionsChoix)[0]);
                    
                conditionsChoix.Add(conditionDefaut);
            }
            GUI.backgroundColor = couleurFondDefaut;
            GUILayout.Space(10);
        }
        
        private static void DessinerChoixLieu(Evenement evenement, List<Lieu> lieux)
        {
            GUIStyle couleurTexteRouge = new(GUI.skin.label)
            {
                normal =
                {
                    textColor = Color.red
                }
            };
            
            
            if (lieux != null)
            {
                if (lieux.Count == 0)
                {
                    GUILayout.Label("Pas assez de lieux dans la liste des lieux", couleurTexteRouge);
                    evenement.lieu = null;
                }
                else
                {
                    int indexSelectionLieu = evenement.lieu != null ? lieux.LastIndexOf(evenement.lieu) : 0;
                    indexSelectionLieu = indexSelectionLieu < 0 ? 0 : indexSelectionLieu;
                    
                    string[] optionsLieux = new string[lieux.Count];
                    
                    for (int i = 0; i < lieux.Count; i++)
                    {
                        optionsLieux[i] = lieux[i].nom;
                    }
                    
                    indexSelectionLieu = EditorGUILayout.Popup("Lieu", indexSelectionLieu, optionsLieux);
                    //Debug.Log(indexSelectionLieu);
                    evenement.lieu = lieux[indexSelectionLieu];
                }
            }
            else
            {
                
                GUILayout.Label("Il n'a pas de liste de lieux assignée dans la scene", couleurTexteRouge);
                evenement.lieu = null;
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

        private static void DessinerListeChoix(Evenement evenement, ListeLieux lieux, ListeConditions conditions)
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
                    ref evenement.ChoixDeployes[i], conditions, lieux,"Choix" + i);
                
                GUILayout.Space(10);

                if (i < evenement.listeChoix.Count - 1)
                {
                    GUILayout.Button("", GUILayout.Height(2));
                }

                GUILayout.Space(10);
            }
        }

        public static Evenement DessinerEmbedInspector(Evenement evenement, ref bool estDeploye,
            ListeConditions conditions, List<Lieu> lieuxDispos, ListeLieux lieux, string label = "Evenement")
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            
            CommencerZoneEmbed(Color.magenta);
            
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
                    return null;
                }
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Supprimer", optionsBoutons))
                {   
                    SupprimerAssetNarration(evenement);
                    return evenement;
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
                        RenommerAssetNarration(evenement, evenement.nomTemporaire);
                    }
                    
                    GUILayout.EndHorizontal();
                    GUILayout.Space(15);
                    DessinerInspecteur(evenement, conditions, lieuxDispos, lieux);   
                }
            }
            
            FinirZoneEmbed(Color.magenta);
            return evenement;
        }
    }
}
