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
            DessinerListeDescription(semaine, conditions);
            DessinerListeEvenements(semaine, conditions, lieux);
            semaine.NettoyerEvenementsDepart();
            AfficherDebugSemaine(semaine);
        }
        
        private static void DessinerListeDescription(Semaine semaine, ListeConditions conditions)
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            Rect rectVerti = EditorGUILayout.BeginVertical();

            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Ajouter Description"))
            {
                semaine.descriptions.Add(new Semaine.Description());
            }
            GUI.backgroundColor = couleurFondDefaut;
            
            for (int i = 0; i < semaine.descriptions.Count; i++)
            {
                Rect rectElementListe = EditorGUILayout.BeginHorizontal();

                GUILayout.BeginVertical();
                int tailleBoutons = 20;
                
                if (i != 0 && 
                         GUILayout.Button("^", GUILayout.Width(tailleBoutons)))
                {
                    Semaine.Description description = semaine.descriptions[i];
                    semaine.descriptions.RemoveAt(i);
                    
                    semaine.descriptions.Insert(i - 1, description);

                    GUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                    return;
                } 
                if (i != semaine.descriptions.Count - 1 && 
                      GUILayout.Button("ˇ",GUILayout.Width(tailleBoutons)))
                {
                    Semaine.Description description = semaine.descriptions[i];
                    semaine.descriptions.RemoveAt(i);
                    semaine.descriptions.Insert(i + 1 , description);
                    
                    GUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                    return;
                }
                GUILayout.EndVertical();
                
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("X",GUILayout.Width(tailleBoutons)))
                {
                    semaine.descriptions.RemoveAt(i);
                    GUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                    return;
                }
                GUI.backgroundColor = couleurFondDefaut;

                GUILayout.BeginVertical();
                GUILayout.Label("Desctiption");
                semaine.descriptions[i].description = GUILayout.TextArea(semaine.descriptions[i].description);
                
                GUILayout.Space(10);
                
                GUILayout.Label("Conditions");

                if (conditions)
                {
                    List<Condition> conditionsDescription = semaine.descriptions[i].conditions;
                    
                    string[] conditionsDispo = conditions.RecupNomsConditionsExlusif(conditionsDescription);
                
                    for (int j = 0; j < conditionsDescription.Count; j++)
                    {
                        GUILayout.BeginHorizontal();
                        string[] conditionsDispoPlusUn = new string[conditionsDispo.Length + 1];
                        conditionsDispoPlusUn[0] = conditionsDescription[j].nom;
                        conditionsDispo.CopyTo(conditionsDispoPlusUn, 1);

                        int indexSelecetion = 0;
                        indexSelecetion = EditorGUILayout.Popup(
                            indexSelecetion, conditionsDispoPlusUn);
                        conditionsDescription[j] = conditions.RecupCondition(conditionsDispoPlusUn[indexSelecetion]);

                        GUI.backgroundColor = Color.red;
                        if (GUILayout.Button("Supprimer"))
                        {
                            conditionsDescription.RemoveAt(j);
                            GUILayout.EndHorizontal();
                            GUILayout.EndVertical();
                            break;
                        }
                        GUI.backgroundColor = couleurFondDefaut;
                    
                        GUILayout.EndHorizontal();
                    } 
                
                    GUI.backgroundColor = Color.green;
                    if (conditions.Conditions.Count > conditionsDescription.Count && 
                        GUILayout.Button("Ajouter Condition"))
                    {
                        Condition conditionDefaut = conditions.RecupCondition(
                            conditions.RecupNomsConditionsExlusif(conditionsDescription)[0]);
                    
                        conditionsDescription.Add(conditionDefaut);
                    }
                    GUI.backgroundColor = couleurFondDefaut;
                    GUILayout.Space(10);
                }
                else
                {
                    GUILayout.Label("Il n'y a pas de liste de condition");
                }
                GUILayout.EndVertical();
                
                
                EditeurNivoFenetre.DessinerCarre(rectElementListe, Color.black);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }
            
            EditeurNivoFenetre.DessinerCarre(rectVerti,Color.black);

            EditorGUILayout.EndVertical();
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