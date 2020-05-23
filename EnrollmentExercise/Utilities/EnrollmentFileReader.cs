using EnrollmentExercise.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EnrollmentExercise
{
    public class EnrollmentFileReader : FileReaderBase<EnrollmentFileContent>
    {

        //Return: 
        //          List<EnrollmentFileContent> if file is valid
        //          Null if file is not valid
        public override IList<EnrollmentFileContent> ReadFileToList(string fileLocation)
        {
            List<EnrollmentFileContent> contents = new List<EnrollmentFileContent>();
            string[] lines = new string[0];
            try
            {
                lines = System.IO.File.ReadAllLines(fileLocation);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            for (int i = 0; i < lines.Length; i++)
            {
                if (ValidateLineContent(lines[i]))
                {
                    contents.Add(TranslateLineIntoObject(lines[i]));
                }
                else
                {
                    //file failed and is passing none of the values back
                    return null;
                }
            }

            return contents;
        }

        //Return:
        //          EnrollmentFileContent if valid
        //          Null if not vaild
        public override EnrollmentFileContent TranslateLineIntoObject(string line)
        {
            try
            {
                var fields = line.Split(',');
                EnrollmentFileContent entry = new EnrollmentFileContent
                {
                    FirstName = fields[0],
                    LastName = fields[1],
                    DOB = DateTime.ParseExact(fields[2], "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    PlanType = fields[3].ToUpper(),
                    EffectiveDate = DateTime.ParseExact(fields[4], "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
                };

                return entry;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while converting file into record that should have been caught by validator. Exception was logged.");
                //log exception, either file or database on a Live environment
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public override bool ValidateLineContent(string line)
        {
            var fields = line.Split(',');
            //Correct column count
            if(fields.Length == 5)
            {
                //All fields required 
                if (fields.All(s => !string.IsNullOrWhiteSpace(s)))
                {
                    //check dates in mmddyyyy format
                    if (DateTime.TryParseExact(fields[2], "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime DOB) &&
                        DateTime.TryParseExact(fields[4], "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime effectDate))
                    {
                        var plan = fields[3].ToUpper();
                        //check valid plan (HSA, HRA, FSA)
                        if (plan == "HSA" || plan == "HRA" || plan == "FSA")
                        {
                            return true;
                        }
                    }
                }
            }

            Console.WriteLine("A record in the file failed validation. Processing has stopped.");

            return false;
        }
    }
}
