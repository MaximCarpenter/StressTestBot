using System;
using System.Collections.Generic;
using DataFeeder;
using DataFeeder.Randomizers;
using Feeders;

namespace Test
{
    public class ConcreteFeeder : IDataFeeder 
    {
        public int CurrentAmountOfRows { get; set; }
        public string CurrentTableName { get; set; }
        public bool End { get; private set; }
        private readonly List<Func<string>> _actions = new List<Func<string>>();

        public int VesselId { get; set; }
        public List<int> AvailablePositions { get; set; } = new List<int>();
        public List<int> Pins { get; set; } = new List<int>();
        public List<string> AvailableRanks { get; set; } = new List<string>();
        public Dictionary<int, int> PinPosition { get; set; } = new Dictionary<int, int>();

        public ConcreteFeeder()
        {
            _actions.Add(C02);
            _actions.Add(C12);
            _actions.Add(Office);
            _actions.Add(Vessel);
            _actions.Add(Department);
            _actions.Add(Positions);
            _actions.Add(P01);
            _actions.Add(P0P);
            _actions.Add(P03);
        }

        private int _currentIndex;

        public string Next()
        {
            if (_currentIndex == _actions.Count)
            {
                End = true;
                return null;
            }

            var result = _actions[_currentIndex].Invoke();
            _currentIndex++;
            return result;
        }

        private string C02()
        {
            CurrentAmountOfRows = 1001;
            CurrentTableName = "C02";
            var c02 = new C02();
            var result = c02
                .WithRandom(new AlphabeticGenerator()) 
                .WithPrefix("RANK_")
                .Generate(CurrentAmountOfRows);
            AvailableRanks = c02.AvailableRanks;
            return result;
        }

        private string C12()
        {
            CurrentAmountOfRows = 2;
            CurrentTableName = "C12";
            return new C12().Generate();
        }

        private string Office()
        {
            CurrentAmountOfRows = 1;
            CurrentTableName = "PWORG";
            return new PWORG()
                .WithOrgType(2)
                .WithCounter(200)
                .WithParent(1)
                .WithRandom(new AlphabeticGenerator())
                .WithPrefix("ORG_")
                .Generate(CurrentAmountOfRows);
        }

        private string Vessel()
        {
            CurrentAmountOfRows = 1;
            CurrentTableName = "PWORG";
            VesselId = 201;
            return new PWORG()
                .WithOrgType(3)
                .WithCounter(VesselId)
                .WithRandom(new AlphabeticGenerator())
                .WithPrefix("VES_")
                .Generate(CurrentAmountOfRows);
        }

        private string Department()
        {
            CurrentAmountOfRows = 1;
            CurrentTableName = "PWORG";
            return new PWORG()
                .WithOrgType(4)
                .WithCounter(202)
                .WithRandom(new AlphabeticGenerator())
                .WithPrefix("DEP_")
                .Generate(CurrentAmountOfRows);
        }

        private string Positions()
        {
            CurrentAmountOfRows = 1001;
            CurrentTableName = "PWORG";
            var positionStart = 203;
            AvailablePositions = Range.Int(positionStart, positionStart + CurrentAmountOfRows);
            return new PWORG()
                .WithOrgType(5)
                .WithCounter(positionStart)
                .WithRandom(new AlphabeticGenerator())
                .WithPrefix("POS_")
                .Generate(CurrentAmountOfRows);
        }

        private string P01()
        {
            CurrentAmountOfRows = 1000;
            CurrentTableName = "P01";
            var positionStart = 1000;
            Pins = Range.Int(positionStart, positionStart + CurrentAmountOfRows);
            return new P01()
                .WithRanks(AvailableRanks)
                .WithCounter(positionStart)
                .WithRandom(new AlphabeticGenerator())
                .WithPrefix("NAME_")
                .Generate(CurrentAmountOfRows);
        }

        private string P0P()
        {
            CurrentAmountOfRows = 1000;
            CurrentTableName = "P0P";
            var positionStart = 1000;
            return new P01()
                .WithRanks(AvailableRanks)
                .WithCounter(positionStart)
                .Generate(CurrentAmountOfRows);
        }

        private string P03()
        {
            CurrentAmountOfRows = 1000;
            CurrentTableName = "P03";
            var positionStart = 1000;

            var p03 = new P03();
            var result = p03.WithPins(Pins)
                .WithPositions(AvailablePositions)
                .WithVessel(VesselId)
                .WithCounter(positionStart)
                .Generate(CurrentAmountOfRows);
            PinPosition = p03.PinPosition;
            return result;
        }
    }
}
