using ProveOfConcept.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ProveOfConcept.Managers
{
    public class CsVManager

    {
        private static CsVManager instance = null;
        public static CsVManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CsVManager();
                }
                return instance;
            }
        }
        private StreamReader streamReader;

        public int ReadFile(string path, out List<RunViewModel> readRunsList)
        {
            int index = 0;
            readRunsList = new List<RunViewModel>
                   (); //I will put here the runs from Pais file
            PropertyInfo[] properties = typeof(RunViewModel).GetProperties();
            streamReader = new StreamReader(path);
            string currentLine;
            streamReader.ReadLine(); //skip trhe first line
            while (streamReader.ReadLine() != null) //read file till the end
            {
                currentLine = streamReader.ReadLine();
                index++;
                RunViewModel currentRunItem = new RunViewModel();
                string[] currentLineSplites = currentLine.Split(','); //it's csv comma delimited
                int tempValue; //for tryParse
                if (int.TryParse(currentLineSplites[0], out tempValue))
                {
                    currentRunItem.RunPaisID = tempValue;
                }
                DateTime tempDate;
                if (DateTime.TryParse(currentLineSplites[1], out tempDate))
                {
                    currentRunItem.RunDate = tempDate;
                }
                for (int i = 2; i < currentLineSplites.Length - 3; i++)
                {

                    if (int.TryParse(currentLineSplites[i], out tempValue))
                    {
                        properties[i + 1].SetValue(currentRunItem, tempValue);
                    }
                }
                currentRunItem.RunDbID = index;
                readRunsList.Add(new RunViewModel(currentRunItem)); //I made this trick in order to get rid of the last Item updates all the items in the list
            }
            streamReader.Close();
            return index;
        }
        private CsVManager()
        {

        }
    }
}

