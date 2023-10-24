using System.Text.RegularExpressions;

namespace Diamond.Utils
{
    public class RegexEvaluator
    {
        private RegexEvaluator()
        {

        }

        public static readonly RegexEvaluator Current = new RegexEvaluator
        {
            Evaluators = new List<IEvaluator> { new DateTimeEvaluator { } }
        };

        public IEnumerable<IEvaluator> Evaluators { get; set; }
        public string EvaluateString(string str)//Evaluates the entire string without conflicting results. (One evaluator does not overlap other ones when matching patterns, and also when replacing the evaluation result with another pattern.)
        {
            var evaluators = Evaluators.OrderBy(x => x.Priority).ToList(); //Sort By Priority (Higher priority stays on the end of the list)
            var matchers = new List<dynamic>();
            foreach (var evaluator in evaluators)
            {
                var matches = Regex.Matches(str, evaluator.RegexPattern);
                foreach (Match match in matches)
                {
                    matchers.RemoveAll(x =>//Due to prevent overlapping matching patterns by two or more evaluators.
                        IndexesHaveConflict(match.Index, match.Index + match.Length, x.Index, x.Index + x.Length));//Removes conflicting previous matches (which has lower priority - because the list is sorted by priority number)
                    var sourceString = match.Value;
                    var resultString = Regex.Replace(sourceString, evaluator.RegexPattern, evaluator.Evaluate);
                    matchers.Add(new
                    {
                        match.Index,
                        match.Length,
                        Evaluator = evaluator,
                        ResultString = resultString
                    });
                }
            }

            var matchersOrdered = matchers.OrderBy(x => x.Index).ToList();
            var prevIndex = 0;
            var newStr = string.Empty;
            for (int i = 0; i < matchersOrdered.Count; i++)//Due to prevent conflicts (if an evaluator matches the previous evaluator's result).
            {
                var matcher = matchersOrdered[i];
                string prevString = matcher.Index - 1 > prevIndex
                    ? str.Substring(prevIndex, matcher.Index - prevIndex)
                    : string.Empty;
                newStr = newStr + prevString + matcher.ResultString;
                prevIndex = matcher.Index + matcher.Length;
            }

            if (prevIndex < str.Length - 1)
            {
                newStr = newStr + str.Substring(prevIndex);
            }

            return newStr;
        }

        private bool IndexesHaveConflict(int index1Start, int index1End, int index2Start, int index2End)
        {
            return index1Start < index2End && index2Start < index1End;
        }

    }
}
