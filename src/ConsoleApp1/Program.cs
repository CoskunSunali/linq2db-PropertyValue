using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new CustomConnection();

            SeedDatabase(connection);

            var carsTable = connection.GetTable<Car>();

            var carsByProperty1 = carsTable.Where(e => e.Brand == "Mercedes").SingleOrDefault();
            var carsByProperty2 = carsTable.Where(e => e.Brand != "Mercedes").ToList();
            var carsByProperty3 = carsTable.Where(e => e.TopSpeed > 250).ToList();
            var carsByProperty4 = carsTable.Where(e => e.TopSpeed >= 250).ToList();
            var carsByProperty5 = carsTable.Where(e => e.TopSpeed != 250).ToList();

            var carsByPropertyValue1 = carsTable.Where(e => e["Brand"] == "Mercedes").SingleOrDefault();
            var carsByPropertyValue2 = carsTable.Where(e => e["Brand"] != "Mercedes").ToList();
            var carsByPropertyValue3 = carsTable.Where(e => e["TopSpeed"] > 250).ToList();
            var carsByPropertyValue4 = carsTable.Where(e => e["TopSpeed"] >= 250).ToList();
            var carsByPropertyValue5 = carsTable.Where(e => e["TopSpeed"] != 250).ToList();

            var peopleTable = connection.GetTable("People");

            var peopleByPropertyValue1 = peopleTable.Where(e => e["FirstName"] == "Svyatoslav").SingleOrDefault();
            var peopleByPropertyValue2 = peopleTable.Where(e => e["FirstName"] != "Svyatoslav").SingleOrDefault();
            var peopleByPropertyValue3 = peopleTable.Where(e => e["Age"] < 20).ToList();
            var peopleByPropertyValue4 = peopleTable.Where(e => e["Age"] <= 20).ToList();
            var peopleByPropertyValue5 = peopleTable.Where(e => e["Age"] > 20).ToList();
            var peopleByPropertyValue6 = peopleTable.Where(e => e["Age"] >= 20).ToList();

            Console.ReadLine();
        }

        private static void SeedDatabase(CustomConnection connection)
        {
            var carsTable = connection.CreateTable<Car>();

            carsTable.Insert(new Car() { Brand = "Mercedes", Color = "Blue", TopSpeed = 260 });
            carsTable.Insert(new Car() { Brand = "BMW", Color = "Green", TopSpeed = 250 });
            carsTable.Insert(new Car() { Brand = "Audi", Color = "Pink", TopSpeed = 150 });

            var peopleTable = connection.CreateTable("Person");

            peopleTable.Insert(new BaseEntity(new Dictionary<string, PropertyValue>() {
                { "FirstName", PropertyValue.Create("FirstName", "Coskun") },
                { "LastName", PropertyValue.Create("LastName", "Sunali") },
                { "Age", PropertyValue.Create("Age", 10) },
            }));

            peopleTable.Insert(new BaseEntity(new Dictionary<string, PropertyValue>() {
                { "FirstName", PropertyValue.Create("FirstName", "Svyatoslav") },
                { "LastName", PropertyValue.Create("LastName", "Danyliv") },
                { "Age", PropertyValue.Create("Age", 20) },
            }));
        }
    }
}
