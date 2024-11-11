using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAppTemplate.Models;

namespace WpfAppTemplate.Dictionary
{
    public class PeopleSelector:DataTemplateSelector
    {
        public DataTemplate Female {  get; set; }
        public DataTemplate Male { get; set; }
        public DataTemplate LGBTQIA_Puls { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is People people)
            {
                if (people.Gender == Gender.FEMALE)
                {
                    return Female;
                }
                else if(people.Gender == Gender.MALE)
                {
                    return Male;
                }
                else if(people.Gender == Gender.LGBTQIAPKDXREW)
                {
                    return LGBTQIA_Puls;
                }
            }
            return base.SelectTemplate(item, container);
        }
    }
}
