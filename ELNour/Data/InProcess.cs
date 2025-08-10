using DataBaseOperations;
using DevExpress.Utils.About;
using ELNour.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELNour.Data
{
    internal class InProcess
    {
        public int BoxesCount { get; set; } = 0;
        public decimal PalleteWeight { get; set; } = 0;
        public decimal BoxWeight { get; set; } = 0;
        public decimal BoxesWeight { get; set; } = 0;
        public decimal Weight { get; set; } = 0;
        public decimal DiscountWeight { get; set; } = 0;
        public decimal TotalDiscount { get; set; } = 0;
        public decimal NetWeight { get; set; } = 0;
        DatabaseConnection con;
        public InProcess(int recieveId,
            int productId)
        {
            con = new DatabaseConnection(Connections.Constr);
            fillData(recieveId, productId);
        }
        private void fillData(int recieveId, int productId)
        {
            try
            {
                if (con.Connection.State == ConnectionState.Closed) { con.OpenConnection(); }
                string query = $@"Select * From InProcess_tbl Where ReciveId =@RecieveId and ProductId=@ProductId";
                using (SqlDataAdapter da = new SqlDataAdapter(query, con.Connection))
                {
                    da.SelectCommand.Parameters.Add("@ReciveId", SqlDbType.Int).Value = recieveId;
                    da.SelectCommand.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        BoxesCount = Convert.ToInt32(dt.Rows[0]["BoxesCount"]);
                        PalleteWeight = Convert.ToInt32(dt.Rows[0]["PalleteWeight"]);
                        BoxWeight = Convert.ToInt32(dt.Rows[0]["BoxWeight"]);
                        BoxesWeight = Convert.ToInt32(dt.Rows[0]["BoxesWeight"]);
                        Weight = Convert.ToInt32(dt.Rows[0]["Weight"]);
                        DiscountWeight = Convert.ToInt32(dt.Rows[0]["DiscountWeight"]);
                        TotalDiscount = Convert.ToInt32(dt.Rows[0]["TotalDiscount"]);
                        NetWeight = Convert.ToInt32(dt.Rows[0]["NetWeight"]);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.Connection.State == ConnectionState.Open) { con.CloseConnection(); }
            }


        }
    }
}
