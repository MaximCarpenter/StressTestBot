using System;
using DataFeeder.Randomizers;

namespace Feeders
{
    public class PWORG
    {
        public const string Insert = "INSERT [dbo].[PWORG] ([ORGCODE], [NAME], [ORGTYPE], [NUMORGID], [NUMORGIDABOVE], [CREATEDBY], [CREATETIME], [SORTING]) VALUES";
        public const string OrgPrefix = "ORG_";
        public const string VesselPrefix = "VES_";
        public const string DepartmentPrefix = "DEP_";
        public const string PositionPrefix = "POS_";
        private const string Values = "(N'{0}', N'{1}', N'{2}', {3}, {4}, N'script', getdate(), 0)";
        private IAlphabeticRandomizer _randomCode;
        private int _insertLimit;

        public PWORG WithRandom(IAlphabeticRandomizer random)
        {
            _randomCode = new AlphabeticRandomizer();
            return this;
        }

        //1 concern, 1 departments, 1 vessels, department 1 
        public string Generate(int count, int positions, int activeVessel)
        {
            var result = Insert;
            var nextPositions = activeVessel + 1;

            result += string.Format(Values, _randomCode.Next(), OrgPrefix + _randomCode.Current(), 2, 2, 1);
            result += ",";
            result += Environment.NewLine;

            result += string.Format(Values, _randomCode.Next(), VesselPrefix + _randomCode.Current(), 3, activeVessel, 2);
            result += ",";
            result += Environment.NewLine;

            result += string.Format(Values, _randomCode.Next(), DepartmentPrefix + _randomCode.Current(), 4, 3, activeVessel);
            result += ",";
            result += Environment.NewLine;

            for (nextPositions = 0; nextPositions < (positions + activeVessel); nextPositions++)
            {
                result += Environment.NewLine;
                result += string.Format(Values, _randomCode.Next(), PositionPrefix + _randomCode.Current(), 5,
                    nextPositions, 3);
                if (nextPositions != (activeVessel + count - 1) && _insertLimit < 996) result += ",";
                if (_insertLimit == 996)
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
