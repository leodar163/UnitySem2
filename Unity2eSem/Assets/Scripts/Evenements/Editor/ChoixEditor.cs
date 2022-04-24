using System.Collections.Generic;
using Plan;
using UnityEngine;
using UnityEngine.UI;

namespace Evenements.Editor
{
    using UnityEditor;
    [CustomEditor(typeof(Choix))]
    public class ChoixEditor : ScriptableNarrationEditor
    {
        public override void OnInspectorGUI()
        {
            Choix choix = target as Choix;
            if(choix == null) return;
            
            DessinerInspecteur(choix, null, null, true);
        }

        public static void DessinerInspecteur(Choix choix, ListeConditions conditions, 
            ListeLieux lieux, bool afficherSauvegarde = false)
        {
            if (afficherSauvegarde)
            {
                DessinerSauvegarde(choix);
                GUILayout.Space(15);
            }
            DessinerDescription(choix);
            GUILayout.Space(15);
            DessinerConditions(choix, conditions);
            GUILayout.Space(15);
            DessinerGainCout(choix);
            GUILayout.Space(15);
            //DessinerEvenementSuivant(choix);
            choix.evenementSuivant =
                EvenementEditor.DessinerEmbedInspector(choix.evenementSuivant, ref choix.montrerEvenementSuivant, 
                    conditions, lieux ? lieux.Lieux : null, lieux, "Evenement Suivant");
        }

        private static void DessinerDescription(Choix choix)
        {
            GUILayout.Label("Description");
            choix.Description = GUILayout.TextArea(choix.Description);
        }
        
        //Bon cette fonction faudra la tester à terme... Pk j'suis pas sûr de son entier fonctionnement
        private static void DessinerConditions(Choix choix, ListeConditions conditions)
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            
            GUIStyle couleurTexteRouge = new(GUI.skin.label)
            {
                normal =
                {
                    textColor = Color.red
                }
            };

            if (!conditions)
            {
                GUILayout.Label("Il n'a pas de liste de condition accessible", couleurTexteRouge);
                return;
            }
            if (conditions.Conditions.Count == 0)
            {
                GUILayout.Label("Aucune condition disponible dans la liste " + conditions.name, couleurTexteRouge);
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                GUILayout.Label(i == 0 ? "Conditions" : "Conséquences");
                List<Condition> conditionsChoix = i == 0 ? choix.Conditions : choix.Consequences;
                    
                string[] conditionsDispo = conditions.RecupNomsConditionsExlusif(conditionsChoix);
                
                for (int j = 0; j < conditionsChoix.Count; j++)
                {
                    GUILayout.BeginHorizontal();
                    string[] conditionsDispoPlusUn = new string[conditionsDispo.Length + 1];
                    conditionsDispoPlusUn[0] = conditionsChoix[j].nom;
                    conditionsDispo.CopyTo(conditionsDispoPlusUn, 1);

                    int indexSelecetion = 0;
                    indexSelecetion = EditorGUILayout.Popup(i == 0 ? "condition " + j : "consequence " + j,
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
                    GUILayout.Button("Ajouter " + (i == 0 ? "Condition" : "Conséquence")))
                {
                    Condition conditionDefaut = conditions.RecupCondition(
                        conditions.RecupNomsConditionsExlusif(conditionsChoix)[0]);
                    
                    conditionsChoix.Add(conditionDefaut);
                }
                GUI.backgroundColor = couleurFondDefaut;
                GUILayout.Space(10);
            }
        }

        private static void DessinerGainCout(Choix choix)
        {
            GUILayout.Label("Gains");
            Choix.Gain gains = new Choix.Gain
            {
                argent = EditorGUILayout.IntSlider("Argent", choix.Gains.argent, 0, 100),
                santeMentale = EditorGUILayout.IntSlider("Santé Mentale", choix.Gains.santeMentale, 0, 100),
                etude = EditorGUILayout.IntSlider("Etude", choix.Gains.etude, 0, 100)
            };
            choix.Gains = gains;
            GUILayout.Space(15);
            GUILayout.Label("Coûts");
            Choix.Gain couts = new Choix.Gain
            {
                argent = EditorGUILayout.IntSlider("Argent", choix.Couts.argent, 0, 100),
                santeMentale = EditorGUILayout.IntSlider("Santé Mentale", choix.Couts.santeMentale, 0, 100),
                etude = EditorGUILayout.IntSlider("Etude", choix.Couts.etude, 0, 100)
            };
            choix.Couts = couts;
        }

        public static Choix DessinerEmbedInspecteur(Choix choix, ref bool estDeploye, ListeConditions conditions,
            ListeLieux lieux, string label = "Choix")
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            
            CommencerZoneEmbed(Color.yellow);
            
            GUILayout.BeginHorizontal();

            estDeploye = EditorGUILayout.Foldout(estDeploye, choix.name);

            GUILayoutOption[] optionsBouton =
            {
                GUILayout.Height(25),
                GUILayout.Width(100)
            };

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Supprimer", optionsBouton))
            {   
                SupprimerAssetNarration(choix);
                return choix;
            }
            GUI.backgroundColor = couleurFondDefaut;
                
            GUILayout.EndHorizontal();
                
            //Affichhe l'inspecteur quand est déployé
            if (estDeploye)
            {
                GUILayout.BeginHorizontal();
                if (choix.nomTemporaire == "")
                    choix.nomTemporaire = choix.name;
                    
                choix.nomTemporaire = 
                    GUILayout.TextField(choix.nomTemporaire, 
                        GUILayout.Height(20));
                    
                if (GUILayout.Button("Renomer", GUILayout.Height(20), GUILayout.Width(130)))
                {
                    RenommerAssetNarration(choix, choix.nomTemporaire);
                }
                    
                GUILayout.EndHorizontal();
                GUILayout.Space(15);
                    
                DessinerInspecteur(choix, conditions, lieux);
            }

            FinirZoneEmbed(Color.yellow);
            
            return choix;
        }
    }
}