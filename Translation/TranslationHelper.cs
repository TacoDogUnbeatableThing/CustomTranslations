using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using Rewired.Utils.Libraries.TinyJson;
using UnityEngine;
using Yarn.Unity;

namespace TranslationMod.Translation
{
    public static class TranslationHelper
    {
        private static readonly FieldInfo STRING_TABLE_FIELD = typeof(Localization).GetField("_stringTable", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void PatchInTranslations(DialogueRunner dialogue, TranslationData data)
        {
            if (data != null)
            {
                var local = dialogue.yarnProject.baseLocalization;
                SerializedDictionary<string, string> dict = (SerializedDictionary<string, string>) STRING_TABLE_FIELD.GetValue(local);
                foreach (string key in data.Dialogue.Keys)
                {
                    dict.Remove(key);
                    string set = data.Dialogue[key];
                    //Debug.Log($"SET: {key} -> {set}");
                    dict.Add(key, set);
                }
            }
            else
            {
                Debug.Log("NO TRANSLATION FILE FOUND!");
            }
        }

        public static void SaveAllCurrentDialogueToFile(Localization localization, string fname)
        {
            TranslationData test = new TranslationData();

            foreach (string id in localization.GetLineIDs())
            {
                test.Dialogue[id] = localization.GetLocalizedString(id);
            }

            Debug.Log($"Saved dialogue to {fname}");

            string jsonText = JsonWriter.ToJson(test);
            jsonText = jsonText.Replace("{", "\n{");
            jsonText = jsonText.Replace("}", "\n}");
            jsonText = jsonText.Replace("\",\"line:", "\",\n\"line:");

            jsonText = EscapeQuotes(jsonText);

            File.WriteAllText(fname, jsonText);
        }

        private static char GetChar(string text, int index)
        {
            if (index >= text.Length || index < 0)
            {
                return '\0';
            }
            return text[index];
        }
        private static string EscapeQuotes(string original)
        {
            StringBuilder builder = new StringBuilder(original.Length);

            bool inner = false;
            char prev = '\0';
            for (int i = 0; i < original.Length; ++i)
            {
                char c = GetChar(original, i);
                if (inner)
                {
                    if (c == '"')
                    {
                        if (
                            (GetChar(original, i+1) == ',' && GetChar(original, i+2) == '\n')
                            || GetChar(original, i+1) == '\n')
                        {
                            inner = false;
                        }
                        else
                        {
                            // Escape
                            builder.Append('\\');
                        }
                    }
                }
                else
                {
                    if (c == '"' && prev == ':')
                    {
                        inner = true;
                    }
                }

                builder.Append(c);

                prev = c;
            }

            return builder.ToString();
        }
    }
}
