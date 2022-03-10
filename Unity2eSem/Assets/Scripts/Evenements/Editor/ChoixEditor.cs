using System.Collections.Generic;
using Plan;
using UnityEngine;

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
            
            DessinerInspecteur(choix, null);
        }

        public static void DessinerInspecteur(Choix choix, ListeConditions conditions)
        {
            DessinerSauvegarde(choix);
            GUILayout.Space(15);
            DessinerDescription(choix);
            GUILayout.Space(15);
            DessinerConditions(choix, conditions);
            GUILayout.Space(15);
            DessinerGainCout(choix);
            GUILayout.Space(15);
            //DessinerEvenementSuivant(choix);
            choix.evenementSuivant =
                EvenementEditor.DessinerEmbedInspector(choix.evenementSuivant, ref choix.montrerEvenementSuivant,
                    "Evenement Suivant");
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
                GUILayout.Label("Aucune condition disponible dans la liste " + conditions.name);
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                GUILayout.Label(i == 0 ? "Conditions" : "Conséquences");
                List<Condition> conditionsChoix = i == 0 ? choix.Conditions : choix.Consequences;
                    
                string[] conditionsDispo = conditions.RecupNomsConditions(conditionsChoix);
                
                for (int j = 0; j < conditionsChoix.Count; j++)
                {
                    GUILayout.BeginHorizontal();
                    string[] conditionsDispoPlusUn = new string[conditionsDispo.Length + 1];
                    conditionsDispoPlusUn[0] = conditionsChoix[i].Nom;
                    conditionsDispo.CopyTo(conditionsDispoPlusUn, 1);

                    int indexSelecetion = 0;
                    indexSelecetion = EditorGUILayout.Popup(i == 0 ? "condition " + i : "consequence " + i,
                        indexSelecetion, conditionsDispoPlusUn);
                    conditionsChoix[i] = conditions.RecupCondition(conditionsDispoPlusUn[indexSelecetion]);

                    GUILayout.EndHorizontal();
                } 
                
                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Ajouter " + (i == 0 ? "Condition" : "Conséquence")))
                {
                    Condition conditionDefaut = conditions.RecupCondition(
                        conditions.RecupNomsConditions(conditionsChoix)[0]);
                    
                    conditionsChoix.Add(conditionDefaut);
                }
                GUI.backgroundColor = couleurFondDefaut;
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
            
            GUILayout.Label("Coûts");
            Choix.Gain couts = new Choix.Gain
            {
                argent = EditorGUILayout.IntSlider("Argent", choix.Couts.argent, 0, 100),
                santeMentale = EditorGUILayout.IntSlider("Santé Mentale", choix.Couts.santeMentale, 0, 100),
                etude = EditorGUILayout.IntSlider("Etude", choix.Couts.etude, 0, 100)
            };
            choix.Couts = couts;
        }

        public static Choix DessinerEmbedInspecteur(Choix choix, ref bool estDeploye, string label = "Choix")
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            GUILayout.BeginVertical();
            
            GUI.backgroundColor = Color.yellow;
            GUILayout.Button("", GUILayout.Height(10));
            GUI.backgroundColor = couleurFondDefaut;
            
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
                return null;
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
                    RenomerAssetNarration(choix, choix.nomTemporaire);
                }
                    
                GUILayout.EndHorizontal();
                GUILayout.Space(15);
                    
                DessinerInspecteur(choix, null);
            }

            GUI.backgroundColor = Color.yellow;
            GUILayout.Button("", GUILayout.Height(5));
            GUI.backgroundColor = couleurFondDefaut;
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            return choix;
        }

        /*private static void DessinerEvenementSuivant(Choix choix)
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            if (choix.evenementSuivant == null)
            {
                GUILayout.BeginHorizontal();
                choix.evenementSuivant = EditorGUILayout.ObjectField("Evenement Suivant", choix.evenementSuivant,
                    typeof(Evenement), false) as Evenement;
                
                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Creer"))
                {
                    choix.evenementSuivant = CreerAssetNarration<Evenement>();
                }
                GUI.backgroundColor = couleurFondDefaut;
                
                GUILayout.EndHorizontal();
            }
            else
            {
                choix.montrerEvenementSuivant =
                    EditorGUILayout.Foldout(choix.montrerEvenementSuivant, "Evénement suivant - " + choix.evenementSuivant.name);

                if (choix.montrerEvenementSuivant)
                {
                    
                    GUILayout.BeginHorizontal();
                    if (choix.evenementSuivant.nomTemporaire == "")
                        choix.evenementSuivant.nomTemporaire = choix.evenementSuivant.name;
                    
                    choix.evenementSuivant.nomTemporaire = 
                        GUILayout.TextField(choix.evenementSuivant.nomTemporaire, 
                            GUILayout.Height(20));
                    
                    if (GUILayout.Button("Renomer", GUILayout.Height(20), GUILayout.Width(130)))
                    {
                        RenomerAssetNarration(choix.evenementSuivant, choix.evenementSuivant.nomTemporaire);
                    }
                    
                    GUILayout.EndHorizontal();
                    GUILayout.Space(15);
                    EvementEditor.DessinerInspecteur(choix.evenementSuivant);   
                }
                
                GUILayout.BeginHorizontal();
                GUI.backgroundColor = new Color32(255, 180, 0, 255);
                if (GUILayout.Button("Retirer"))
                {
                    choix.evenementSuivant = null;
                }
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Supprimer"))
                {   
                    SupprimerEvenementSuivant(choix);
                }

                GUI.backgroundColor = couleurFondDefaut;
                GUILayout.EndHorizontal();
            }
            
        }
        */
        
        /*private static void SupprimerEvenementSuivant(Choix choix )
        {
            if (EditorUtility.DisplayDialog("Supprimer Evenement", "T sûr de vouloir surpprimer " + 
                                                                   choix.evenementSuivant.name + " ?", "oui",
                "annuler"))
            {
                AssetDatabase.DeleteAsset(RecupChemin<Evenement>() + '/' + choix.evenementSuivant.name + ".asset");
                DestroyImmediate(choix.evenementSuivant, true);
                choix.evenementSuivant = null;
            }
        }
        */
    }
}