using Loria.Lib.Randomiser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loria.Lib
{
    public class Hero
    {
        public AdventureRandomiser Randomiser { get; set; }

        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int CurrentStamina { get; set; }
        public List<Skill> Skills { get; set; }
        public string Weapon { get; set; }
        public int Meal { get; set; }
        public int Gold { get; set; }

        public Hero(AdventureRandomiser randomiser)
        {
            Randomiser = randomiser;

            Strength = Randomiser.Draw() + 10;
            Stamina = Randomiser.Draw() + 20;
            CurrentStamina = Stamina;
            Skills = new List<Skill>();
            Weapon = "Hache";
            Meal = 1;
            Gold = Randomiser.Draw();
        }

        public void Learn(Skill skill)
        {
            Skills.Add(skill);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                $"Endurance : {CurrentStamina}/{Stamina}",
                $"Habilité : {Strength}",
                $"Repas : {Meal}",
                $"Or : {Gold}",
                $"Arme : {Weapon}",
                $"Compétences : {string.Join(", ", Skills.Select(skill => skill.Name))}"
            );
        }
    }
}
