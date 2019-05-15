
namespace Feeders
{
    public abstract class DbCounter
    {
        public string InsertCounter = "IF NOT EXISTS (SELECT * FROM PWSEQ WHERE name = '{0}') insert into PWSEQ (Name, SeqNo) values ('{0}', 0)";
        public string SelectCounter = "SELECT TOP 1 * FROM PWSEQ WHERE name = '{0}'";
        public string UpdateCounter = "update PWSEQ set SeqNo =(SeqNo + {1}) where name = '{0}'";
        public abstract string CounterName();

        public string Insert()
        {
            return string.Format(InsertCounter, CounterName());
        }

        public string Select()
        {
            return string.Format(SelectCounter, CounterName());
        }

        public string Update(int value)
        {
            return string.Format(UpdateCounter, CounterName(), value);
        }

    }

    public class APMCounter : DbCounter
    {
        public override string CounterName()
        {
            return "PE001SEQ0";
        }
    }

    public class APPCounter : DbCounter
    {
        public override string CounterName()
        {
            return "PWCREWPORTAL";
        }
    }
}