using System;

namespace Feeders
{
    public class C02 : DataToInsert
    {
        protected override string Insert => "INSERT [dbo].[PW001C02] ([CODE], [GROUPNO], [CREATED], [CREATEDBY], [SEQUENCENO], [CODETYPE], [NAME], [OPTIONS]) VALUES";
        protected override string Values => " (N'{0}', 0, getdate(), N'script', {1}, N' ', N'{2}', N' ')";

        protected override Func<string> InsertForm()
        {
            return () => string.Format(Values, _randomCode.Next(), 1, Prefix + _randomCode.Current());
        }

        public override string Generate(int count)
        {
            return GenerateInsert(0, count);
        }
    }
}
