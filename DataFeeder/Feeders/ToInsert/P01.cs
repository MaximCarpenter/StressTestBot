using System;
using System.Collections.Generic;

namespace Feeders
{
    public class P01 : DataToInsert
    {
        protected override string Insert => "INSERT [dbo].[PW001P01] ([PIN], [CREATEDBY], [CREATETIME], [NAME], [EMPLOYMENTSTARTDATE], [RANK], [SEQUENCENO]) VALUES ";
        protected override string Values => " ({0}, 'script', getdate(), '{1}', '2010-01-01', '{2}', {0})";

        private List<string> Ranks = new List<string>();
        private int _counter;

        public P01 WithCounter(int counter)
        {
            _counter = counter;
            return this;
        }

        public P01 WithRanks(List<string> ranks)
        {
            Ranks = ranks;
            return this;
        }

        private string RandomRank()
        {
            var rnd = new Random();
            return Ranks[rnd.Next(Ranks.Count)];
        }

        protected override Func<string> InsertForm()
        {
            return () => string.Format(Values, IterationCounter, Prefix + RandomCode.Current(), RandomRank());
        }

        public override string Generate(int count)
        {
            return GenerateInsert(_counter, count);
        }
    }
}
