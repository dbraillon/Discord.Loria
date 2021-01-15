using Loria.Lib.Player;
using Loria.Lib.Randomiser;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loria.Lib
{
    public class HeroBuilder : IPlayer
    {
        protected virtual AdventureRandomiser Randomiser { get; }

        public HeroBuilder(AdventureRandomiser randomiser)
        {
            Randomiser = randomiser;
        }

        public virtual void Start()
        {
        }

        public virtual bool Continue(string input)
        {
        }
    }
}
