using CommunityToolkit.Mvvm.ComponentModel;
using WpfAppDictionary.Models;
using WpfAppTemplate.Models;

namespace WpfAppDictionary.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<Card> _cards;

        [ObservableProperty]
        private List<Person> _person;

        [ObservableProperty]
        private Student _student;

        [ObservableProperty]
        private Teacher _teacher;

        [ObservableProperty]
        private List<Creature> _creature;

        //public Teacher Teacher { get; set; }

        //public Student Student { get; set; }

        public MainViewModel()
        {
            Teacher = new Teacher()
            {
                Sex = Sex.MALE,
                Salary = 4000,
                Age = 40,
            };
            Student = new Student()
            {
                Age = 20,
                Id = 34,
                Sex = Sex.MALE,
            };
            Cards = new List<Card>()
            {
                new Card(
                    Card.MP.ZERO,
                    "感染者",
                    "下回合可夺走1张调和位置的牌",
                    Card.Priority.TWO,
                    "调和失败"
                ),
                new Card(
                    Card.MP.NEGATIVE_ONE,
                    "外星人",
                    "持有期间，可装作犯人",
                    Card.Priority.ONE,
                    "被监禁"
                ),
                new Card(Card.MP.ZERO, "犯人", "不能使用", Card.Priority.THREE, "不被监禁"),
            };
            Person = new List<Person>()
            {
                new Teacher()
                {
                    Age = 30,
                    Salary = 5000,
                    Sex = Sex.MALE,
                },
                new Student()
                {
                    Age = 20,
                    Id = 43,
                    Sex = Sex.MALE,
                },
                new Teacher()
                {
                    Age = 30,
                    Salary = 3000,
                    Sex = Sex.FEMALE,
                },
                new Student()
                {
                    Age = 10,
                    Id = 23,
                    Sex = Sex.FEMALE,
                },
            };
            Creature = new List<Creature>()
            {
                new Creature() { Age = 10, Gender = Gender.MALE },
                new People() {Age= 20 ,Gender= Gender.MALE,Name = "asdf"},
                new People() {Age= 20 ,Gender= Gender.FEMALE,Name = "asdf"},
                new People() {Age= 20 ,Gender= Gender.LGBTQIAPKDXREW,Name = "asdf"},

            };
        }
    }
}
