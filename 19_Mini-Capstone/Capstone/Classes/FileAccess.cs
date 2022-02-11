using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class FileAccess
    {
        // all files for this application should in this directory
        // you will likley need to create it on your computer
        private string filePath = @"C:\Catering\cateringsystem.csv";
        //public string sourceFile = @"";
        //public string cateringMenu = Path.Combine(filePath, sourceFile);
        private string destinationFile = @"C:\Catering\log.txt";

        public List<CateringItem> ReadFromFile()
        {
            List<CateringItem> CateringMenu = new List<CateringItem>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] CateringList = line.Split("|");
                        CateringItem cateringItem = new CateringItem(CateringList[1], CateringList[2], "25", decimal.Parse(CateringList[3]));
                        CateringMenu.Add(cateringItem);


                    }

                    
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return CateringMenu;
        }
        public void WriteToFile(string transaction)
        {
            try
            {

                using (StreamWriter sw = new StreamWriter(destinationFile, true))
                {


                    sw.WriteLine(transaction);

                }
            }

            catch (IOException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
       
        

    

    

        // This class should contain any and all details of access to files
    }
}
