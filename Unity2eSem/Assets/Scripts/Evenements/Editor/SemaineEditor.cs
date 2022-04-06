using System.Collections.Generic;

using UnityEngine;

namespace Evenements.Editor
{
    using UnityEditor;
    using Plan;
    
    [CustomEditor(typeof(Semaine))]
    public class SemaineEditor : ScriptableNarrationEditor
    {
        public override void OnInspectorGUI()
        {
            Semaine semaine = target as Semaine;
            if(!semaine) return;
            
            DessinerInspecteur(semaine, null, null, true);
        }

        public static void DessinerInspecteur(Semaine semaine, ListeConditions conditions,
            ListeLieux lieux, bool afficherSauvegarde = false)
        {
            if(afficherSauvegarde)
            {
                DessinerSauvegarde(semaine);
                GUILayout.Space(15);
            }
            DessinerListeEvenements(semaine, conditions, lieux);
            semaine.NettoyerEvenementsDepart();
            AfficherDebugSemaine(semaine);
        }
        
        private static void DessinerListeEvenements(Semaine semaine, ListeConditions conditions, ListeLieux lieux)
        {
            
            List<Lieu> lieuxDispos = lieux ? new List<Lieu>(lieux.Lieux) : null;
                
            for (int i = 0; i < semaine.EvenementsDepart.Count; i++)
            {
                Evenement evenement = semaine.EvenementsDepart[i];

                semaine.EvenementsDepart[i] = 
                    EvenementEditor.DessinerEmbedInspector(evenement, ref semaine.EvenementsDeployes[i], conditions, 
                        lieuxDispos, lieux, "Evenement "+i);
                
                if (evenement && evenement.lieu != null && lieuxDispos != null) 
                    lieuxDispos.Remove(evenement.lieu);
                
                GUILayout.Space(10);

                if (i < semaine.EvenementsDepart.Count - 1)
                {
                    GUILayout.Button("", GUILayout.Height(2));
                }

                GUILayout.Space(10);
            }
        }
        
        public static Semaine DessinerEmbedInspector(Semaine semaine, ref bool estDeploye, ListeConditions conditions,
            ListeLieux lieux, string label = "Semaine")
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            
            CommencerZoneEmbed(Color.cyan);
            
            if (semaine == null)
            {
                GUILayout.BeginHorizontal();
                semaine = EditorGUILayout.ObjectField(label, semaine,
                    typeof(Semaine), false) as Semaine;
                
                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Creer"))
                {
                    semaine = CreerAssetNarration<Semaine>();
                }
                GUI.backgroundColor = couleurFondDefaut;
                
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal();
                
                estDeploye =
                    EditorGUILayout.Foldout(estDeploye, label + " - " + semaine.name);

                GUILayoutOption[] optionsBoutons =
                {
                    GUILayout.Width(80)
                };
                
                if(!Plan.Singleton.debugListeLieu && Plan.Singleton.semaineDebug == semaine) 
                    GUI.backgroundColor = Color.yellow;
                    
                if (GUILayout.Button("Visualiser", optionsBoutons))
                {
                    Plan.Singleton.semaineDebug = Plan.Singleton.semaineDebug == null || Plan.Singleton.semaineDebug != semaine ?
                        semaine : null;
                    Plan.Singleton.debugListeLieu = false;
                }

                GUI.backgroundColor = new Color32(255, 180, 0, 255);
                
                if (GUILayout.Button("Retirer", optionsBoutons))
                {
                    return null;
                }
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Supprimer", optionsBoutons))
                {   
                    SupprimerAssetNarration(semaine);
                    return semaine;
                }

                GUI.backgroundColor = couleurFondDefaut;
                GUILayout.EndHorizontal();

                if (estDeploye)
                {
                    GUILayout.BeginHorizontal();
                    if (semaine.nomTemporaire == "")
                        semaine.nomTemporaire = semaine.name;
                    
                    semaine.nomTemporaire = 
                        GUILayout.TextField(semaine.nomTemporaire, 
                            GUILayout.Height(20));
                    
                    if (GUILayout.Button("Renomer", GUILayout.Height(20), GUILayout.Width(130)))
                    {
                        RenommerAssetNarration(semaine, semaine.nomTemporaire);
                    }
                    
                    GUILayout.EndHorizontal();
                    GUILayout.Space(15);
                    
                    DessinerInspecteur(semaine, conditions, lieux);   
                }
            }
            
            FinirZoneEmbed(Color.cyan);
            return semaine;
        }

        private static void AfficherDebugSemaine(Semaine semaine)
        {
            if (Application.isPlaying) return;
            if (Plan.Singleton.semaineDebug == semaine && !Plan.Singleton.debugListeLieu)
            {
                Plan.Singleton.ChargerSemaine(semaine, true);
            }

            if (Plan.Singleton.semaineDebug == null && !Plan.Singleton.debugListeLieu)
            {
                Plan.Singleton.NettoyerPins();
            }
        }
    }
}