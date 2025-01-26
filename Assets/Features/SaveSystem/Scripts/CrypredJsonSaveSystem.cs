using Cysharp.Threading.Tasks;
using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Features.SaveSystem
{
    public class CrypredJsonSaveSystem : AbstractStringSaveSystem
    {
        protected string fileName = string.Empty;
        protected string path = string.Empty;
        protected string folderPath = string.Empty;
        protected string fullPath = string.Empty;

        public CrypredJsonSaveSystem(SaveSystemInfo saveSystemInfo) : base(saveSystemInfo)
            => ParsePath();

        public override async UniTask<string> LoadData(CancellationToken token = default)
        {
            try
            {
                return GetDecrypt(await File.ReadAllTextAsync(fullPath, token));
            }
            catch (Exception ex)
            {
                if (ex is not OperationCanceledException)
                {
                    Debug.LogError($"{nameof(CrypredJsonSaveSystem)}: Load data ex: {ex.Message}\n{ex.StackTrace}");
                }
            }
            return string.Empty;
        }

        public override async UniTask SaveData(string data, CancellationToken token = default)
        {
            try
            {
                CreateDirectory();
                await File.WriteAllTextAsync(fullPath, GetEncrypt(data), token);
            }
            catch (Exception ex)
            {
                if (ex is not OperationCanceledException)
                {
                    Debug.LogError($"{nameof(CrypredJsonSaveSystem)}: Save data ex: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }
        
        protected virtual void CreateDirectory()
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        protected virtual void ParsePath()
        {
            path = string.Empty;
            int index = saveInfo.LastIndexOf('/');
            if (index != -1)
            {
                fileName = saveInfo.Substring(index + 1);
                path = saveInfo.Substring(0, index);
            }
            else
            {
                fileName = saveInfo;
            }
            folderPath = Path.Combine(Application.persistentDataPath, path);
            fullPath = Path.Combine(Application.persistentDataPath, path, fileName);
        }
    }
}
