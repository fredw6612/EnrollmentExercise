using System;

namespace EnrollmentExercise
{
    class Program
    {
        /*  
        *  Application to read in files and process the data found 
        * 
        *  args.empty - user input CLI
        *  args[0] - File location
        *   
        */
        static void Main(string[] args)
        {
            //Can be swapped with a dynamic if other types of FileReaders are used
            EnrollmentFileReader fileReader = new EnrollmentFileReader();
           
            if (fileReader != null)
            {
                string fileLocation;
                if (args.Length == 0)
                {
                    Console.WriteLine("Please input file location and name:");
                    Console.WriteLine("(e.x. C:\\Temp\\FileName.csv)");

                    fileLocation = Console.ReadLine();
                }
                else
                {
                    fileLocation = args[0];
                }

                //read File
                var fileContent = fileReader.ReadFileToList(fileLocation);
                if(fileContent != null)
                {
                    foreach(var entry in fileContent)
                    {
                        Console.WriteLine(entry.ToString());
                    }
                }
            }
            
        }
    }
}
