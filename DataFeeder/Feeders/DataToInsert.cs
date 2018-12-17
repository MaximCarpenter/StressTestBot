using System;
using DataFeeder.Randomizers;

namespace Feeders
{
    public abstract class DataToInsert
    {
        protected abstract string Insert { get; }
        protected abstract string Values { get; }
        protected string Prefix { get; private set; }

        protected IAlphabeticRandomizer _randomCode;
        protected const int InsertLimit = 999;
        protected int InsertCounter = 0;
        protected int IterationCounter = 0;

        public DataToInsert WithRandom(IAlphabeticRandomizer random)
        {
            _randomCode = new AlphabeticRandomizer();
            return this;
        }

        public DataToInsert WithPrefix(string prefix)
        {
            Prefix = prefix;
            return this;
        }

        protected abstract Func<string> InsertForm();

        protected string GenerateInsert(int start, int count)
        {
            var result = Insert;
            count += start;
            for (IterationCounter = start; IterationCounter < count; IterationCounter++)
            {
                result += Environment.NewLine;
                result += InsertForm().Invoke();
                
                if (IterationCounter != count - 1)
                {
                    if (InsertCounter < InsertLimit)
                    {
                        result += ",";
                        InsertCounter++;
                    }
                    else
                    {
                        InsertCounter = 0;
                        result += Environment.NewLine;
                        result += Insert;
                    }
                }
            }

            return result;
        }

        public abstract string Generate(int count);
    }
}