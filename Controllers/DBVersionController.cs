﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using product_and_receipt.Models;
using product_and_receipt.Models.DBs.Tables;

namespace product_and_receipt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DBVersionController : ControllerBase
    {
        private readonly ILogger<DBVersionController> _logger;

        public DBVersionController(ILogger<DBVersionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            DBVersion version = GlobalInstance.DB.DBInfoTable.GetVersion();

            return version.ToString();
        }

        [HttpPost]
        public bool Post([FromBody]string value)
        {
            if (value == "1111")
            {
                GlobalInstance.DB.RecordTable.Insert("Update DB Version", null);

                string info = GlobalInstance.DB.UpdateDBVersion();
                GlobalInstance.DB.RecordTable.Insert("Update DB Version Done", info);

                return true;
            }
            if (value == "1122")
            {
                GlobalInstance.DB.DBInfoTable.Backup(GlobalInstance.ContentRoot);
                GlobalInstance.DB.RecordTable.Insert("Backup DB Done", null);

                return true;
            }
            return false;
        }
    }
}
