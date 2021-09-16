using System;
using BepInEx;
using HarmonyLib;
using TranslationMod.Patches;
using UnityEngine;

namespace TranslationMod
{
    [BepInPlugin("tacodog.unbeatable.translationmod", "Unbeatable Translation Mod", "1.0.0")]
    public class TranslationMod : BaseUnityPlugin
    {
        public static TranslationMod Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            Debug.Log($"LOADED: {Info.Metadata.Name}");

            Harmony.CreateAndPatchAll(typeof(YarnDialogPatch));
            Harmony.CreateAndPatchAll(typeof(TestPracticeScenePatch));
        }
    }

}
