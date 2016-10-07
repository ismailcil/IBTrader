using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IB.Db.Model;
using IB.Core.Tool;
using IB.Core.Db;
using IB.Core.Extensions;

namespace IB.Db.Function
{
    public static class FnIntraDayData
    {
        public static MdlIntraDayData GetIntraDayData(long id)
        {
            const string query = "Select * from IntraDayData(nolock) where id = @id";

            var data = DbClass.ReturnDataSet(query, ToolGeneralParameter.ConnectionString,
                DbClass.CreateParameter("id", id));
            if (data == null)
                return null;
            if (data.Tables.Count == 0)
                return null;
            return data.Tables[0].Rows.Count == 0 ? null : data.Tables[0].Rows[0].ConvertDataRowToEntity<MdlIntraDayData>();
        }

        public static IEnumerable<MdlIntraDayData> GetAllByTickerId(int tickerId)
        {
            const string query = "Select * from IntraDayData(nolock) where TickerId = @TickerId";
            var data = DbClass.ReturnDataSet(query, ToolGeneralParameter.ConnectionString,
                DbClass.CreateParameter("TickerId", tickerId));
            return data == null ? null : data.Tables[0].ConvertToList<MdlIntraDayData>();
        }

        public static IEnumerable<MdlIntraDayData> GetAllByTickerIdWithDate(int tickerId, DateTime date1, DateTime date2)
        {
            const string query = "Select * from IntraDayData(nolock) where TickerId = @TickerId and Date Between @date1 and @date2";
            var data = DbClass.ReturnDataSet(query, ToolGeneralParameter.ConnectionString,
                DbClass.CreateParameter("TickerId", tickerId),
                DbClass.CreateParameter("date1", date1),
                DbClass.CreateParameter("date2", date2));
            return data == null ? null : data.Tables[0].ConvertToList<MdlIntraDayData>();
        }
    }
}
