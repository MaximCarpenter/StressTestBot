using System;
using DataFeeder.Randomizers;

namespace Feeders
{
    public class P01
    {
        public const string Insert = "INSERT [dbo].[PW001C02] ([CODE], [GROUPNO], [CREATED], [CREATEDBY], [SEQUENCENO], [CODETYPE], [NAME], [OPTIONS]) VALUES";
        public const string NamePrefix = "PERSON_";
        private const string Values = " (N'{0}', 0, getdate(), N'script', {1}, N' ', N'{2}', N' ')";
        private IAlphabeticRandomizer _randomCode;
        private int _insertLimit;

        public P01 WithRandom(IAlphabeticRandomizer random)
        {
            _randomCode = new AlphabeticRandomizer();
            return this;
        }

        public string Generate(int count)
        {
            var result = Insert;
            for (var i = 0; i < count; i++)
            {
                result += Environment.NewLine;
                result += string.Format(Values, _randomCode.Next(), 1, NamePrefix + _randomCode.Current());
                if (i != count - 1 && _insertLimit < 999) result += ",";
                if (_insertLimit == 999)
                {
                    _insertLimit = 0;
                    result += Environment.NewLine;
                    result += Insert;
                }
                else
                    _insertLimit++;
            }

            return result;
        }
    }
}
