using Cysharp.Threading.Tasks;
using System.Threading;
using UnityCipher;

namespace Features.SaveSystem
{
    public abstract class AbstractStringSaveSystem : ISaveSystem<string>
    {
        protected string saveInfo = string.Empty;
        protected string password = string.Empty;

        public AbstractStringSaveSystem(SaveSystemInfo saveSystemInfo)
        {
            saveInfo = saveSystemInfo.SaveInfo;
            password = saveSystemInfo.Password;
        }

        public abstract UniTask<string> LoadData(CancellationToken token = default);

        public abstract UniTask SaveData(string data, CancellationToken token = default);

        protected virtual string GetEncrypt(string text)
        {
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(password))
            {
                return RijndaelEncryption.Encrypt(text, password);
            }
            return text;
        }

        protected virtual string GetDecrypt(string text)
        {
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(password))
            {
                return RijndaelEncryption.Decrypt(text, password);
            }
            return text;
        }
    }
}
