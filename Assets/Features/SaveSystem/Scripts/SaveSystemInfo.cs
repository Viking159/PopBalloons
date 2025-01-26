using System;
using UnityEngine;

namespace Features.SaveSystem
{
    [Serializable]
    public class SaveSystemInfo
    {
        /// <summary>
        /// Location (prefsKey/file name/URL)
        /// </summary>
        [Header("Location (prefsKey/file name/URL)")]
        public string SaveInfo = "Data.txt";
        /// <summary>
        /// Password for encrypt and decrypt
        /// </summary>
        [Header("Password for encrypt and decrypt")]
        public string Password = string.Empty;
    }
}
