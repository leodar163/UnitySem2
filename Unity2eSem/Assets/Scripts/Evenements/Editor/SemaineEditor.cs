using UnityEngine;

namespace Evenements.Editor
{
    using UnityEditor;
    
    [CustomEditor(typeof(Semaine))]
    public class SemaineEditor : ScriptableNarrationEditor
    {
        public override void OnInspectorGUI()
        {
            Semaine semaine = target as Semaine;
            if(!semaine) return;
            
            DessinerInspecteur(semaine, null, true);
        }

        public static void DessinerInspecteur(Semaine semaine, ListeConditions conditions, bool afficherSauvegarde = false)
        {
            if(afficherSauvegarde)
            {
                DessinerSauvegarde(semaine);
                GUILayout.Space(15);
            }
            DessinerListeEvenements(semaine, conditions);
            semaine.NettoyerEvenementsDepart();
        }
        
        private static void DessinerListeEvenements(Semaine semaine, ListeConditions conditions)
        {
            for (int i = 0; i < semaine.EvenementsDepart.Count; i++)
            {
                Evenement evenement = semaine.EvenementsDepart[i];

                semaine.EvenementsDepart[i] = 
                    EvenementEditor.DessinerEmbedInspector(evenement, ref semaine.EvenementsDeployes[i], conditions, "Evenement "+i);
                
                
                GUILayout.Space(10);

                if (i < semaine.EvenementsDepart.Count - 1)
                {
                    GUILayout.Button("", GUILayout.Height(2));
                }

                GUILayout.Space(10);
            }
        }
        
        public static Semaine DessinerEmbedInspector(Semaine semaine, ref bool estDeploye, ListeConditions conditions, string label = "Semaine")
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            GUILayout.BeginVertical();
            
            GUI.backgroundColor = Color.cyan;
            GUILayout.Button("", GUILayout.Height(15));
            GUI.backgroundColor = couleurFondDefaut;
            
            if (semaine == null)
            {
                GUILayout.BeginHorizontal();
                semaine = EditorGUILayout.ObjectField(label, semaine,
                    typeof(Evenement), false) as Semaine;
                
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
                
                GUI.backgroundColor = new Color32(255, 180, 0, 255);

                GUILayoutOption[] optionsBoutons =
                {
                    GUILayout.Width(80)
                };
                
                if (GUILayout.Button("Retirer", optionsBoutons))
                {
                    semaine = null;
                    return null;
                }
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Supprimer", optionsBoutons))
                {   
                    SupprimerAssetNarration(semaine);
                    return null;
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
                        RenomerAssetNarration(semaine, semaine.nomTemporaire);
                    }
                    
                    GUILayout.EndHorizontal();
                    GUILayout.Space(15);
                    
                    DessinerInspecteur(semaine, conditions);   
                }
            }
            
            GUI.backgroundColor = Color.cyan;
            GUILayout.Button("", GUILayout.Height(5));
            GUI.backgroundColor = couleurFondDefaut;
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            return semaine;
        }
    }
}