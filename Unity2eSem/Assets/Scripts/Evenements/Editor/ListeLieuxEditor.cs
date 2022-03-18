using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Evenements.Editor
{
    using Plan;
    public class ListeLieuxEditor : ScriptableNarrationEditor
    {
        public override void OnInspectorGUI()
        {
            ListeLieux lieux = target as ListeLieux;
            if(lieux == null) return;
            
            DessinerInspecteur(lieux, true);
        }

        public static void DessinerInspecteur(ListeLieux lieux, bool afficherSauvegarde = false)
        {
            if (afficherSauvegarde)
            {
                DessinerSauvegarde(lieux);
                GUILayout.Space(15);
            }
            DessinerListeLieux(lieux);
            AfficherDebugListeLieu(lieux);
        }

        private static void DessinerListeLieux(ListeLieux lieux)
        {
            Color couleurFondDefaut = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Ajouter Lieu"))
            {

                int indexNom = 0;

                while (lieux.Lieux.Find(lieu1 => lieu1.nom == "Lieu" + indexNom) != null)
                {
                    indexNom++;
                }

                string nomNvLieu = "Lieu" + indexNom;

                lieux.Lieux.Add(new Lieu(nomNvLieu));
            }

            GUI.backgroundColor = couleurFondDefaut;

            GUILayout.Space(15);

            for (int i = 0; i < lieux.Lieux.Count; i++)
            {
                Lieu lieu = lieux.Lieux[i];

                GUILayout.BeginHorizontal();
                GUILayout.Label(lieu.nom);

                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Supprimer", GUILayout.Width(130)))
                {
                    if (EditorUtility.DisplayDialog("Supprimer Lieu",
                        "T sûr de vouloir surpprimer le lieu \"" + lieu.nom + "\" ?" +
                        "\n(L'action ne peut pas être annulée)", "oui", "annuler"))
                    {
                        lieux.Lieux.Remove(lieu);
                        lieu = null;
                        return;
                    }
                }

                GUI.backgroundColor = couleurFondDefaut;

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (lieu.nomTemporaire == "")
                    lieu.nomTemporaire = lieu.nom;

                lieu.nomTemporaire =
                    GUILayout.TextField(lieu.nomTemporaire,
                        GUILayout.Height(20));

                if (GUILayout.Button("Renomer", GUILayout.Height(20), GUILayout.Width(130)))
                {
                    List<Lieu> listeTest =
                        lieux.Lieux.FindAll(lieu1 => lieu1.nom == lieu.nomTemporaire);

                    if (lieu.nomTemporaire.Length < 1 || listeTest.Count > 0)
                    {
                        if (EditorUtility.DisplayDialog("Nom invalide",
                            "Le nom choisi est soit trop court, soit existe déjà",
                            "Mince"))
                        {
                            return;
                        }
                    }
                    else
                    {
                        lieu.nom = lieu.nomTemporaire;
                    }
                }

                GUILayout.EndHorizontal();
                
                GUILayout.Space(5);

                lieu.position = EditorGUILayout.Vector2Field("Postition", lieu.position);
                
                GUILayout.Space(5);
                
                lieu.icone = EditorGUILayout.ObjectField("Icone", lieu.icone, 
                typeof(Sprite), false) as Sprite;
                
                GUILayout.Space(10);
                
                lieu.illustration = EditorGUILayout.ObjectField("Illustration", lieu.illustration,
                 typeof(Sprite), false) as Sprite;

                if (i < lieux.Lieux.Count - 1)
                {
                    GUILayout.Button("", GUILayout.Height(2));
                }

                GUILayout.Space(10);
            }
        }
        
        public static ListeLieux DessinerEmbedInspecteur(ListeLieux lieux, ref bool estDeploye,
            string label = "Lieux")
        {
            Color couleurFondDefaut = GUI.backgroundColor;

            CommencerZoneEmbed(Color.blue);           
            //Debug.Log("Commence");

            if (lieux == null)
            {
                GUILayout.BeginHorizontal();
                lieux = EditorGUILayout.ObjectField(label, lieux,
                    typeof(ListeLieux), false) as ListeLieux;

                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Créer"))
                {
                    lieux = CreerAssetNarration<ListeLieux>();
                }

                GUI.backgroundColor = couleurFondDefaut;

                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal();

                estDeploye =
                    EditorGUILayout.Foldout(estDeploye, label + " - " + lieux.name);

                GUILayoutOption[] optionsBoutons =
                {
                    GUILayout.Width(80)
                };

                if(Plan.Singleton.debugListeLieu) GUI.backgroundColor = Color.yellow;
                    
                if (GUILayout.Button("Visualiser", optionsBoutons))
                {
                    Plan.Singleton.debugListeLieu = !Plan.Singleton.debugListeLieu;
                    Plan.Singleton.semaineDebug = null;
                }   

                GUI.backgroundColor = new Color32(255, 180, 0, 255);
                if (GUILayout.Button("Retirer", optionsBoutons))
                {
                    if(EditorUtility.DisplayDialog("Retirer Liste Lieux",
                        "Retirer la liste va réinitialiser les lieux de tous les évènements. T sûr du coup ?",
                        "Allez !", "En fait non"))
                    {
                        return null;    
                    }
                    
                }

                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Supprimer", optionsBoutons))
                {
                    if(EditorUtility.DisplayDialog("Supprimer Liste Lieux",
                        "Supprimer la liste va réinitialiser les lieux de tous les évènements. T sûr du coup ?",
                        "Allez !", "En fait non"))
                    {
                        SupprimerAssetNarration(lieux);
                        return lieux;
                    }
                }

                GUI.backgroundColor = couleurFondDefaut;
                GUILayout.EndHorizontal();

                if (estDeploye)
                {
                    GUILayout.BeginHorizontal();
                    if (lieux.nomTemporaire == "")
                        lieux.nomTemporaire = lieux.name;

                    lieux.nomTemporaire =
                        GUILayout.TextField(lieux.nomTemporaire,
                            GUILayout.Height(20));

                    if (GUILayout.Button("Renommer", GUILayout.Height(20), GUILayout.Width(130)))
                    {
                        RenommerAssetNarration(lieux, lieux.nomTemporaire);
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.Space(15);
                    DessinerInspecteur(lieux);
                }
            }
            
            FinirZoneEmbed(Color.blue);
            //Debug.Log("fini");
            
            return lieux;
        }

        private static void AfficherDebugListeLieu(ListeLieux lieux)
        {
            if(Application.isPlaying) return;
            if (Plan.Singleton.debugListeLieu)
            {
                Plan.Singleton.ChargerListeLieux(lieux);
            }
            if (Plan.Singleton.semaineDebug == null && !Plan.Singleton.debugListeLieu)
            {
                Plan.Singleton.NettoyerPins();
            }
        }
    }
}