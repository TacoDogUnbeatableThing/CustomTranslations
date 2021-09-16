using System.IO;
using HarmonyLib;
using TranslationMod.Translation;
using UnityEngine;
using Yarn.Unity;

namespace TranslationMod.Patches
{
    public static class YarnDialogPatch
    {

        public static readonly string UTILITY_ORIGINAL_FILENAME = "UnbeatableTranslations_ORIGINAL.json";

        [HarmonyPatch(typeof(DialogueRunner), "Awake")]
        [HarmonyPostfix]
        private static void AddCustomProject(DialogueRunner __instance)
        {
            if (!File.Exists(UTILITY_ORIGINAL_FILENAME))
            {
                TranslationHelper.SaveAllCurrentDialogueToFile(__instance.yarnProject.baseLocalization, UTILITY_ORIGINAL_FILENAME);
            }
            TranslationHelper.PatchInTranslations(__instance, TranslationMod.Instance.TranslationLoader.Data);
        }
    }
}
