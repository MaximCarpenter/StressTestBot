using System;
using System.Collections.Generic;

namespace Feeders
{
    public class P03 : DataToInsert
    {
        protected override string Insert => "INSERT [dbo].[PW001P03] ([PIN], [CREATEDBY], [CREATETIME], [HISTORICAL], " +
                                            "[GROUPNO], [SEQUENCENO], [CODE], [DATEFROM], [DATETO], [VESSEL], " +
                                            "[NUMORGID], [RANK])  VALUES ";
        protected override string Values => " ({0}, N'script', '2017-05-24', N'T', 0, {5}, N'ONB1', '{1}', {2}, {3}, {4}, N'A1')";

        private List<int> Pins = new List<int>();
        private int _vessel;
        private List<int> Positions = new List<int>();
        public Dictionary<int, int> PinPosition { get; set; } = new Dictionary<int, int>();
        private int _counter;

        public P03 WithVessel(int vessel)
        {
            _vessel = vessel;
            return this;
        }

        public P03 WithCounter(int counter)
        {
            _counter = counter;
            return this;
        }

        public P03 WithPins(List<int> pins)
        {
            Pins = pins;
            return this;
        }

        public P03 WithPositions(List<int> positions)
        {
            Positions = positions;
            return this;
        }

        private int RandomPosition(int pin)
        {
            if (PinPosition.ContainsKey(pin))
                return PinPosition[pin];
            var rnd = new Random();
            var position = Positions[rnd.Next(Positions.Count)];
            PinPosition.Add(pin, position);
            return position;
        }

        protected override Func<string> InsertForm()
        {
            return () => string.Format(Values, IterationCounter, DateTime.Today.AddDays(-5), "NULL", _vessel,
                RandomPosition(IterationCounter), IterationCounter);
        }

        public override string Generate(int count)
        {
            return GenerateInsert(_counter, count);
        }
    }
}
