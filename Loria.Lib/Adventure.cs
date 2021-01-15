using System;
using System.Collections.Generic;

namespace Loria.Lib
{
    public class Adventure
    {
        public string Name { get; set; }
        public string HeroRules { get; set; }
        public string SkillRules { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
