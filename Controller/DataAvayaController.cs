using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_Bravo.Model;

namespace WEBAPI_Bravo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataAvayaController : ControllerBase
    {
        private readonly pcc135Context _context;

        public DataAvayaController(pcc135Context context)
        {
            _context = context;


        }

        [HttpGet("NewSummaryDaily")]

        public IActionResult NewSummaryDaily()
        {
            string localDirectory = @"E:\DataAvaya";
            string filePath = Path.Combine(localDirectory, "Summary_Daily.txt");
            NewSummaryDaily? totalsData = null;
            List<NewSummaryDaily> parsedDataRows = new List<NewSummaryDaily>();

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                List<string> data = System.IO.File.ReadAllLines(filePath).ToList();

                // Extract the header row
                var headerRow = data.FirstOrDefault(row => row.StartsWith("Date;"));

                // Extract data rows starting from "Totals"
                var dataRows = data.SkipWhile(row => !row.StartsWith("Totals")).ToList();

                if (dataRows.Any())
                {
                    var totalsRow = dataRows.FirstOrDefault(row => row.StartsWith("Totals"));

                    if (totalsRow != null)
                    {
                        totalsData = ParseData(totalsRow);

                        // Parse all data rows excluding "Totals"
                        foreach (var row in dataRows.Skip(1)) // Skip "Totals"
                        {
                            var parsedRow = ParseData(row);
                            if (parsedRow != null)
                            {
                                parsedDataRows.Add(parsedRow);
                            }
                        }

                        return Ok(new
                        {
                            Header = headerRow,
                            TotalsRow = totalsData,
                            DataRows = parsedDataRows // Now structured as a list of objects
                        });
                    }
                }

                Console.WriteLine("Totals row not found.");
                return Ok(new { Header = headerRow, TotalsRow = (string?)null, DataRows = parsedDataRows });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Error = ex.ToString() });
            }
        }



        [HttpGet("ActualRelativeToTargetDaily")]

        public IActionResult ActualRelativeToTargetDaily()
        {

            string localDirectory = @"E:\DataAvaya";
            string filePath = Path.Combine(localDirectory, "ActualRelativeToTargetDaily.txt");
            string value = "0";

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                var lines = System.IO.File.ReadAllLines(filePath);

                // var targetLine = lines.FirstOrDefault(line => line.Contains("CH_Livechat_UAT"));

                if (lines != null)
                {
                    //  var parts = lines.Split(';');
                    if (lines.Length > 1)
                    {
                        value = lines[4].Trim();
                        Console.WriteLine($"The value for CH_Livechat_UAT is: {value}");
                    }
                    else
                    {
                        Console.WriteLine("Unable to extract value for CH_Livechat_UAT.");
                    }
                }
                else
                {
                    Console.WriteLine("CH_Livechat_UAT not found in the file.");
                }

                return Ok(value);


            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("ManageCareReportAllDataOne")]
        public IActionResult ManageCareReportAllDataOne()
        {
            //string localDirectory = @"E:\DataAvaya";
            //string filePath = Path.Combine(localDirectory, "ManageCareReportAllDataOne.txt");

            //try
            //{
            //    if (!System.IO.File.Exists(filePath))
            //    {
            //        return NotFound("File not found.");
            //    }

            //    var dataList = ParseACDData(filePath);
            //    var dataLogin = ParseDataLogin(filePath);

            //    return Ok(dataList);


            //    //return Ok(new { SkillData = skillDataList, StateData = stateDataList });
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex.ToString());
            //}

            string localDirectory = @"E:\DataAvaya";
            string filePath = Path.Combine(localDirectory, "ManageCareReportAllDataOne.txt");
            List<ACDStats> statsList = new List<ACDStats>();
            List<ACDActivity> activityList = new List<ACDActivity>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    bool isStatsSection = true; // Flag to identify whether we're in the stats section or activity section

                    while ((line = sr.ReadLine()) != null)
                    {
                        // Skip empty lines
                        if (string.IsNullOrWhiteSpace(line)) continue;



                        // Split data by semicolon
                        string[] values = line.Split(';');

                        // Validate that the line has enough columns
                        if (values.Length < 15) continue;



                        if (values[0].Contains("AVAIL") || values[0].Contains("AUX"))
                        {

                            ACDActivity activityRow = new ACDActivity
                            {
                                State = values[0],
                                LoginID = values[1],
                                Extn = values[2],
                                AUXReason = values[3],
                                Direction = values[4],
                                Time = values[5],
                                SplitSkill = values[6],
                                ACDCalls = int.TryParse(values[7], out int acdCalls) ? acdCalls : 0,
                                ACWINTIME = values[8], // Assuming this is a string, so no parsing needed
                                ACWTime = values[9],  // Assuming this is a string, so no parsing needed
                                AbanCalls = int.TryParse(values[10], out int abanCalls) ? abanCalls : 0,
                                AcceptedInterrupts = int.TryParse(values[11], out int acceptedInterrupts) ? acceptedInterrupts : 0,
                                Skill1 = int.TryParse(values[12], out int skill1) ? skill1 : 0,
                                Skill2 = int.TryParse(values[13], out int skill2) ? skill2 : 0,
                                Skill3 = int.TryParse(values[14], out int skill3) ? skill3 : 0
                            };

                            activityList.Add(activityRow);
                            // Read data for ACDStats

                        }
                        else
                        {
                            ACDStats statsRow = new ACDStats
                            {
                                SplitSkill = values[0],
                                ACDCalls = int.TryParse(values[1], out int acdCalls) ? acdCalls : 0,
                                CsplitInQueue = int.TryParse(values[2], out int csplitInQueue) ? csplitInQueue : 0,
                                AgentsStaffed = int.TryParse(values[3], out int agentsStaffed) ? agentsStaffed : 0,
                                AgentsInAux = int.TryParse(values[4], out int agentsInAux) ? agentsInAux : 0,
                                PercentWithinServiceLevel = double.TryParse(values[5], out double percentWithinServiceLevel) ? percentWithinServiceLevel : 0.0,
                                PercentAbanCalls = int.TryParse(values[6], out int percentAbanCalls) ? percentAbanCalls : 0,
                                OutboundAbanCalls = int.TryParse(values[7], out int outboundAbanCalls) ? outboundAbanCalls : 0,
                                AbanCalls = int.TryParse(values[8], out int abanCalls) ? abanCalls : 0,
                                PercentServiceLevel = double.TryParse(values[9], out double percentServiceLevel) ? percentServiceLevel : 0.0,
                                CallsOffered = int.TryParse(values[10], out int callsOffered) ? callsOffered : 0,
                                AgentsInACW = int.TryParse(values[11], out int agentsInACW) ? agentsInACW : 0,
                                InRing = int.TryParse(values[12], out int inRing) ? inRing : 0,
                                AHT = values[13], // Assuming this is a string, so no parsing needed
                                AgentsAvail = int.TryParse(values[14], out int agentsAvail) ? agentsAvail : 0
                            };

                            statsList.Add(statsRow);

                        }


                    }
                }

                // Serialize data into JSON
                var output = new
                {
                    Stats = statsList,
                    Activities = activityList
                };

                string jsonOutput = JsonConvert.SerializeObject(output, Formatting.Indented);
                return StatusCode(201, jsonOutput);  // Return as HTTP response or process further

                // Optionally display to console:
                // Console.WriteLine(jsonOutput);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString()); // Return error if something goes wrong
            }



        }
        //}
        //List<Dictionary<string, string>> dataList = new List<Dictionary<string, string>>();
        //try
        //{
        //    using (StreamReader sr = new StreamReader(filePath))
        //    {
        //        string headerLine = sr.ReadLine();
        //        if (headerLine != null)
        //        {
        //            // Pisahkan header pertama (kolom) dengan ';'
        //            string[] headers = headerLine.Split(';');

        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                // Pisahkan data berdasarkan ';'
        //                string[] values = line.Split(';');

        //                // Membuat dictionary untuk setiap baris data
        //                Dictionary<string, string> dataRow = new Dictionary<string, string>();

        //                // Pastikan jumlah header dan nilai cocok
        //                for (int i = 0; i < headers.Length && i < values.Length; i++)
        //                {
        //                    dataRow[headers[i]] = values[i];
        //                }

        //                // Menambahkan baris data ke dalam daftar
        //                dataList.Add(dataRow);
        //            }
        //        }
        //    }

        //    // Mengonversi data menjadi JSON
        //    string jsonOutput = JsonConvert.SerializeObject(dataList, Formatting.Indented);

        //    // Menampilkan JSON ke konsol
        //    .//Console.WriteLine(jsonOutput);

        //    return StatusCode(201, jsonOutput);
        //}
        //catch (Exception e)
        //{
        //    return StatusCode(500, e.ToString());
        //}
        //  }
        //  }



        [HttpGet("SplitSkillSummaryIntervalNew")]

        public IActionResult SplitSkillSummaryIntervalNew()
        {

            string localDirectory = @"E:\DataAvaya";
            string filePath = Path.Combine(localDirectory, "SplitSkillSummaryIntervalNew.txt");


            Totals totalsData;

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }



                List<string> data = System.IO.File.ReadAllLines(filePath).ToList();

                var headerRow = data.FirstOrDefault(row => row.Contains("Date;"));
                var dataRows = data.SkipWhile(row => !row.Contains("Totals")).ToList();  // Skip metadata rows and start from Totals

                // Process the "Totals" row
                var totalsRow = dataRows.FirstOrDefault(row => row.StartsWith("Totals"));
                if (totalsRow != null)
                {
                    totalsData = ParseDataNew(totalsRow);

                    return Ok(totalsData);


                }
                else
                {
                    Console.WriteLine("Totals row not found.");
                }
                return Ok(totalsRow);
            }



            catch (Exception ex)
            {

                return StatusCode(500, ex.ToString());
            }


        }

        static NewSummaryDaily ParseData(string dataLine)
        {
            var fields = dataLine.Split(';');

            return new NewSummaryDaily
            {
                Date = fields[0],
                CallsOffered = int.Parse(fields[1]),
                ACDCalls = int.Parse(fields[2]),
                Acceptable = int.Parse(fields[3]),
                AbanCalls = int.Parse(fields[4]),
                AbanCallsGreaterThan5Sec = int.Parse(fields[5]),
                AbanCallsLessThan5Sec = int.Parse(fields[6]),
                AbanTime = fields[7],
                AHT = int.Parse(fields[8]),
                ForcedDiscCalls = int.Parse(fields[9]),
                ForcedBusyCalls = int.Parse(fields[10]),
                AvgSpeedAns = fields[11],
                AvgAbanTime = fields[12],
                AvgACDTime = fields[13],
                AvgACWTime = fields[14],
                FlowIn = int.Parse(fields[15]),
                FlowOut = int.Parse(fields[16]),
                ExtnOutCalls = int.Parse(fields[17]),
                AvgExtnOutTime = fields[18],
                PercentACDTime = decimal.Parse(fields[19]),
                PercentAnsCalls = decimal.Parse(fields[20]),
                PercentAbandCalls = decimal.Parse(fields[21]),
                PercentWithinServiceLevel = decimal.Parse(fields[22])
            };
        }

        static Totals ParseDataNew(string dataLine)
        {
            var columns = dataLine.Split(';');

            return new Totals
            {
                CallsOffered = int.Parse(columns[4]),
                AcceptCalls = int.Parse(columns[5]),
                ACDCalls = int.Parse(columns[6]),
                AbandonedCalls = int.Parse(columns[7]),
                AbandonedCallsUnder5Sec = int.Parse(columns[8]),
                AbandonedCallsOver5Sec = int.Parse(columns[9]),
                ABNTIME = columns[10].ToString(),
                ForcedDiscCalls = int.Parse(columns[11]),
                ForcedBusyCalls = int.Parse(columns[12]),
                FlowOut = int.Parse(columns[13]),
                AvgSpeedAnswer = columns[14].ToString(),
                AvgACDTime = columns[15].ToString(),
                ACDTime = columns[16].ToString(),
                AvgACWTime = columns[17].ToString(),
                AvgAbandonTime = columns[18].ToString(),
                FlowIn = int.Parse(columns[19]),
                ExtnOutCalls = int.Parse(columns[20]),
                AvgExtnOutTime = columns[21].ToString(),
                ACDPercentage = double.Parse(columns[22]),
                AnswerPercentage = double.Parse(columns[23]),
                AbandonedPercentage = double.Parse(columns[24]),
                AvgPosStaff = double.Parse(columns[25]),
                CallsPerPos = double.Parse(columns[26]),
                HeldCalls = int.Parse(columns[27]),
                HoldTime = columns[28].ToString(),
                AvgHoldTime = columns[29].ToString(),
                AHT = columns[30].ToString(),
                PercentWithinServiceLevel = double.Parse(columns[31]),
                ServiceLevel = double.Parse(columns[32])
            };
        }
        public static List<ACDData> ParseACDData(string filePath)
        {
            var acdDataList = new List<ACDData>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    bool headerParsed = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        // Skip header lines (optional based on your file structure)
                        if (!headerParsed)
                        {
                            // If you have headers or specific lines to skip, handle here
                            headerParsed = true;
                            continue;
                        }

                        // Split the line by semicolon
                        var values = line.Split(';');

                        if (values.Length > 1)
                        {
                            var acdData = new ACDData
                            {
                                SplitSkill = values[0],
                                ACDCalls = int.TryParse(values[1], out int acdCalls) ? acdCalls : 0,
                                CsplitInQueue = int.TryParse(values[2], out int csplitInQueue) ? csplitInQueue : 0,
                                AgentsStaffed = int.TryParse(values[3], out int agentsStaffed) ? agentsStaffed : 0,
                                AgentsInAux = int.TryParse(values[4], out int agentsInAux) ? agentsInAux : 0,
                                PercentWithinServiceLevel = double.TryParse(values[5], out double percentWithinSL) ? percentWithinSL : 0,
                                PercentAbanCalls = int.TryParse(values[6], out int percentAbanCalls) ? percentAbanCalls : 0,
                                OutboundAbanCalls = int.TryParse(values[7], out int outboundAbanCalls) ? outboundAbanCalls : 0,
                                AbanCalls = int.TryParse(values[8], out int abanCalls) ? abanCalls : 0,
                                PercentServiceLevel = double.TryParse(values[9], out double percentServiceLevel) ? percentServiceLevel : 0,
                                CallsOffered = int.TryParse(values[10], out int callsOffered) ? callsOffered : 0,
                                AgentsInACW = int.TryParse(values[11], out int agentsInACW) ? agentsInACW : 0,
                                InRing = int.TryParse(values[12], out int inRing) ? inRing : 0,
                                AHT = double.TryParse(values[13], out double aht) ? aht : 0,
                                AgentsAvail = int.TryParse(values[14], out int agentsAvail) ? agentsAvail : 0
                            };

                            acdDataList.Add(acdData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }

            return acdDataList;
        }

        public static List<DataLogin> ParseDataLogin(string filePath)
        {
            var acdDataList = new List<DataLogin>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    bool headerParsed = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        // Skip header line if it's the first line
                        if (!headerParsed)
                        {
                            headerParsed = true;
                            continue;
                        }

                        // Split the line by semicolon
                        var values = line.Split(';');

                        if (values.Length > 1)
                        {
                            var acdData = new DataLogin
                            {
                                State = string.IsNullOrEmpty(values[0]) ? null : values[0],  // Handle empty string for State
                                LoginID = string.IsNullOrEmpty(values[1]) ? null : values[1],  // Handle empty string for LoginID
                                Extn = string.IsNullOrEmpty(values[2]) ? null : values[2],     // Handle empty string for Extn
                                AUXReason = string.IsNullOrEmpty(values[3]) ? null : values[3],
                                Direction = string.IsNullOrEmpty(values[4]) ? null : values[4],
                                Time = string.IsNullOrEmpty(values[5]) ? null : values[5],
                                SplitSkill = string.IsNullOrEmpty(values[6]) ? null : values[6],
                                ACDCalls = int.TryParse(values[7], out int acdCalls) ? acdCalls : 0,  // Handle empty or zero for ACD Calls
                                ACWINTIME = int.TryParse(values[8], out int acwInTime) ? acwInTime : 0, // Handle empty or zero for ACWINTIME
                                ACWTime = int.TryParse(values[9], out int acwTime) ? acwTime : 0,
                                AbanCalls = int.TryParse(values[10], out int abanCalls) ? abanCalls : 0,
                                AcceptedInterrupts = int.TryParse(values[11], out int acceptedInterrupts) ? acceptedInterrupts : 0,
                                Skill1 = string.IsNullOrEmpty(values[12]) ? null : values[12],  // Handle empty string for Skill 1
                                Skill2 = string.IsNullOrEmpty(values[13]) ? null : values[13],  // Handle empty string for Skill 2
                                Skill3 = string.IsNullOrEmpty(values[14]) ? null : values[14]   // Handle empty string for Skill 3
                            };

                            acdDataList.Add(acdData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }

            return acdDataList;
        }


        //public List<SplitSkillSummary> ParseSummaryData(string input)
        //{
        //    var summaryList = new List<SplitSkillSummary>();

        //    using (var reader = new StringReader(input))
        //    {
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            var columns = line.Split(';');

        //            // Skip headers
        //            if (columns.Length < 3 || columns[0] == "Totals")
        //                continue;

        //            var summary = new SplitSkillSummary
        //            {
        //                SplitSkill = columns[0],
        //                Time = columns[1],
        //                CallOffered = int.TryParse(columns[2], out int temp1) ? temp1 : 0,
        //                Accepted = int.TryParse(columns[3], out int temp2) ? temp2 : 0,
        //                ACDCalls = int.TryParse(columns[4], out int temp3) ? temp3 : 0,
        //                AbanCalls = int.TryParse(columns[5], out int temp4) ? temp4 : 0,
        //                AbanCallsLessThan5Sec = int.TryParse(columns[6], out int temp5) ? temp5 : 0,
        //                AbanCallsMoreThan5Sec = int.TryParse(columns[7], out int temp6) ? temp6 : 0,
        //                ABNTIME = int.TryParse(columns[8], out int temp7) ? temp7 : 0,
        //                ForcedDiscCalls = int.TryParse(columns[9], out int temp8) ? temp8 : 0,
        //                ForcedBusyCalls = int.TryParse(columns[10], out int temp9) ? temp9 : 0,
        //                FlowOut = int.TryParse(columns[11], out int temp10) ? temp10 : 0,
        //                AvgSpeedAns = double.TryParse(columns[12], out double temp11) ? temp11 : 0,
        //                AvgACDTime = columns[13],
        //                AvgACWTime = columns[14],
        //                AvgAbanTime = columns[15],
        //                FlowIn = double.TryParse(columns[16], out double temp12) ? temp12 : 0,
        //                ExtnOutCalls = int.TryParse(columns[17], out int temp13) ? temp13 : 0,
        //                AvgExtnOutTime = double.TryParse(columns[18], out double temp14) ? temp14 : 0,
        //                ACDTimePercentage = double.TryParse(columns[19], out double temp15) ? temp15 : 0,
        //                AnsCallsPercentage = double.TryParse(columns[20], out double temp16) ? temp16 : 0,
        //                AbandCallsPercentage = double.TryParse(columns[21], out double temp17) ? temp17 : 0,
        //                AvgPosStaff = double.TryParse(columns[22], out double temp18) ? temp18 : 0,
        //                CallsPerPos = double.TryParse(columns[23], out double temp19) ? temp19 : 0,
        //                HeldCalls = int.TryParse(columns[24], out int temp20) ? temp20 : 0,
        //                HoldTime = columns[25],
        //                AvgHoldTime = columns[26],
        //                AHT = columns[27],
        //                PercentWithinServiceLevel = double.TryParse(columns[28], out double temp21) ? temp21 : 0,
        //                SL = columns[29]
        //            };

        //            summaryList.Add(summary);
        //        }
        //    }

        //    return summaryList;
        //}



    }







}
public class SplitSkillSummary
{
    public string SplitSkill { get; set; }
    public string Time { get; set; }
    public int CallOffered { get; set; }
    public int Accepted { get; set; }
    public int ACDCalls { get; set; }
    public int AbanCalls { get; set; }
    public int AbanCallsLessThan5Sec { get; set; }
    public int AbanCallsMoreThan5Sec { get; set; }
    public int ABNTIME { get; set; }
    public int ForcedDiscCalls { get; set; }
    public int ForcedBusyCalls { get; set; }
    public int FlowOut { get; set; }
    public double AvgSpeedAns { get; set; }
    public string AvgACDTime { get; set; }
    public string AvgACWTime { get; set; }
    public string AvgAbanTime { get; set; }
    public double FlowIn { get; set; }
    public int ExtnOutCalls { get; set; }
    public double AvgExtnOutTime { get; set; }
    public double ACDTimePercentage { get; set; }
    public double AnsCallsPercentage { get; set; }
    public double AbandCallsPercentage { get; set; }
    public double AvgPosStaff { get; set; }
    public double CallsPerPos { get; set; }
    public int HeldCalls { get; set; }
    public string HoldTime { get; set; }
    public string AvgHoldTime { get; set; }
    public string AHT { get; set; }
    public double PercentWithinServiceLevel { get; set; }
    public string SL { get; set; }
}

