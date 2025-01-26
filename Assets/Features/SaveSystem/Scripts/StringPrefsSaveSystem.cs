using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Features.SaveSystem
{
    /// <summary>
    /// Prefs save system
    /// </summary>
    public class StringPrefsSaveSystem : AbstractStringSaveSystem
    {
        public StringPrefsSaveSystem(SaveSystemInfo saveSystemInfo) : base(saveSystemInfo) { }

        public override async UniTask<string> LoadData(CancellationToken token = default)
        {
            try
            {
                return GetDecrypt(PlayerPrefs.GetString(saveInfo));
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(StringPrefsSaveSystem)}: Load data ex: {ex.Message}\n{ex.StackTrace}");
            }
            return string.Empty;
        }

        public override async UniTask SaveData(string data, CancellationToken token = default)
        {
            if (!string.IsNullOrEmpty(data))
            {
                PlayerPrefs.SetString(saveInfo, GetEncrypt(data));
            }
        }
    }
}
