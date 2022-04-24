using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Evenements.Editor
{
    [CustomEditor(typeof(Semestre))]
    public class SemestreEditor : ScriptableNarrationEditor
    {
        public override void OnInspectorGUI()
        {
            Semestre semestre = target as Semestre;
            if(!semestre) return;
            
            DessinerInspecteur(semestre, true);
        }

        public static void DessinerInspecteur(Semestre semestre, bool afficherSauvegarde = false)
        {
               if (afficherSauvegarde)
               {
                   DessinerSauvegarde(semestre);
               }
            
               DessinerConditions(semestre);
               GUILayout.Space(15);
               DessinerLieux(semestre);
               GUILayout.Space(15);
               DessinerListeSemaine(semestre);
               semestre.NettoyerSemaines();
        }

        private static void DessinerConditions(Semestre semestre)
        {
            semestre.conditions = ListeConditionsEditor.DessinerEmbedInspecteur(semestre.conditions, ref semestre.ConditionsDeployees,
                semestre.conditions != null ? semestre.conditions.name : "Conditions");
            if(!semestre.conditions) SupprimerConditions(semestre);
            else if (semestre.conditions.retired)
            {
                semestre.conditions.retired = false;
                SupprimerConditions(semestre);
            }
        }

        private static void DessinerLieux(Semestre semestre)
        {
            semestre.lieux = ListeLieuxEditor.DessinerEmbedInspecteur(semestre.lieux, ref semestre.LieuxDeployees,
                semestre.lieux != null ? semestre.lieux.name : "Lieux");
        }
        
        private static void DessinerListeSemaine(Semestre semestre)
        {
            for (int i = 0; i < semestre.Semaines.Count; i++)
            {
                Semaine semaine = semestre.Semaines[i];

                semestre.Semaines[i] = 
                    SemaineEditor.DessinerEmbedInspector(semaine, ref semestre.SemainesDeployes[i], 
                        semestre.conditions, semestre.lieux, "Semaine "+i);
                
                
                GUILayout.Space(10);

                if (i < semestre.Semaines.Count - 1)
                {
                    GUILayout.Button("", GUILayout.Height(2));
                }

                GUILayout.Space(10);
            }
        }

        public static Semestre DessinerEmbedInspector(Semestre semestre, ref bool estDeploye, string label = "Semestre")
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            
            CommencerZoneEmbed(Color.grey);
            
            if (semestre == null)
            {
                GUILayout.BeginHorizontal();
                semestre = EditorGUILayout.ObjectField(label, semestre,
                    typeof(Semestre), false) as Semestre;
                
                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Creer"))
                {
                    semestre = CreerAssetNarration<Semestre>();
                }
                GUI.backgroundColor = couleurFondDefaut;
                
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal();
                
                estDeploye =
                    EditorGUILayout.Foldout(estDeploye, label + " - " + semestre.name);
                
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
                    SupprimerAssetNarration(semestre);
                    return semestre;
                }

                GUI.backgroundColor = couleurFondDefaut;
                GUILayout.EndHorizontal();

                if (estDeploye)
                {
                    GUILayout.BeginHorizontal();
                    if (semestre.nomTemporaire == "")
                        semestre.nomTemporaire = semestre.name;
                    
                    semestre.nomTemporaire = 
                        GUILayout.TextField(semestre.nomTemporaire, 
                            GUILayout.Height(20));
                    
                    if (GUILayout.Button("Renommer", GUILayout.Height(20), GUILayout.Width(130)))
                    {
                        RenommerAssetNarration(semestre, semestre.nomTemporaire);
                    }
                    
                    GUILayout.EndHorizontal();
                    GUILayout.Space(15);
                    
                    DessinerInspecteur(semestre);   
                }
            }
            
            FinirZoneEmbed(Color.grey);
            return semestre;
        }

        private static void SupprimerConditions(Semestre semestre)
        {
            foreach (var semaine in semestre.Semaines)
            {
                if(!semaine) continue;
                foreach (var desc in semaine.descriptions)
                {
                    desc.conditions.Clear();
                }

                foreach (var evenement in semaine.EvenementsDepart)
                {
 
                    if(!evenement) continue;
                    evenement.conditions.Clear();
                    
                    foreach (var choix in evenement.listeChoix)
                    {
                        choix.NettoyezConditions();
                    }
                }
            }
        }
    }
}