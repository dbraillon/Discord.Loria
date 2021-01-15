using Loria.Lib.Randomiser;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Loria.Lib.Player
{
    public class AdventurePlayer
    {
        public AdventureRandomiser Randomiser { get; set; }
        public Adventure Adventure { get; set; }
        public Hero Hero { get; set; }

        public Event CurrentEvent { get; set; }
        public Action<string> Speak { get; set; }

        public AdventurePlayer(Adventure adventure, Action<string> speak)
        {
            Randomiser = new AdventureRandomiser();
            Adventure = adventure;
            Hero = new Hero(Randomiser);

            CurrentEvent = adventure.Events.First();
            Speak = speak;
        }

        /// <summary>
        /// Start a new adventure or load the previous one.
        /// </summary>
        public void Play()
        {
            Speak($"Bienvenue dans \"{Adventure.Name}\" !");
            Speak(string.Empty);

            Speak($"La génération du héro est automatique :");
            Speak(Hero.ToString());
            Speak(string.Empty);

            Play(CurrentEvent);
        }

        public void Play(Event @event)
        {
            CurrentEvent = @event;

            Speak(@event.Text);
            Fight(@event);
        }

        public bool Continue(string input)
        {
            var possibilities = Regex.Matches(CurrentEvent.Text, @"\[(\d+)\]").OfType<Match>();

            if (possibilities.Any(p => p.Value.Replace("[", "").Replace("]", "") == input))
            {
                Play(Adventure.Events.FirstOrDefault(e => e.Id.ToString() == input));
            }
            else
            {
                Speak("Mauvais choix, réessaye.");
            }

            return input != "quit";
        }

        public void Fight(Event @event)
        {
            var fights = Regex.Matches(@event.Text, @"\{(\d+)\}").OfType<Match>();
            if (!fights.Any()) return;

            foreach (var fight in fights)
            {
                var strength = int.Parse(Regex.Match(fight.Value, @"HABILETÉ : (\d+)").Groups[1].Value);
                var stamina = int.Parse(Regex.Match(fight.Value, @"ENDURANCE : (\d+)").Groups[1].Value);

                var attack = Hero.Strength - strength;

                while (Hero.CurrentStamina <= 0 || stamina <= 0)
                {
                    var value = Randomiser.Draw();

                    Hero.CurrentStamina -= value switch
                    {
                        1 when attack <= -11 => Hero.CurrentStamina,
                        1 when attack >= -10 && attack <= -9 => Hero.CurrentStamina,
                        1 when attack >= -8 && attack <= -7 => 8,
                        1 when attack >= -6 && attack <= -5 => 6,
                        1 when attack >= -4 && attack <= -3 => 6,
                        1 when attack >= -2 && attack <= -1 => 5,
                        1 when attack == 0 => 5,
                        1 when attack >= 1 && attack <= 2 => 5,
                        1 when attack >= 3 && attack <= 4 => 4,
                        1 when attack >= 5 && attack <= 6 => 4,
                        1 when attack >= 7 && attack <= 8 => 4,
                        1 when attack >= 9 && attack <= 10 => 3,
                        1 when attack >= 11 => 3,

                        2 when attack <= -11 => Hero.CurrentStamina,
                        2 when attack >= -10 && attack <= -9 => 8,
                        2 when attack >= -8 && attack <= -7 => 7,
                        2 when attack >= -6 && attack <= -5 => 6,
                        2 when attack >= -4 && attack <= -3 => 5,
                        2 when attack >= -2 && attack <= -1 => 5,
                        2 when attack == 0 => 4,
                        2 when attack >= 1 && attack <= 2 => 4,
                        2 when attack >= 3 && attack <= 4 => 3,
                        2 when attack >= 5 && attack <= 6 => 3,
                        2 when attack >= 7 && attack <= 8 => 3,
                        2 when attack >= 9 && attack <= 10 => 3,
                        2 when attack >= 11 => 2,

                        3 when attack <= -11 => 8,
                        3 when attack >= -10 && attack <= -9 => 7,
                        3 when attack >= -8 && attack <= -7 => 6,
                        3 when attack >= -6 && attack <= -5 => 5,
                        3 when attack >= -4 && attack <= -3 => 5,
                        3 when attack >= -2 && attack <= -1 => 4,
                        3 when attack == 0 => 4,
                        3 when attack >= 1 && attack <= 2 => 3,
                        3 when attack >= 3 && attack <= 4 => 3,
                        3 when attack >= 5 && attack <= 6 => 3,
                        3 when attack >= 7 && attack <= 8 => 2,
                        3 when attack >= 9 && attack <= 10 => 2,
                        3 when attack >= 11 => 2,

                        4 when attack <= -11 => 8,
                        4 when attack >= -10 && attack <= -9 => 7,
                        4 when attack >= -8 && attack <= -7 => 6,
                        4 when attack >= -6 && attack <= -5 => 5,
                        4 when attack >= -4 && attack <= -3 => 4,
                        4 when attack >= -2 && attack <= -1 => 4,
                        4 when attack == 0 => 3,
                        4 when attack >= 1 && attack <= 2 => 3,
                        4 when attack >= 3 && attack <= 4 => 2,
                        4 when attack >= 5 && attack <= 6 => 2,
                        4 when attack >= 7 && attack <= 8 => 2,
                        4 when attack >= 9 && attack <= 10 => 2,
                        4 when attack >= 11 => 2,

                        5 when attack <= -11 => 7,
                        5 when attack >= -10 && attack <= -9 => 6,
                        5 when attack >= -8 && attack <= -7 => 5,
                        5 when attack >= -6 && attack <= -5 => 4,
                        5 when attack >= -4 && attack <= -3 => 4,
                        5 when attack >= -2 && attack <= -1 => 3,
                        5 when attack == 0 => 2,
                        5 when attack >= 1 && attack <= 2 => 2,
                        5 when attack >= 3 && attack <= 4 => 2,
                        5 when attack >= 5 && attack <= 6 => 2,
                        5 when attack >= 7 && attack <= 8 => 2,
                        5 when attack >= 9 && attack <= 10 => 2,
                        5 when attack >= 11 => 1,

                        6 when attack <= -11 => 6,
                        6 when attack >= -10 && attack <= -9 => 6,
                        6 when attack >= -8 && attack <= -7 => 5,
                        6 when attack >= -6 && attack <= -5 => 4,
                        6 when attack >= -4 && attack <= -3 => 3,
                        6 when attack >= -2 && attack <= -1 => 2,
                        6 when attack == 0 => 2,
                        6 when attack >= 1 && attack <= 2 => 2,
                        6 when attack >= 3 && attack <= 4 => 2,
                        6 when attack >= 5 && attack <= 6 => 1,
                        6 when attack >= 7 && attack <= 8 => 1,
                        6 when attack >= 9 && attack <= 10 => 1,
                        6 when attack >= 11 => 1,

                        7 when attack <= -11 => 5,
                        7 when attack >= -10 && attack <= -9 => 5,
                        7 when attack >= -8 && attack <= -7 => 4,
                        7 when attack >= -6 && attack <= -5 => 3,
                        7 when attack >= -4 && attack <= -3 => 2,
                        7 when attack >= -2 && attack <= -1 => 2,
                        7 when attack == 0 => 1,
                        7 when attack >= 1 && attack <= 2 => 1,
                        7 when attack >= 3 && attack <= 4 => 1,
                        7 when attack >= 5 && attack <= 6 => 0,
                        7 when attack >= 7 && attack <= 8 => 0,
                        7 when attack >= 9 && attack <= 10 => 0,
                        7 when attack >= 11 => 0,

                        8 when attack <= -11 => 4,
                        8 when attack >= -10 && attack <= -9 => 4,
                        8 when attack >= -8 && attack <= -7 => 3,
                        8 when attack >= -6 && attack <= -5 => 2,
                        8 when attack >= -4 && attack <= -3 => 1,
                        8 when attack >= -2 && attack <= -1 => 1,
                        8 when attack == 0 => 0,
                        8 when attack >= 1 && attack <= 2 => 0,
                        8 when attack >= 3 && attack <= 4 => 0,
                        8 when attack >= 5 && attack <= 6 => 0,
                        8 when attack >= 7 && attack <= 8 => 0,
                        8 when attack >= 9 && attack <= 10 => 0,
                        8 when attack >= 11 => 0,

                        9 when attack <= -11 => 3,
                        9 when attack >= -10 && attack <= -9 => 3,
                        9 when attack >= -8 && attack <= -7 => 2,
                        9 when attack >= -6 && attack <= -5 => 0,
                        9 when attack >= -4 && attack <= -3 => 0,
                        9 when attack >= -2 && attack <= -1 => 0,
                        9 when attack == 0 => 0,
                        9 when attack >= 1 && attack <= 2 => 0,
                        9 when attack >= 3 && attack <= 4 => 0,
                        9 when attack >= 5 && attack <= 6 => 0,
                        9 when attack >= 7 && attack <= 8 => 0,
                        9 when attack >= 9 && attack <= 10 => 0,
                        9 when attack >= 11 => 0,

                        0 when attack <= -11 => 0,
                        0 when attack >= -10 && attack <= -9 => 0,
                        0 when attack >= -8 && attack <= -7 => 0,
                        0 when attack >= -6 && attack <= -5 => 0,
                        0 when attack >= -4 && attack <= -3 => 0,
                        0 when attack >= -2 && attack <= -1 => 0,
                        0 when attack == 0 => 0,
                        0 when attack >= 1 && attack <= 2 => 0,
                        0 when attack >= 3 && attack <= 4 => 0,
                        0 when attack >= 5 && attack <= 6 => 0,
                        0 when attack >= 7 && attack <= 8 => 0,
                        0 when attack >= 9 && attack <= 10 => 0,
                        0 when attack >= 11 => 0,

                        _ => 1
                    };

                    stamina -= value switch
                    {
                        1 when attack <= -11 => 0,
                        1 when attack >= -10 && attack <= -9 => 0,
                        1 when attack >= -8 && attack <= -7 => 0,
                        1 when attack >= -6 && attack <= -5 => 0,
                        1 when attack >= -4 && attack <= -3 => 1,
                        1 when attack >= -2 && attack <= -1 => 2,
                        1 when attack == 0 => 3,
                        1 when attack >= 1 && attack <= 2 => 4,
                        1 when attack >= 3 && attack <= 4 => 5,
                        1 when attack >= 5 && attack <= 6 => 6,
                        1 when attack >= 7 && attack <= 8 => 7,
                        1 when attack >= 9 && attack <= 10 => 8,
                        1 when attack >= 11 => 9,

                        2 when attack <= -11 => 0,
                        2 when attack >= -10 && attack <= -9 => 0,
                        2 when attack >= -8 && attack <= -7 => 0,
                        2 when attack >= -6 && attack <= -5 => 1,
                        2 when attack >= -4 && attack <= -3 => 2,
                        2 when attack >= -2 && attack <= -1 => 3,
                        2 when attack == 0 => 4,
                        2 when attack >= 1 && attack <= 2 => 5,
                        2 when attack >= 3 && attack <= 4 => 6,
                        2 when attack >= 5 && attack <= 6 => 7,
                        2 when attack >= 7 && attack <= 8 => 8,
                        2 when attack >= 9 && attack <= 10 => 9,
                        2 when attack >= 11 => 10,

                        3 when attack <= -11 => 0,
                        3 when attack >= -10 && attack <= -9 => 0,
                        3 when attack >= -8 && attack <= -7 => 1,
                        3 when attack >= -6 && attack <= -5 => 2,
                        3 when attack >= -4 && attack <= -3 => 3,
                        3 when attack >= -2 && attack <= -1 => 4,
                        3 when attack == 0 => 5,
                        3 when attack >= 1 && attack <= 2 => 6,
                        3 when attack >= 3 && attack <= 4 => 7,
                        3 when attack >= 5 && attack <= 6 => 8,
                        3 when attack >= 7 && attack <= 8 => 9,
                        3 when attack >= 9 && attack <= 10 => 10,
                        3 when attack >= 11 => 11,

                        4 when attack <= -11 => 0,
                        4 when attack >= -10 && attack <= -9 => 1,
                        4 when attack >= -8 && attack <= -7 => 2,
                        4 when attack >= -6 && attack <= -5 => 3,
                        4 when attack >= -4 && attack <= -3 => 4,
                        4 when attack >= -2 && attack <= -1 => 5,
                        4 when attack == 0 => 6,
                        4 when attack >= 1 && attack <= 2 => 7,
                        4 when attack >= 3 && attack <= 4 => 8,
                        4 when attack >= 5 && attack <= 6 => 9,
                        4 when attack >= 7 && attack <= 8 => 10,
                        4 when attack >= 9 && attack <= 10 => 11,
                        4 when attack >= 11 => 12,

                        5 when attack <= -11 => 1,
                        5 when attack >= -10 && attack <= -9 => 2,
                        5 when attack >= -8 && attack <= -7 => 3,
                        5 when attack >= -6 && attack <= -5 => 4,
                        5 when attack >= -4 && attack <= -3 => 5,
                        5 when attack >= -2 && attack <= -1 => 6,
                        5 when attack == 0 => 7,
                        5 when attack >= 1 && attack <= 2 => 8,
                        5 when attack >= 3 && attack <= 4 => 9,
                        5 when attack >= 5 && attack <= 6 => 10,
                        5 when attack >= 7 && attack <= 8 => 11,
                        5 when attack >= 9 && attack <= 10 => 12,
                        5 when attack >= 11 => 14,

                        6 when attack <= -11 => 2,
                        6 when attack >= -10 && attack <= -9 => 3,
                        6 when attack >= -8 && attack <= -7 => 4,
                        6 when attack >= -6 && attack <= -5 => 5,
                        6 when attack >= -4 && attack <= -3 => 6,
                        6 when attack >= -2 && attack <= -1 => 7,
                        6 when attack == 0 => 8,
                        6 when attack >= 1 && attack <= 2 => 9,
                        6 when attack >= 3 && attack <= 4 => 10,
                        6 when attack >= 5 && attack <= 6 => 11,
                        6 when attack >= 7 && attack <= 8 => 12,
                        6 when attack >= 9 && attack <= 10 => 14,
                        6 when attack >= 11 => 16,

                        7 when attack <= -11 => 3,
                        7 when attack >= -10 && attack <= -9 => 4,
                        7 when attack >= -8 && attack <= -7 => 5,
                        7 when attack >= -6 && attack <= -5 => 6,
                        7 when attack >= -4 && attack <= -3 => 7,
                        7 when attack >= -2 && attack <= -1 => 8,
                        7 when attack == 0 => 9,
                        7 when attack >= 1 && attack <= 2 => 10,
                        7 when attack >= 3 && attack <= 4 => 11,
                        7 when attack >= 5 && attack <= 6 => 12,
                        7 when attack >= 7 && attack <= 8 => 14,
                        7 when attack >= 9 && attack <= 10 => 16,
                        7 when attack >= 11 => 18,

                        8 when attack <= -11 => 4,
                        8 when attack >= -10 && attack <= -9 => 5,
                        8 when attack >= -8 && attack <= -7 => 6,
                        8 when attack >= -6 && attack <= -5 => 7,
                        8 when attack >= -4 && attack <= -3 => 8,
                        8 when attack >= -2 && attack <= -1 => 9,
                        8 when attack == 0 => 10,
                        8 when attack >= 1 && attack <= 2 => 11,
                        8 when attack >= 3 && attack <= 4 => 12,
                        8 when attack >= 5 && attack <= 6 => 14,
                        8 when attack >= 7 && attack <= 8 => 16,
                        8 when attack >= 9 && attack <= 10 => 18,
                        8 when attack >= 11 => stamina,

                        9 when attack <= -11 => 5,
                        9 when attack >= -10 && attack <= -9 => 6,
                        9 when attack >= -8 && attack <= -7 => 7,
                        9 when attack >= -6 && attack <= -5 => 8,
                        9 when attack >= -4 && attack <= -3 => 9,
                        9 when attack >= -2 && attack <= -1 => 10,
                        9 when attack == 0 => 11,
                        9 when attack >= 1 && attack <= 2 => 12,
                        9 when attack >= 3 && attack <= 4 => 14,
                        9 when attack >= 5 && attack <= 6 => 16,
                        9 when attack >= 7 && attack <= 8 => 18,
                        9 when attack >= 9 && attack <= 10 => stamina,
                        9 when attack >= 11 => stamina,

                        0 when attack <= -11 => 6,
                        0 when attack >= -10 && attack <= -9 => 7,
                        0 when attack >= -8 && attack <= -7 => 8,
                        0 when attack >= -6 && attack <= -5 => 9,
                        0 when attack >= -4 && attack <= -3 => 10,
                        0 when attack >= -2 && attack <= -1 => 11,
                        0 when attack == 0 => 12,
                        0 when attack >= 1 && attack <= 2 => 14,
                        0 when attack >= 3 && attack <= 4 => 16,
                        0 when attack >= 5 && attack <= 6 => 18,
                        0 when attack >= 7 && attack <= 8 => stamina,
                        0 when attack >= 9 && attack <= 10 => stamina,
                        0 when attack >= 11 => stamina,

                        _ => 1
                    };
                }
            }
        }
    }
}
