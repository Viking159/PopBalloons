using System;
using System.Collections.Generic;

namespace Features.Score
{
    /// <summary>
    /// Score data list wrapper
    /// </summary>
    [Serializable]
    public class ScoresData
    {
        public List<ScoreData> ScoreDatas = new List<ScoreData>();
    }
}
