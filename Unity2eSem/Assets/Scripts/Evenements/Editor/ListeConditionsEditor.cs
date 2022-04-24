using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Evenements.Editor
{
    [CustomEditor(typeof(ListeConditions))]
    public class ListeConditionsEditor : ScriptableNarrationEditor
    {
        public override void OnInspectorGUI()
        {
            ListeConditions conditions = target as ListeConditions;
            if (!conditions) return;
            DessinerInspecteur(conditions, true);
        }

        public static void DessinerInspecteur(ListeConditions conditions, bool afficherSauvegarde = false)
        {
            if (GUILayout.Button("Réinit liste"))
            {
                conditions.ReinitConditions();
            }
            if (afficherSauvegarde)
            {
                DessinerSauvegarde(conditions);
                GUILayout.Space(15);
            }
            DessinerListeCondition(conditions);
        }

        private static void DessinerListeCondition(ListeConditions conditions)
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Ajouter Condition"))
            {

                int indexNom = 0;

                while (conditions.Conditions.Find(condition => condition.nom == "Condition" + indexNom) != null)
                {
                    indexNom++;
                }

                string nomNvlleConditon = "Condition" + indexNom;

                conditions.Conditions.Add(new Condition(nomNvlleConditon));
            }

            GUI.backgroundColor = couleurFondDefaut;

            GUILayout.Space(15);

            for (int i = 0; i < conditions.Conditions.Count; i++)
            {
                Condition condition = conditions.Conditions[i];

                GUILayout.BeginHorizontal();
                GUILayout.Label(condition.nom);

                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Supprimer", GUILayout.Width(130)))
                {
                    if (EditorUtility.DisplayDialog("Supprimer Condition",
                        "T sûr de vouloir surpprimer la condition \"" + condition.nom + "\" ?" +
                        "\n(L'action ne peut pas être annulée)", "oui", "annuler"))
                    {
                        conditions.Conditions.Remove(condition);
                        break;
                    }
                }

                GUI.backgroundColor = couleurFondDefaut;

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (condition.nomTemporaire == "")
                    condition.nomTemporaire = condition.nom;

                condition.nomTemporaire =
                    GUILayout.TextField(condition.nomTemporaire,
                        GUILayout.Height(20));

                if (GUILayout.Button("Renomer", GUILayout.Height(20), GUILayout.Width(130)))
                {
                    List<Condition> listeTest =
                        conditions.Conditions.FindAll(condition1 => condition1.nom == condition.nomTemporaire);

                    if (condition.nomTemporaire.Length < 1 || listeTest.Count > 0)
                    {
                        EditorUtility.DisplayDialog("Nom invalide",
                            "Le nom choisi est soit trop court, soit existe déjà",
                            "Mince");
                    }
                    else
                    {
                        condition.nom = condition.nomTemporaire;
                    }
                }

                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                if (i < conditions.Conditions.Count - 1)
                {
                    GUILayout.Button("", GUILayout.Height(2));
                }

                GUILayout.Space(10);
            }
        }

        public static ListeConditions DessinerEmbedInspecteur(ListeConditions conditions, ref bool estDeploye,
            string label = "Conditions")
        {
            Color couleurFondDefaut = GUI.backgroundColor;

            CommencerZoneEmbed(Color.red);            

            if (conditions == null)
            {
                GUILayout.BeginHorizontal();
                conditions = EditorGUILayout.ObjectField(label, conditions,
                    typeof(ListeConditions), false) as ListeConditions;

                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Créer"))
                {
                    conditions = CreerAssetNarration<ListeConditions>();
                }

                GUI.backgroundColor = couleurFondDefaut;

                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal();

                estDeploye =
                    EditorGUILayout.Foldout(estDeploye, label + " - " + conditions.name);

                GUI.backgroundColor = new Color32(255, 180, 0, 255);

                GUILayoutOption[] optionsBoutons =
                {
                    GUILayout.Width(80)
                };

                if (GUILayout.Button("Retirer", optionsBoutons))
                {
                    if(EditorUtility.DisplayDialog("Retirer Liste Conditions",
                        "Retirer la liste va réinitialiser toutes les conditions des choix. T sûr du coup ?",
                        "Allez !", "En fait non"))
                    {
                        conditions.retired = true;
                        return null;    
                    }
                    
                }

                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Supprimer", optionsBoutons))
                {
                    if(EditorUtility.DisplayDialog("Supprimer Liste Conditions",
                        "Supprimer la liste va réinitialiser toutes les conditions des choix. T sûr du coup ?",
                        "Allez !", "En fait non"))
                    {
                        SupprimerAssetNarration(conditions);
                        conditions.retired = true;
                        return conditions;
                    }
                }

                GUI.backgroundColor = couleurFondDefaut;
                GUILayout.EndHorizontal();

                if (estDeploye)
                {

                    GUILayout.BeginHorizontal();
                    if (conditions.nomTemporaire == "")
                        conditions.nomTemporaire = conditions.name;

                    conditions.nomTemporaire =
                        GUILayout.TextField(conditions.nomTemporaire,
                            GUILayout.Height(20));

                    if (GUILayout.Button("Renomer", GUILayout.Height(20), GUILayout.Width(130)))
                    {
                        RenommerAssetNarration(conditions, conditions.nomTemporaire);
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.Space(15);
                    DessinerInspecteur(conditions);
                }
            }
            
            FinirZoneEmbed(Color.red);
            
            return conditions;
        }
    }
}