using System;
namespace IDNetworksCodeTest_BryanBeale
{
    public class GenderInfo
    {

        public enum Genders
        {
            Male,
            Female
        }

        public static Genders GetInfo(bool pIsFemale)
        {
            if (pIsFemale)
            {
                return Genders.Female;
            }
            else
            {
                return Genders.Male;
            }
        }

    }

}
