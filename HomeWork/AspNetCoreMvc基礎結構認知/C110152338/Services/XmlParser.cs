using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebApp.Models;

namespace WebApp.Services
{
    public class XmlParser
    {
        public static List<RescueData> ParseXml(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);

            return doc.Descendants("row")
                      .Select(row => new RescueData
                      {
                          Month = int.Parse(row.Element("Col1")?.Value ?? "0"),
                          GeneralAmbulance = int.Parse(row.Element("Col2")?.Value ?? "0"),
                          ICUAmbulance = int.Parse(row.Element("Col3")?.Value ?? "0"),
                          Transported = int.Parse(row.Element("Col4")?.Value ?? "0"),
                          NotTransported = int.Parse(row.Element("Col5")?.Value ?? "0"),
                          AcuteDisease = int.Parse(row.Element("Col6")?.Value ?? "0"),
                          DrugPoisoning = int.Parse(row.Element("Col7")?.Value ?? "0"),
                          CO_Poisoning = int.Parse(row.Element("Col8")?.Value ?? "0"),
                          Seizure = int.Parse(row.Element("Col9")?.Value ?? "0"),
                          Collapse = int.Parse(row.Element("Col10")?.Value ?? "0"),
                          MentalDisorder = int.Parse(row.Element("Col11")?.Value ?? "0"),
                          PregnancyEmergency = int.Parse(row.Element("Col12")?.Value ?? "0"),
                          NonTrauma_OHCA = int.Parse(row.Element("Col13")?.Value ?? "0"),
                          NonTrauma_Other = int.Parse(row.Element("Col14")?.Value ?? "0"),
                          GeneralTrauma = int.Parse(row.Element("Col15")?.Value ?? "0"),
                          TrafficInjury = int.Parse(row.Element("Col16")?.Value ?? "0"),
                          Drowning = int.Parse(row.Element("Col17")?.Value ?? "0"),
                          FallInjury = int.Parse(row.Element("Col18")?.Value ?? "0"),
                          Falling = int.Parse(row.Element("Col19")?.Value ?? "0"),
                          StabWound = int.Parse(row.Element("Col20")?.Value ?? "0"),
                          Burn = int.Parse(row.Element("Col21")?.Value ?? "0"),
                          ElectricShock = int.Parse(row.Element("Col22")?.Value ?? "0"),
                          AnimalBite = int.Parse(row.Element("Col23")?.Value ?? "0"),
                          Trauma_OHCA = int.Parse(row.Element("Col24")?.Value ?? "0"),
                          Trauma_Other = int.Parse(row.Element("Col25")?.Value ?? "0")
                      }).ToList();
        }
    }
}
