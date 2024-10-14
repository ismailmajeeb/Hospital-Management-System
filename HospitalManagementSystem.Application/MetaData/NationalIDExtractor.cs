
using HospitalManagementSystem.Core.Entities;
using System;
using System.Globalization;
namespace HospitalManagementSystem.Application.MetaData
{


    public class NationalIDExtractor
    {
        public static (DateTime? BirthDate, string Governorate, Gender Gender) ExtractDataFromNationalID(string nationalID)
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
            Gender gender = (int.Parse(genderDigit) % 2 == 0) ? Gender.Female : Gender.Female;

            return (birthDate, governorate, gender);
        }

        private static string GetGovernorateByCode(string code)
        {
            switch (code)
            {
                case "01": return "Cairo";
                case "02": return "Alexandria";
                case "03": return "Port Said";
                case "04": return "Suez";
                case "11": return "Damietta";
                case "12": return "Dakahlia";
                case "13": return "Sharkia";
                case "14": return "Kalyubia";
                case "15": return "Kafr El Sheikh";
                case "16": return "Gharbia";
                case "17": return "Monufia";
                case "18": return "Beheira";
                case "19": return "Ismailia";
                case "21": return "Giza";
                case "22": return "Beni Suef";
                case "23": return "Fayoum";
                case "24": return "Minya";
                case "25": return "Asyut";
                case "26": return "Sohag";
                case "27": return "Qena";
                case "28": return "Aswan";
                case "29": return "Luxor";
                case "31": return "Red Sea";
                case "32": return "New Valley (El Wadi El Gedid)";
                case "33": return "Matrouh";
                case "34": return "North Sinai";
                case "35": return "South Sinai";
                case "88": return "Foreign (Born Abroad)";
                default: return "Unknown Governorate";
            }
        }

    }

}
