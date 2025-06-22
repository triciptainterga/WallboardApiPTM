using static DetailServices;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WEBAPI_Bravo.Services
{
    public interface iDetailServices
    {
        CallCenterData ReadDataFromTxt(string filePath);
        Task<IActionResult> ReadDataCallFromFile(string path);
        Task<IActionResult> ReadDataTodayFromFile(string path);
       
    }
}
