using System;
using System.Collections.Generic;

namespace Feeders
{
    public class P0P : DataToInsert
    {
        protected override string Insert => "INSERT [dbo].[PW001P0P] ([PIN], [PNUMBER], [POSITIONID], " +
                                            "[SENIORITYYEARS], [POSITIONFRACTION], [PAYSCALETABLE], [PAYSCALECODE], " +
                                            "[SEQUENCENO], [SESSIONID], [DBACTION], [AUDIT_LINENR], [CREATEDBY], " +
                                            "[HISTORICAL], [SEA_SENIORITY_PRE], [CREATETIME])  VALUES ";
        protected override string Values => " ({0}, N'A', N'{1}',N'', N'', N'', N'', {0},  1, N'UPDATE', 1, N'script',  N'F', CAST(0.0000000000 AS Decimal(19, 10)), getdate())";

        private List<string> Ranks = new List<string>();
        private int _counter;

        public P0P WithCounter(int counter)
        {
            _counter = counter;
            return this;
        }

        public P0P WithRanks(List<string> ranks)
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
            return () => string.Format(Values, IterationCounter, RandomRank());
        }

        public override string Generate(int count)
        {
            return GenerateInsert(_counter, count);
        }
    }
}
