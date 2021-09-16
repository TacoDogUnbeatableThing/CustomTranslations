using System;
using System.IO;
using Rewired.Utils.Libraries.TinyJson;

namespace TranslationMod.Translation
{
    public class TranslationLoader
    {
        private readonly string _filename;
        public TranslationData Data { get; private set; }

        private readonly Action<TranslationData> _onReload;

        public TranslationLoader(string filename, Action<TranslationData> onReload)
        {
            _filename = filename;
            _onReload = onReload;
            if (Reload())
            {
                FileWatchHelper.WatchFile(filename, OnFileChanged);
            }
        }

        private bool Reload()
        {
            if (File.Exists(_filename))
            {
                Data = JsonParser.FromJson<TranslationData>(File.ReadAllText(_filename));
                return true;
            }

            return false;
        }

        private void OnFileChanged(string _)
        {
            if (Reload())
            {
                _onReload?.Invoke(Data);
            }
        }
    }
}
