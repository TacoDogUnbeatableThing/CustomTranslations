using System;
using BepInEx;
using HarmonyLib;
using TranslationMod.Patches;
using TranslationMod.Translation;
using UnityEngine;
using Yarn.Unity;

namespace TranslationMod
{
    [BepInPlugin("tacodog.unbeatable.translationmod", "Unbeatable Translation Mod", "1.0.0")]
    public class TranslationMod : BaseUnityPlugin
    {
        public static readonly string TRANSLATION_FILE = "UnbeatableTranslations.json";
        public static TranslationMod Instance { get; private set; }

        public TranslationLoader TranslationLoader;

        private void Awake()
        {
            Instance = this;

            Harmony.CreateAndPatchAll(typeof(YarnDialogPatch));
            Harmony.CreateAndPatchAll(typeof(TestPracticeScenePatch));

            TranslationLoader = new TranslationLoader(TRANSLATION_FILE, OnTranslationsReload);
        }

        private void OnTranslationsReload(TranslationData obj)
        {
            DialogueRunner[] runners = FindObjectsOfType<DialogueRunner>();
            Debug.Log($"RELOADING TRANSLATIONS FOR {runners.Length} DIALOGUE RUNNERS");
            foreach (DialogueRunner dialogueRunner in runners)
            {
                TranslationHelper.PatchInTranslations(dialogueRunner, TranslationLoader.Data);
            }
        }
    }
}
