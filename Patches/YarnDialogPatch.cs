using HarmonyLib;
using UnityEngine;
using Yarn.Unity;

namespace TranslationMod.Patches
{
    public static class YarnDialogPatch
    {


        [HarmonyPatch(typeof(DialogueRunner), "Awake")]
        [HarmonyPostfix]
        private static void AddCustomProject(DialogueRunner __instance)
        {
            var local = __instance.yarnProject.baseLocalization;
            foreach (string key in local.GetLineIDs())
            {
                local.AddLocalizedString(key, "Beat: Sussy Balls.");
            }
            Debug.Log($"REPLACED: {local.LocaleCode}");
        }
    }
}
