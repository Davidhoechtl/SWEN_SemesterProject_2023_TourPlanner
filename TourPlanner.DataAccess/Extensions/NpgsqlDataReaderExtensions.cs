using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataAccess.Extensions
{
    public static class NpgsqlDataReaderExtensions
    {
        public static T GetValue<T>(this NpgsqlDataReader reader, string fieldName)
        {
            if (reader.IsOnRow)
            {
                object value = reader.GetValue(reader.GetOrdinal(fieldName));
                return (T)value;
            }

            throw new Exception("Reader is not on a row");
        }
    }
}
