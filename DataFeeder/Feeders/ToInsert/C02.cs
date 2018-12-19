using System;
using System.Collections.Generic;

namespace Feeders
{
    public class C02 : DataToInsert
    {
        protected override string Insert => "INSERT [dbo].[PW001C02] ([CODE], [GROUPNO], [CREATED], [CREATEDBY], [SEQUENCENO], [CODETYPE], [NAME], [OPTIONS]) VALUES";
        protected override string Values => " (N'{0}', 0, getdate(), N'script', {1}, N' ', N'{2}', N' ')";
        public List<string> AvailableRanks { get; set; } = new List<string>();

        protected override Func<string> InsertForm()
        {
            return () =>
            {
                AvailableRanks.Add(_randomCode.Next());
                return string.Format(Values, _randomCode.Current(), 1, Prefix + _randomCode.Current());
            };
        }

        public override string Generate(int count)
        {
            return GenerateInsert(0, count);
        }
    }
}
