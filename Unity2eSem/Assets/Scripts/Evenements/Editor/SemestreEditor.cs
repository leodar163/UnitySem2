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
            
            DessinerInspecteur(semestre);
        }

        public static void DessinerInspecteur(Semestre semestre)
        {
               DessinerSauvegarde(semestre);
               GUILayout.Space(15);
               DessinerListeSemaine(semestre);
               semestre.NettoyerSemaines();
        }

        private static void DessinerListeSemaine(Semestre semestre)
        {
            for (int i = 0; i < semestre.Semaines.Count; i++)
            {
                Semaine semaine = semestre.Semaines[i];

                semestre.Semaines[i] = 
                    SemaineEditor.DessinerEmbedInspector(semaine, ref semestre.SemainesDeployes[i], "Semaine "+i);
                
                
                GUILayout.Space(10);

                if (i < semestre.Semaines.Count - 1)
                {
                    GUILayout.Button("", GUILayout.Height(2));
                }

                GUILayout.Space(10);
            }
        }
    }
}