using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppDictionary.Models
{
    internal class Card
    {
        public override string ToString()
        {
            return $"{MPValue},{Name},...";
        }

        public Card(MP mp, string name, string skill, Priority priority, string victoryStr)
        {
            MPValue = mp;
            Name = name;
            Skill_Description = skill;
            PriorityValue = priority;
            V_Condition = victoryStr;
        }

        public enum MP
        {
            NEGATIVE_ONE = -1,
            ZERO,
            ONE,
            TWO,
            THREE,
        }

        public enum Priority
        {
            ONE = 1,
            TWO,
            THREE,
            FOUR,
            FIVE,
        }

        public MP MPValue { get; set; }
        public string Name { get; set; }
        public Priority PriorityValue { get; set; }
        public string Skill_Description { get; set; }
        public string V_Condition { get; set; }
    }
}