using Features.Extensions;

namespace Features.Score
{
    public sealed class ScoreComparer : AbstractComparer<ScoreData>
    {
        public ScoreComparer() : this(false) { }

        public ScoreComparer(bool ascending) : base(ascending) { }

        protected override int CompareDatas(ScoreData score1, ScoreData score2)
        {
            if (score1 != null)
            {
                if (score2 != null)
                {
                    return score1.Score.CompareTo(score2.Score);
                }
                return MORE_VALUE;
            }
            return score2 == null ? EQUAL_VALUE : LESS_VALUE;
        }
    }
}
