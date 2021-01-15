using System;
using System.Collections.Generic;
using System.Text;

namespace Loria.Lib.Player
{
    public interface IPlayer
    {
        void Start();
        bool Continue(string input);
    }
}