public class DetailedRecord
{
    public string SplitSkill { get; set; }
    public string TimeSlot { get; set; }
    public int CallOffered { get; set; }
    public int Accepted { get; set; }
    public int ACDCalls { get; set; }
    public int AbanCalls { get; set; }
    public double AvgSpeedAns { get; set; }
    public string AvgACDTime { get; set; }
    public string AvgACWTime { get; set; }
}
public class DataLogin
{
    public string State { get; set; }
    public string LoginID { get; set; }
    public string Extn { get; set; }
    public string AUXReason { get; set; }
    public string Direction { get; set; }
    public string Time { get; set; }
    public string SplitSkill { get; set; }
    public int ACDCalls { get; set; }
    public int ACWINTIME { get; set; }
    public int ACWTime { get; set; }
    public int AbanCalls { get; set; }
    public int AcceptedInterrupts { get; set; }
    public string Skill1 { get; set; }
    public string Skill2 { get; set; }
    public string Skill3 { get; set; }
}


public class Totals
{
    public int CallsOffered { get; set; }
    public int AcceptCalls { get; set; }
    public int ACDCalls { get; set; }
    public int AbandonedCalls { get; set; }
    public int AbandonedCallsUnder5Sec { get; set; }
    public int AbandonedCallsOver5Sec { get; set; }
    public string ABNTIME { get; set; }
    public int ForcedDiscCalls { get; set; }
    public int ForcedBusyCalls { get; set; }
    public int FlowOut { get; set; }
    public string AvgSpeedAnswer { get; set; }
    public string AvgACDTime { get; set; }
    public string ACDTime { get; set; }
    public string AvgACWTime { get; set; }
    public string AvgAbandonTime { get; set; }
    public int FlowIn { get; set; }
    public int ExtnOutCalls { get; set; }
    public string AvgExtnOutTime { get; set; }
    public double ACDPercentage { get; set; }
    public double AnswerPercentage { get; set; }
    public double AbandonedPercentage { get; set; }
    public double AvgPosStaff { get; set; }
    public double CallsPerPos { get; set; }
    public int HeldCalls { get; set; }
    public string HoldTime { get; set; }
    public string AvgHoldTime { get; set; }
    public string AHT { get; set; }
    public double PercentWithinServiceLevel { get; set; }
    public double ServiceLevel { get; set; }
}
public class NewSummaryDaily
{
    public string Date { get; set; }
    public int CallsOffered { get; set; }
    public int ACDCalls { get; set; }
    public int Acceptable { get; set; }
    public int AbanCalls { get; set; }
    public int AbanCallsGreaterThan5Sec { get; set; }
    public int AbanCallsLessThan5Sec { get; set; }
    public string AbanTime { get; set; }
    public int AHT { get; set; }
    public int ForcedDiscCalls { get; set; }
    public int ForcedBusyCalls { get; set; }
    public string AvgSpeedAns { get; set; }
    public string AvgAbanTime { get; set; }
    public string AvgACDTime { get; set; }
    public string AvgACWTime { get; set; }
    public int FlowIn { get; set; }
    public int FlowOut { get; set; }
    public int ExtnOutCalls { get; set; }
    public string AvgExtnOutTime { get; set; }
    public decimal PercentACDTime { get; set; }
    public decimal PercentAnsCalls { get; set; }
    public decimal PercentAbandCalls { get; set; }
    public decimal PercentWithinServiceLevel { get; set; }
}

