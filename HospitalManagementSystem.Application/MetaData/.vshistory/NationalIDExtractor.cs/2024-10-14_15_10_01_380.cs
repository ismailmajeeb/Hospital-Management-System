
using System;
using System.Globalization;
namespace HospitalManagementSystem.Application.MetaData
{


    public class NationalIDExtractor
    {
        public static (DateTime? BirthDate, string Governorate, string Gender) ExtractDataFromNationalID(string nationalID)
        {
            if (string.IsNullOrEmpty(nationalID) || nationalID.Length != 14)
                throw new ArgumentException("National ID must be 14 digits long.");

            // Extract century of birth (1st digit)
            string centuryIndicator = nationalID.Substring(0, 1);
            int century = (centuryIndicator == "2") ? 1900 : 2000;

            // Extract year, month, and day of birth (2nd to 7th digits)
            int year = century + int.Parse(nationalID.Substring(1, 2));
            int month = int.Parse(nationalID.Substring(3, 2));
            int day = int.Parse(nationalID.Substring(5, 2));

            // Try to construct the birth date
            DateTime? birthDate = null;
            try
            {
                birthDate = new DateTime(year, month, day);
            }
            catch
            {
                throw new ArgumentException("Invalid birth date in National ID.");
            }

            // Extract governorate code (8th and 9th digits)
            string governorateCode = nationalID.Substring(7, 2);
            string governorate = GetGovernorateByCode(governorateCode);

            // Extract gender (13th digit): odd = male, even = female
            string genderDigit = nationalID.Substring(12, 1);
            string gender = (int.Parse(genderDigit) % 2 == 0) ? "Female" : "Male";

            return (birthDate, governorate, gender);
        }

        private static string GetGovernorateByCode(string code)
        {
            switch (code)
            {
                case "01": return "Cairo";
                case "02": return "Alexandria";
                case "11": return "Sharqia";
                case "27": return "South Sinai";
                default: return "Unknown Governorate";
            }
        }
    }

}
