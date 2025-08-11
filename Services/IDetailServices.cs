using static DetailServices;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WEBAPI_Bravo.Services
{
    public interface iDetailServices
    {
        CallCenterData ReadDataFromTxt(string filePath);
        CallCenterData ReadDataFromTxtNew(string filePath, string skill);
        Task<IActionResult> ReadDataCallFromFile(string path, string skill);
        Task<IActionResult> ReadDataTodayFromFile(string path,string skill);
       
    }
}
