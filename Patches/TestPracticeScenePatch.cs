using System;
using HarmonyLib;
using UnityEngine;

namespace TranslationMod.Patches
{
    /// <summary>
    /// Just for testing.
    /// </summary>
    public static class TestPracticeScenePatch
    {
        [HarmonyPatch(typeof(WhiteLabelMainMenu), "Start")]
        [HarmonyPostfix]
        private static void AddPracticeSceneButton()
        {
            new GameObject().AddComponent<PracticeSceneButton>();
        }

        private class PracticeSceneButton : MonoBehaviour
        {
            private void OnGUI()
            {
                float size = 64;
                if (GUI.Button(new Rect(0, Screen.height - size, size, size), "Goto Practice Scene"))
                {
                    LevelManager.LoadLevel("PracticeRoomWalkaround");
                }
                if (GUI.Button(new Rect(0, Screen.height - size*2, size, size), "Goto Train Station"))
                {
                    LevelManager.LoadLevel("C2_TrainStationNight");
                }
            }
        }
    }
}
