using System;

namespace Feeders
{
    public class C12
    {
        public const string Insert = "INSERT [dbo].[PW001C12] ([CODE], [GROUPNO], [CREATED], [CREATEDBY], [CHANGEDBY], [SEQUENCENO], [CODETYPE], [TEXT], [LEAVEPAYFACTOR], [COLOR], [TRANSACTIONCODE], [OPTIONS], [ACTIVITYCLASS], [LEAVEDAYSDEDUCTEDFACTOR], [LEAVEDAYSACCUMULATORDEDUCTED], [PAYROLL_ACTGRP],  [MEASUREMENT], [IS_EARNING]) VALUES";
        private const string Values = " (N'{0}', 0, getdate(), N'script',  N'',  1, N'B', N'Leave', N'', N'', N'', N' {1}',N'', N'', N'', 0, 3, N'Y')";

        public string Generate()
        {
            var result = Insert;
            result += Environment.NewLine;
            result += string.Format(Values, "LV1", "V");
            result += ", ";
            result += string.Format(Values, "ONB1", "S");

            return result;
        }
    }
}
