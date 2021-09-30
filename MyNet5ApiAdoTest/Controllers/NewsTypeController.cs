using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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


        [HttpDelete("Delete")]
        public ActionResult<int> DeleteNewsType(int? id)
        {
            if (id == null)
                return NotFound();

            string strSQL = @"delete from NewsType where NewsTypeId=@Id";
            Hashtable htParms = new Hashtable();
            htParms.Add("@Id", id);
            int RowCount = MSSQLHelper.ExecuteNonQuery(strSQL, htParms);
            return RowCount;
        }

        [HttpGet("GetById")]
        public ActionResult<NewsType> GetSpecificNewsTypeById(int? id)
        {
            if (id == null)
                return NotFound();

            string strSQL = "select * from NewsType where NewsTypeId=@Id";
            Hashtable htParams = new Hashtable();
            htParams.Add("@Id", id);
            SqlDataReader dataReader = MSSQLHelper.GetSqlDataReader(strSQL, htParams);
            NewsType newsType = new NewsType();
            while (dataReader.Read())
            {
                newsType.NewsTypeId = dataReader.GetInt32(0);
                newsType.NewsTypeName = dataReader.GetString(1);
                newsType.isEnabled = dataReader.GetBoolean(2);
            }
            dataReader.Close();
            return newsType;
        }

        [HttpPut("Update")]
        public ActionResult<int> UpdateNewsType(NewsType newsType)
        {
            int RowCount = 0;
            if (newsType != null)
            {
                int enabled = (bool)newsType.isEnabled ? 1 : 0;
                string strSQL = @"update NewsType set NewsTypeName=@NewsTypeName , IsEnabled=@IsEnabled 
                                    where NewsTypeId=@NewsTypeId ";
                Hashtable htParams = new Hashtable();
                htParams.Add("@NewsTypeId", newsType.NewsTypeId);
                htParams.Add("@NewsTypeName", newsType.NewsTypeName);
                htParams.Add("@IsEnabled", newsType.isEnabled);
                
                RowCount = MSSQLHelper.ExecuteNonQuery(strSQL, htParams);
            }
            return RowCount;
        }


        [HttpGet("Show")]
        public ActionResult<List<NewsType>> ShowNewsType()
        {
            string strSQL = @"select * from NewsType";
            Hashtable htParms = new Hashtable();
            SqlDataReader dataReader = MSSQLHelper.GetSqlDataReader(strSQL);
            if (!dataReader.HasRows)
                return NotFound();

            List<NewsType> lsNewsType = new List<NewsType>();

            while (dataReader.Read())
            {
                lsNewsType.Add(new NewsType()
                {
                    NewsTypeId = dataReader.GetInt32(0),
                    NewsTypeName = dataReader.GetString(1),
                    isEnabled = dataReader.GetBoolean(2)
                });
            }
            dataReader.Close();
            return lsNewsType;
        }

    }
}
