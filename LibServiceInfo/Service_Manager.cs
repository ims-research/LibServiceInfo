#region

using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

#endregion

namespace LibServiceInfo
{
    public class ServiceManager
    {
        public static Dictionary<string, Service> ServiceList = null;
        public static Dictionary<string, Condition> ConditionList = null;

        public static void LoadServices(string fromDirectory)
        {
            ServiceList = new Dictionary<string, Service>();
            string[] fileList = Directory.GetFiles(fromDirectory, "*.xml");
            foreach (string file in fileList)
            {
                Service tempService = new Service(file);
                ServiceList.Add(tempService.ServiceConfig["GUID"], tempService);
            }
        }

        public static void LoadConditions(string file_name)
        {
            ConditionList = new Dictionary<string, Condition>();
            XDocument doc = XDocument.Load(file_name);
            foreach (XElement block in doc.Elements("Conditions").Elements("Condition"))
            {
                Condition newCondition = new Condition(block);
                ConditionList.Add(newCondition.GUID, newCondition);
            }
        }
    }
}