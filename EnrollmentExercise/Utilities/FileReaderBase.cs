using System;
using System.Collections.Generic;

namespace EnrollmentExercise
{
    public abstract class FileReaderBase<T>
    {
        //Reads the file to a list 
        //
        // input:
        //  string path to file
        //
        // return:
        //  IList of generic
        //
        //Exceptions:
        //  System.IO.FileLoadException
        //  System.IO.FileNotFoundException
        //
        public abstract IList<T> ReadFileToList(string fileLocation);

        //Translates string input into the generic object, 
        //is dependent on the validator to catch issues with data
        //
        // input:
        //  string line of the file
        //
        // return:
        //  line converted to generic
        //
        //Exceptions:
        //  System.IndexOutOfRangeException
        //
        public abstract T TranslateLineIntoObject(string line);


        //Validates the line contains valid data
        //
        // input:
        //  string line of the file
        //
        // return:
        //  bool value of the validity of the data
        //
        //Exceptions:
        //  System.IndexOutOfRangeException
        //
        public abstract bool ValidateLineContent(string line);
    }
}