public class ACDData
{
    public string SplitSkill { get; set; }
    public int ACDCalls { get; set; }
    public int CsplitInQueue { get; set; }
    public int AgentsStaffed { get; set; }
    public int AgentsInAux { get; set; }
    public double PercentWithinServiceLevel { get; set; }
    public int PercentAbanCalls { get; set; }
    public int OutboundAbanCalls { get; set; }
    public int AbanCalls { get; set; }
    public double PercentServiceLevel { get; set; }
    public int CallsOffered { get; set; }
    public int AgentsInACW { get; set; }
    public int InRing { get; set; }
    public double AHT { get; set; }
    public int AgentsAvail { get; set; }
}
public class ACDStats
{
    public string SplitSkill { get; set; }
    public int ACDCalls { get; set; }
    public int CsplitInQueue { get; set; }
    public int AgentsStaffed { get; set; }
    public int AgentsInAux { get; set; }
    public double PercentWithinServiceLevel { get; set; }
    public int PercentAbanCalls { get; set; }
    public int OutboundAbanCalls { get; set; }
    public int AbanCalls { get; set; }
    public double PercentServiceLevel { get; set; }
    public int CallsOffered { get; set; }
    public int AgentsInACW { get; set; }
    public int InRing { get; set; }
    public string AHT { get; set; }
    public int AgentsAvail { get; set; }
}

public class ACDActivity
{
    public string State { get; set; }
    public string LoginID { get; set; }
    public string Extn { get; set; }
    public string AUXReason { get; set; }
    public string Direction { get; set; }
    public string Time { get; set; }
    public string SplitSkill { get; set; }
    public int ACDCalls { get; set; }
    public string ACWINTIME { get; set; }
    public string ACWTime { get; set; }
    public int AbanCalls { get; set; }
    public int AcceptedInterrupts { get; set; }
    public int Skill1 { get; set; }
    public int Skill2 { get; set; }
    public int Skill3 { get; set; }
}

public class StateData
{
    public string State { get; set; }
    public string LoginID { get; set; }
    public string Extn { get; set; }
    public string AUXReason { get; set; }
    public string Direction { get; set; }
    public string Time { get; set; }
    public string SplitSkill { get; set; }
    public int ACDCalls { get; set; }
    public string ACWInTime { get; set; }
    public int ACWTime { get; set; }
    public int AbanCalls { get; set; }
    public int AcceptedInterrupts { get; set; }
    public string Skill1 { get; set; }
    public string Skill2 { get; set; }
    public string Skill3 { get; set; }
}


