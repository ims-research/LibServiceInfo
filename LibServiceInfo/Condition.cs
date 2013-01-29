#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#endregion

namespace LibServiceInfo
{
    public class Condition
    {
        public Condition()
        {
            InitialiseVariables();
        }

        public Condition(XElement xmlblock)
        {
            try
            {
                Name = xmlblock.Element("Name").Value;
                Type = xmlblock.Element("Type").Value;
                Description = xmlblock.Element("Description").Value;
                GUID = xmlblock.Element("GUID").Value;
                List<string> possibleValues =
                    new List<string>(xmlblock.Element("Possible_Values").Value.Split(',').Select(p => p.Trim()).ToList());
                PossibleValues = possibleValues;
            }
            catch (Exception)
            {
                PossibleValues = new List<string>();
                throw;
            }
        }

        public string ConType { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public List<string> PossibleValues { get; set; }
        public string GUID { get; set; }

        private void InitialiseVariables()
        {
            PossibleValues = new List<string>();
        }
    }
}