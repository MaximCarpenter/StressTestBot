using System;

namespace Feeders
{
    public class PWORG: DataToInsert
    {
        protected override string Insert => "INSERT [dbo].[PWORG] ([ORGCODE], [NAME], [ORGTYPE], [NUMORGID], [NUMORGIDABOVE], [CREATEDBY], [CREATETIME], [SORTING]) VALUES";
        protected override string Values => "(N'{0}', N'{1}', N'{2}', {3}, {4}, N'script', getdate(), 0)";

        protected override Func<string> InsertForm()
        {
            return () => string.Format(Values, _randomCode.Next(), Prefix + _randomCode.Current(), _orgType, IterationCounter, GetParent());
        }

        private int GetParent()
        {
            if (_parent != -1) return _parent;
            return IterationCounter - 1;
        }

        private int _counter;
        private int _orgType;
        private int _parent = -1;

        public PWORG WithCounter(int counter)
        {
            _counter = counter;
            return this;
        }

        public PWORG WithOrgType(int orgType)
        {
            _orgType = orgType;
            return this;
        }

        public PWORG WithParent(int parent)
        {
            _parent = parent;
            return this;
        }

        public override string Generate(int count)
        {
            return GenerateInsert(_counter, count);
        }
    }
}
