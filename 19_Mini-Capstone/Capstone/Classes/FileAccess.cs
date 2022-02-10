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
        public string filePath = @"C:\Catering\cateringsystem.csv";
        //public string sourceFile = @"";
        //public string cateringMenu = Path.Combine(filePath, sourceFile);
        //private string destinationFile = @"\log.txt";

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
                        CateringItem cateringItem = new CateringItem(CateringList[1], CateringList[2], 25, decimal.Parse(CateringList[3]));
                        CateringMenu.Add(cateringItem);


                    }

                    
                }
            }
            catch (IOException ex)
            {
                //return 
            }
            return CateringMenu;
        }
       
        

    

    

        // This class should contain any and all details of access to files
    }
}
