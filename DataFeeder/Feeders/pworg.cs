using System;
using DataFeeder.Randomizers;

namespace Feeders
{
    public class PWORG
    {
        public const string Insert = "INSERT [dbo].[PW001C02] ([CODE], [GROUPNO], [CREATED], [CREATEDBY], [SEQUENCENO], [CODETYPE], [NAME], [OPTIONS]) VALUES";
        public const string OrgPrefix = "ORG_";
        public const string VesselPrefix = "VES_";
        public const string DepartmentPrefix = "DEP_";
        public const string PositionPrefix = "POS_";
        private const string Values = " (N'{0}', 0, getdate(), N'script', {1}, N' ', N'{2}', N' ')";
        private IAlphabeticRandomizer _randomCode;
        private int _insertLimit;

        public PWORG WithRandom(IAlphabeticRandomizer random)
        {
            _randomCode = new AlphabeticRandomizer();
            return this;
        }

        //1 concern, 100 departments, 1000 vessels, 5000 department 10000 positions

        public string Generate(int count, int positions)
        {
            var result = Insert;

            return result;
        }
    }
}
