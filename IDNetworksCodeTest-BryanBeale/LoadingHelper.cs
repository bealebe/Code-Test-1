using System;
using System.Collections.Generic;

namespace IDNetworksCodeTest_BryanBeale
{
    public class LoadingHelper
    {
        public LoadingHelper()
        {
            foreach (Person p in ParseData(LoadData()))
            {
                p.PrintPerson();
            }
        }

        private String LoadData()
        {
            return "(  Name)John Do)e\r" +
                "(AGe)20\r" +
                "(City)Ashtabula, OH\r" +
                "(Flags)NYN\r\n" +
                "(Name)Jane Doe\r" +
                "(Flags)YNY\r" +
                "(City)N Kingsvi(lle, OH\r\n" +
                "(Name)Sally Jones\r" +
                "(Age)25\r" +
                "(City)Paris\r" +
                "(Flags)YYY\r";
        }

        internal IEnumerable<Person> ParseData(string pData)
        {
            var inputLines = pData.Split('\n');


            foreach (var line in inputLines)
            {
                yield return Person.BuildPersonFromInput(line);
            }
        }
    }
}
