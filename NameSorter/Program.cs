using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NameSorter
{
    /// <summary>
    /// Author: Rex
    /// Create Date: 02/03/2019
    /// Description: Sort a name list first by LAST name then GIVING name
    /// Version: 1.0
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Entrance
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //if no filename provided, write a error message on console.
            if (args.Length == 0)
            {
                Console.WriteLine("OOPS! we need a specific filename to continue...");
            }
            else
            {
                var unsortedFileName = args[0];
                //if filename provided but the specific file doesn't exist, write a error message on console.
                if (!File.Exists(unsortedFileName))
                {
                    Console.WriteLine("OOPS! the specific file doesn't exist...");
                }
                else
                {
                    //initialize nameSorterAction instance
                    var nameSorterAction = new NameSorterAction();
                    //read all name to the memory
                    var nameList = nameSorterAction.ReadFile(unsortedFileName);
                    //Sorted the namelist by Linq and provided compare method
                    nameList.Sort(nameSorterAction);
                    //write sorted namelist on console;
                    nameSorterAction.OutputResult(nameList);
                }
            }

            //wait for a key press
            Console.ReadKey();
        }
    }

    public class NameSorterAction :IComparer<string>
    {
        public List<string> ReadFile(string fileName)
        {
            var nameList = new List<string>();
            StreamReader nameFileReader = new StreamReader(fileName);
            //flag to continue read the file
            var readFlag = true;
            while (readFlag)
            {
                var name = nameFileReader.ReadLine();
                if (name == null)
                {
                    readFlag = false;
                }
                else
                {
                    if(name!="") nameList.Add(FormatName(name));
                }
            }
            //release memory
            nameFileReader.Close();
            nameFileReader.Dispose();
            return nameList;
        }

        /// <summary>
        /// write each name on console
        /// </summary>
        /// <param name="nameList">specific nameList</param>
        public void OutputResult(IList<string> nameList)
        {
            foreach (var name in nameList)
                Console.WriteLine(name);
        }
    
        /// <summary>
        /// Implement Compare method on 
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <returns></returns>
        public int Compare(string name1, string name2)
        {
            //analyze 2 names and get 2 lastNames and 2 givenNames
            string lastName1 =GetLastName(name1), givenName1=GetGivenName(name1);
            string lastName2 = GetLastName(name2), givenName2 = GetGivenName(name2);
            //compare first by lastname;
            var result = string.Compare(lastName1, lastName2);
            //return result while result!=0 or compare givenName while result=0
            return result == 0 ? string.Compare(givenName1, givenName2) : result;
        }

        public string FormatName(string name)
        {
            //format name string
            name = name.Trim();
            Regex doubleSpace = new Regex("\\s{2,}");
            if (doubleSpace.IsMatch(name)) name=doubleSpace.Replace(name, " ");
            return name;
        }

        string GetLastName(string originalName)
        {
            originalName = originalName.Trim();
            if (originalName.IndexOf(' ') < 0)
            {
                return originalName;
            }
            else
            {
                var lastSpace = originalName.LastIndexOf(' ');
                return originalName.Substring(lastSpace + 1).Trim();
            }
        }

        string GetGivenName(string originalName)
        {
            originalName = originalName.Trim();
            if (originalName.IndexOf(' ') < 0)
            {
                return "";
            }
            else
            {
                var lastSpace = originalName.LastIndexOf(' ');
                return originalName.Substring(0, lastSpace).Trim();
            }
        }
    }
}
