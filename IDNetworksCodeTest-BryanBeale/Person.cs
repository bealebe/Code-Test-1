using System;
using System.Collections.Generic;
using System.Linq;

using static IDNetworksCodeTest_BryanBeale.GenderInfo;

namespace IDNetworksCodeTest_BryanBeale
{
    public class Person
    {
        public static Person BuildPersonFromInput(string pPersonString)
        {
            return BuildPerson(PersonField.ParsePersonFields(pPersonString));
        }

        static Person BuildPerson(IEnumerable<PersonField> pFields)
        {
            Person per = new Person();

            foreach (var f in pFields)
            {
                var prop = per.GetType().GetProperties().Where(i => i.Name == f.Key.Replace("(", string.Empty).Replace(")", string.Empty)).FirstOrDefault();
                //If the property is invalid skip it.
                if (prop == null) continue;
                var type = Nullable.GetUnderlyingType(prop.PropertyType) == null ? prop.PropertyType : Nullable.GetUnderlyingType((prop.PropertyType));
                var propValue = Convert.ChangeType(f.Value, type);
                prop.SetValue(per, propValue, null);
            }
            return per;

        }

        public string FormatPerson()
        {
            string mask = "";

            if (!Age.HasValue)
            {
                mask = @"{0} [{2}]
    City        : {3}
    State       : {4}
    Student     : {5}
    Employee    : {6}";
            }
            else
            {

                mask = @"{0} [{1}, {2}]
    City        : {3}
    State       : {4}
    Student     : {5}
    Employee    : {6}";
            }

            return String.Format(mask, Name, Age, Gender, City, State, Student, Employee);
        }

        public void PrintPerson()
        {
            Console.WriteLine(FormatPerson());
        }

        public string Name { get; set; } = "";
        public int? Age { get; set; }
        public string City { get; set; } = "";
        public string State { get; set; } = "N/A";
        public string Gender { get; set; } = "";
        public string Student { get; set; } = "";
        public string Employee { get; set; } = "";
    }
}
