using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_Bravo.Services;

public class DetailServices: iDetailServices
{

    public CallCenterData ReadDataFromTxtNew(string filePath, string Skill)
    {
        var lines = File.ReadAllLines(filePath);
        var data = new CallCenterData();
        var agents = new List<AgentData>();

        var skillList = Skill.Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .Select(s => s.Trim())
                             .ToList();

        foreach (var line in lines)
        {
            var parts = line.Split(';');

            if (string.IsNullOrWhiteSpace(line)) continue;

            if (parts[0].StartsWith("Split/Skill:"))
            {
                for (int i = 1; i < parts.Length; i++)
                {
                    if (parts[i].StartsWith("Skill State:"))
                    {
                        data.SkillState = parts[i + 1];
                        break;
                    }
                }
            }
            else if (parts[0].StartsWith("Calls Waiting:"))
            {
                data.CallsWaiting = int.Parse(parts[1]);
                data.AgentsStaffed = int.Parse(parts[3]);
            }
            else if (parts[0].StartsWith("Oldest Call Waiting:"))
            {
                data.OldestCallWaiting = parts[1];
                data.AgentsAvailable = int.Parse(parts[3]);
            }
            else if (parts[0].StartsWith("Service Level:"))
            {
                data.ServiceLevel = int.Parse(parts[1]);
            }
            else if (parts[2].StartsWith("Agents in AUX:") && parts.Length > 1)
            {
                if (int.TryParse(parts[3], out int auxCount))
                    data.AgentsInAUX = auxCount;
            }
            else if (int.TryParse(parts[0], out _)) // agent data line
            {
                if (parts.Length >= 12)
                {
                    var splitSkill = parts[9].Trim();

                    // 💡 Tambahkan filter berdasarkan skillList
                    if (skillList.Any(s => string.Equals(s, splitSkill, StringComparison.OrdinalIgnoreCase)))
                    {
                        var agent = new AgentData
                        {
                            AgentName = parts[1],
                            LoginID = parts[2],
                            Extension = parts[3],
                            Role = parts[4],
                            Percent = parts[5],
                            AUXReason = parts[6],
                            State = parts[7],
                            Direction = parts[8],
                            SplitSkill = parts[9],
                            Level = parts[10],
                            Time = parts[11],
                            VDNName = parts.Length > 12 ? parts[12] : ""
                        };

                        agents.Add(agent);
                    }
                }
            }
        }

        data.Agents = agents.Distinct().ToList();
        return data;
    }
    public CallCenterData ReadDataFromTxt(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var data = new CallCenterData();
        var agents = new List<AgentData>();

        foreach (var line in lines)
        {
            var parts = line.Split(';');

            if (string.IsNullOrWhiteSpace(line)) continue;

            if (parts[0].StartsWith("Split/Skill:"))
            {
                for (int i = 1; i < parts.Length; i++)
                {
                    if (parts[i].StartsWith("Skill State:"))
                    {
                        data.SkillState = parts[i + 1];
                        break;
                    }
                }
            }
            else if (parts[0].StartsWith("Calls Waiting:"))
            {
                data.CallsWaiting = int.Parse(parts[1]);
                data.AgentsStaffed = int.Parse(parts[3]);
            }
            else if (parts[0].StartsWith("Oldest Call Waiting:"))
            {
                data.OldestCallWaiting = parts[1];
                data.AgentsAvailable = int.Parse(parts[3]);
            }
            else if (parts[0].StartsWith("Service Level:"))
            {
                data.ServiceLevel = int.Parse(parts[1]);
            }

            else if (parts[2].StartsWith("Agents in AUX:") && parts.Length > 1)
            {
                if (int.TryParse(parts[3], out int auxCount))
                    data.AgentsInAUX = auxCount;
            }
            else if (int.TryParse(parts[0], out _)) // agent data line
            {
                if (parts.Length >= 12)
                {
                    var agent = new AgentData
                    {
                        AgentName = parts[1],
                        LoginID = parts[2],
                        Extension = parts[3],
                        Role = parts[4],
                        Percent = parts[5],
                        AUXReason = parts[6],
                        State = parts[7],
                        Direction = parts[8],
                        SplitSkill = parts[9],
                        Level = parts[10],
                        Time = parts[11],
                        VDNName = parts.Length > 12 ? parts[12] : ""
                    };

                    agents.Add(agent);
                }
            }
        }

        data.Agents = agents.Distinct().ToList();
        return data;
    }


