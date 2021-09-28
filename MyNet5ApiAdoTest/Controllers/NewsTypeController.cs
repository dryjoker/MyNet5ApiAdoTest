using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNet5ApiAdoTest.Models;
using MyNet5ApiAdoTest.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNet5ApiAdoTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsTypeController : ControllerBase
    {
        [HttpPost("Add")]
        public ActionResult<int> AddNewsType(NewsType newsType)
        {
            int RowCount = 0;
            if (newsType != null)
            {
                string strSQL = @"INSERT INTO NewsType (NewsTypeName,IsEnabled)
                                VALUES (@NewsTypeName,@IsEnabled)";

                Hashtable htParms = new Hashtable();
                htParms.Add("@NewsTypeName", newsType.NewsTypeName);
                htParms.Add("@IsEnabled", newsType.isEnabled);
                RowCount = MSSQLHelper.ExecuteNonQuery(strSQL, htParms);
                return CreatedAtAction(nameof(AddNewsType), RowCount);
            }
            return RowCount;
        }

    }
}
