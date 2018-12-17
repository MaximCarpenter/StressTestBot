using System;
using System.Collections.Generic;
using DataFeeder.Randomizers;

namespace Feeders
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
            return new C02().WithRandom(new AlphabeticRandomizer()).Generate(1001);
        }
    }
}
