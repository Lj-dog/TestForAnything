using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace WpfAppMVVMTest.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly BirthDate { get; set; }

        public int Salary { get; set; }

        public static Employee FakeOne()=> employeeFaker.Generate();

        public static IEnumerable<Employee> FakeMany(int count)=>employeeFaker.Generate(count);

        private static readonly Faker<Employee> employeeFaker = new Faker<Employee>()
            .RuleFor(x=>x.Id,x=>x.IndexFaker)
            .RuleFor(x=>x.FirstName,x=>x.Person.FirstName)
            .RuleFor(Person => Person.LastName, x => x.Person.LastName)
            .RuleFor(x=>x.BirthDate,x=>DateOnly.FromDateTime(x.Person.DateOfBirth))
            .RuleFor(x=>x.Salary, x => x.Random.Int(30000, 120000));
    }
}
