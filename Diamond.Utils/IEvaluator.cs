using System.Text.RegularExpressions;

namespace Diamond.Utils
{
    public interface IEvaluator
    {
        string RegexPattern { get; }
        long Priority { get; }
        string Evaluate(Match match);
    }
}