    public async Task<IActionResult> ReadDataCallFromFile(string path,string  Skill)
    {

        var skillList = Skill.Split(',', StringSplitOptions.RemoveEmptyEntries)
                       .Select(s => s.Trim().ToLower())
                       .ToList();

        if (!File.Exists(path))
        {
            return new NotFoundObjectResult("File not found");
        }

        var lines = await File.ReadAllLinesAsync(path);
        if (lines.Length < 5)
        {
            return new BadRequestObjectResult("Invalid file format");
        }

        var headers = lines[3].Split(';');
        var records = new List<Dictionary<string, string>>();
        Dictionary<string, string> totals = null;

        // Dapatkan index kolom Skill
        int skillIndex = Array.FindIndex(headers, h => h.Equals("Skill", StringComparison.OrdinalIgnoreCase));

        for (int i = 4; i < lines.Length; i++)
        {
            var columns = lines[i].Split(';');

            if (columns[0].Equals("Totals", StringComparison.OrdinalIgnoreCase))
            {
                totals = new Dictionary<string, string>();
                for (int j = 0; j < headers.Length; j++)
                {
                    totals[headers[j]] = j < columns.Length ? columns[j] : "0";
                }
            }
            else if (columns.Length >= 5)
            {
                if (skillIndex >= 0 && skillIndex < columns.Length)
                {
                    var lineSkills = columns[skillIndex]
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim().ToLower());

                    if (!skillList.Any() || lineSkills.Any(s => skillList.Contains(s)))
                    {
                        var record = new Dictionary<string, string>();
                        for (int j = 0; j < headers.Length; j++)
                        {
                            record[headers[j]] = j < columns.Length ? columns[j] : "";
                        }
                        records.Add(record);
                    }
                }
                else
                {
                    var record = new Dictionary<string, string>();
                    for (int j = 0; j < headers.Length; j++)
                    {
                        record[headers[j]] = j < columns.Length ? columns[j] : "";
                    }
                    records.Add(record);
                }
            }
        }

        // Hitung total CEF dan AHT per tanggal
        var groupByDate = records
            .GroupBy(r => r["Date"])
            .Select(g =>
            {
                double totalCef = 0;
                double totalAht = 0;

                foreach (var r in g)
                {
                    double.TryParse(r["Call Offered"], out var cef);
                    double.TryParse(r["AHT"], out var aht);

                    totalCef += cef;
                    totalAht += aht;
                }

                return new
                {
                    Date = g.Key,
                    TotalCoF = totalCef,
                    TotalAHT = totalAht
                };
            })
            .OrderBy(g => g.Date)
            .ToList();

        return new OkObjectResult(new
        {
            Headers = headers,
            Totals = totals,
            Data = records,
            TotalPerDate = groupByDate
        });

    }

    public async Task<IActionResult> ReadDataTodayFromFile(string path, string Skill)
    {
        var lines = await System.IO.File.ReadAllLinesAsync(path);

        if (lines.Length < 2)
        {
            return new BadRequestObjectResult("File tidak memiliki cukup baris.");
        }

        var metadataParts = lines[0].Split(';').Select(v => v.Trim()).ToArray();
        var metadata = new Dictionary<string, string>
        {
            [metadataParts[0]] = metadataParts.Length > 1 ? metadataParts[1] : ""
        };

        var headerIndex = 1;
        var headers = lines[headerIndex].Split(';').Select(h => h.Trim()).ToArray();

        var records = new List<Dictionary<string, string>>();

        // 🟡 Pisahkan Skill menjadi list (hapus spasi, case-insensitive opsional)
        var skillList = Skill.Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .Select(s => s.Trim())
                             .ToList();

        for (int i = headerIndex + 1; i < lines.Length; i++)
        {
            var columns = lines[i].Split(';').Select(c => c.Trim()).ToArray();
            if (columns.Length != headers.Length) continue;

            var record = new Dictionary<string, string>();
            for (int j = 0; j < headers.Length; j++)
            {
                record[headers[j]] = columns[j];
            }

            // ✅ Cek apakah skill termasuk dalam daftar yang dikirim
            if (record.ContainsKey("Split/Skill") && skillList.Contains(record["Split/Skill"]))
            {
                records.Add(record);
            }
        }

        return new OkObjectResult(new
        {
            Headers = headers,
            Data = records
        });
    }

    public class DataRecord
    {
        public DateTime Date { get; set; }
        public string SplitSkill { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int CallOffered { get; set; }
    }
    public class DataRecordToday
    {
        public string SplitSkill { get; set; }
        public string SkillState { get; set; }
        public int AgentsStaffed { get; set; }
        public int AvgAbanTime { get; set; }
        public int AbanCalls { get; set; }
        public int AvgACDTime { get; set; }
        public int ACDCalls { get; set; }
        public int AvgSpeedAns { get; set; }
        public int OldestCallWaiting { get; set; }
        public int CallsOffered { get; set; }
        public int PctWithinServiceLevel { get; set; }
        public int PctAbanCalls { get; set; }
        public int AHT { get; set; }
    }

}
