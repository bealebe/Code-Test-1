using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace IDNetworksCodeTest_BryanBeale
{
    public class PersonField
    {
        const string NAME = "(Name)";
        const string AGE = "(Age)";
        const string CITY = "(City)";
        const string STATE = "(State)";
        const string GENDER = "(Gender)";
        const string STUDENT = "(Student)";
        const string EMPLOYEE = "(Employee)";
        const string FLAGS = "(Flags)";

        const char KEY_DELIM = ')';
        const char CITY_STATE_DELIM = ',';
        const char NEW_LINE = '\r';

        public string Key { get; private set; }
        public string Value { get; private set; }

        PersonField() { }

        PersonField(string pKeyVal)
        {
            Key = pKeyVal.Substring(0, pKeyVal.IndexOf(KEY_DELIM) + 1);
            Value = pKeyVal.Substring(Key.Length);
            Key = Key.Replace(" ", string.Empty);
        }

        public static IEnumerable<PersonField> ParsePersonFields(string pPersonRecord)
        {

            var records = pPersonRecord.Split(NEW_LINE);

            var otherRecords = new StringBuilder();
            string flagRecord = "";
            string cityRecord = "";

            foreach (string rec in records)
            {
                if (rec.ToUpper().Contains(FLAGS.ToUpper()))
                {
                    flagRecord = rec;
                }
                else if (rec.ToUpper().Contains(CITY.ToUpper()))
                {
                    cityRecord = rec;
                }
                else
                {
                    otherRecords.Append(rec);
                    otherRecords.Append(NEW_LINE);
                }
            }

            return GetAllFields(otherRecords.ToString()).Concat(ParseFlags(flagRecord)).Concat(GetCityState(cityRecord));
        }

        static IEnumerable<PersonField> GetAllFields(string pPersonRecord)
        {
            foreach (var field in pPersonRecord.Split('\r'))
            {
                if (field == "")
                {
                    continue;
                }
                yield return new PersonField(field);
            }
        }

        static IEnumerable<PersonField> ParseFlags(string pFlags)
        {

            StringBuilder bob = new StringBuilder();
            if (pFlags.ToUpper().Replace(FLAGS.ToUpper(), "").Length >= 3)
            {
                var flags = pFlags.Replace(FLAGS, "").ToCharArray();

                bob.Append(GENDER);
                bob.Append(GenderInfo.GetInfo(flags[0] == 'Y'));
                bob.Append(NEW_LINE);

                bob.Append(STUDENT);
                bob.Append(flags[1] == 'Y' ? "Yes" : "No");
                bob.Append(NEW_LINE);

                bob.Append(EMPLOYEE);
                bob.Append(flags[2] == 'Y' ? "Yes" : "No");
                bob.Append(NEW_LINE);

            }

            return GetAllFields(bob.ToString());
        }

        static IEnumerable<PersonField> GetCityState(string field)
        {

            var expanded = field.Split(CITY_STATE_DELIM);
            StringBuilder bob = new StringBuilder(expanded[0]);

            if (expanded.Length > 1)
            {
                bob.Append(NEW_LINE);
                bob.Append(STATE);
                bob.Append(expanded[1].Replace(" ", ""));
            }

            return GetAllFields(bob.ToString());

        }
    }
}