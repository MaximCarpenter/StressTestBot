namespace DataFeeder.Randomizers
{
    public class AlphabeticRandomizer : IAlphabeticRandomizer
    {
        private string _currentChar;
        private int _currentIndex = -1;

        public string Current()
        {
            return _currentChar;
        }

        public string Next()
        {
            if (_currentChar == null || _currentChar.Contains("Z"))
            {
                _currentIndex++;
                _currentChar = "A" + _currentIndex;
            }
            else
            {
                var nextChar = (char)(_currentChar[0] + 1);
                _currentChar = nextChar.ToString() + _currentIndex;
            }

            return _currentChar;
        }
    }
}
