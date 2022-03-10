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
            
            DessinerInspecteur(semaine);
        }

        public static void DessinerInspecteur(Semaine semaine)
        {
            DessinerSauvegarde(semaine);
            GUILayout.Space(15);
            DessinerListeEvenements(semaine);
            semaine.NettoyerEvenementsDepart();
        }
        
        private static void DessinerListeEvenements(Semaine semaine)
        {
            Color couleurDefaut = GUI.backgroundColor;
            
            for (int i = 0; i < semaine.EvenementsDepart.Count; i++)
            {
                Evenement evenement = semaine.EvenementsDepart[i];

                semaine.EvenementsDepart[i] = 
                    EvenementEditor.DessinerEmbedInspector(evenement, ref semaine.EvenementsDeployes[i], "Evenement "+i);
                
                
                GUILayout.Space(7);

                if (i < semaine.EvenementsDepart.Count - 1)
                {
                    GUILayout.Button("", GUILayout.Height(2));
                }

                GUILayout.Space(7);
            }

            GUI.backgroundColor = couleurDefaut;
        }
    }
}