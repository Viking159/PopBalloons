using Features.Extensions;
using System;
using UnityEngine;

namespace Features.Score
{
    /// <summary>
    /// Converts string and ScoreData
    /// </summary>
    public class ScoresConverter : IConverter<string, ScoresData>
    {
        public string ConvertFrom(ScoresData converting)
        {
            if (converting != null)
            {
                return JsonUtility.ToJson(converting);
            }
            return string.Empty;
        }

        public ScoresData ConvertTo(string converting)
        {
            try
            {
                return JsonUtility.FromJson<ScoresData>(converting);
            }
            catch (Exception ex)
            {
                Debug.Log($"{nameof(ScoresConverter)}: Convert to {nameof(ScoresData)} ex: {ex.Message}\n{ex.StackTrace}");
                return null;
            }
        }
    }
}
