using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTemplate.Models
{
   public enum Gender
    {
        FEMALE,
        MALE,
        UNKONW,
        LGBTQIAPKDXREW,
    }
    public  class Creature
    {
       public Gender Gender {  get; set; }

       public uint Age {  get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
