using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace LibraryManagement 
{
    public class DataAccess
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public string PenaltyForCountry(int NoOfDays,string Country)
        {
            int BusinessDays = NoOfDays - 10;
            var PenaltyAmount = 0m;
            var currency = "$";
            decimal Amount = 0m;
            var response = "Penalty Amount is ${PenaltyAmount}${currency}";
            try
            {
                if(NoOfDays > 10)
                {
                    PenaltyAmount = Convert.ToDecimal(ConfigurationManager.AppSettings["PenaltyAmount"]);
                    if (BusinessDays > 0)
                        PenaltyAmount = BusinessDays * PenaltyAmount;
                    
                }
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();

                    string query = "Insert into BusinessDaysForCountry(TotalDays,PenatlyAmount,CountryId)";
                    query += " Values(@TotalDays,@PenaltyAmount,@CountryId)";

                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@TotalDays", NoOfDays);
                    com.Parameters.AddWithValue("@PenaltyAmount", PenaltyAmount);
                    com.Parameters.AddWithValue("@CountryId", Country);
                    int rows = com.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        StringBuilder sb = new StringBuilder(response);
                        decimal fine = PenaltyAmount;
                        if (Country == "1")
                        {
                            sb.Replace("${PenaltyAmount}",fine.ToString("N2")).Replace("${currency}", "Rs");
                        }
                        if(Country == "2")
                        {
                            sb.Replace("${PenaltyAmount}", fine.ToString("N2")).Replace("${currency}", currency);
                        }
                        return sb.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return response;
        }
    }
}