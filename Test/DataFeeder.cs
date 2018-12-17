using System;
using System.Collections.Generic;
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

        public ConcreteFeeder()
        {
            _actions.Add(C02);
            _actions.Add(C12);
            _actions.Add(Office);
            _actions.Add(Vessel);
            _actions.Add(Department);
            _actions.Add(Positions);
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
            return new C02()
                .WithRandom(new AlphabeticRandomizer())
                .WithPrefix("RANK_")
                .Generate(CurrentAmountOfRows);
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
                .WithRandom(new AlphabeticRandomizer())
                .WithPrefix("ORG_")
                .Generate(CurrentAmountOfRows);
        }

        private string Vessel()
        {
            CurrentAmountOfRows = 1;
            CurrentTableName = "PWORG";
            return new PWORG()
                .WithOrgType(3)
                .WithCounter(201)
                .WithRandom(new AlphabeticRandomizer())
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
                .WithRandom(new AlphabeticRandomizer())
                .WithPrefix("DEP_")
                .Generate(CurrentAmountOfRows);
        }

        private string Positions()
        {
            CurrentAmountOfRows = 1001;
            CurrentTableName = "PWORG";
            return new PWORG()
                .WithOrgType(5)
                .WithCounter(203)
                .WithRandom(new AlphabeticRandomizer())
                .WithPrefix("POS_")
                .Generate(CurrentAmountOfRows);
        }
    }
}
