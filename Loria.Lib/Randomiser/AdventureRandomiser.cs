using System;

namespace Loria.Lib.Randomiser
{
    public class AdventureRandomiser
    {
        protected Random Random { get; }

        public AdventureRandomiser()
        {
            Random = new Random();
        }

        public int Draw()
        {
            return Draw(0, 9);
        }

        public int Draw(int maxValue)
        {
            return Random.Next(maxValue);
        }

        public int Draw(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }
    }
}
