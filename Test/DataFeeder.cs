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
      //      _actions.Add(C02);
        //    _actions.Add(C12);
            _actions.Add(PWORG);
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
            return new C02().WithRandom(new AlphabeticRandomizer()).Generate(CurrentAmountOfRows);
        }

        private string C12()
        {
            CurrentAmountOfRows = 2;
            CurrentTableName = "C12";
            return new C12().Generate();
        }

        private string PWORG()
        {
            CurrentAmountOfRows = 10000;
            CurrentTableName = "PWORG";
            return new PWORG().WithRandom(new AlphabeticRandomizer()).Generate(CurrentAmountOfRows, 10000, 9999);
        }
    }
}
