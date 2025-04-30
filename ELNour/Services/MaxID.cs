using DataBaseOperations;
using ELNour.Classes;
using MessageBoxes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELNour.Services
{
    class MaxID : IMaxID
    {
        DatabaseConnection con;
        public MaxID()
        {
            con = new DatabaseConnection(Connections.Constr);
        }
        public int MaxIDs(string Coulmn_Name, string TableDb)
        {
            int dt = new int();
            
            try
            {
                if (con.Connection.State == ConnectionState.Open)
                    con.CloseConnection();
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con.Connection;
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = ("Select Max(" + Coulmn_Name + ") From " + TableDb + "");
                con.OpenConnection();
                dt = Convert.ToInt32(Cmd.ExecuteScalar());
                return dt;

            }
            catch
            {
                dt = 0;
                con.CloseConnection();
                return dt;
            }
        }
        public int MaxIDs(string Coulmn_Name, string TableDb, string condi, int value)
        {
            int dt = new int();
            try
            {
                if (con.Connection.State == ConnectionState.Open)
                    con.CloseConnection();
                con.OpenConnection();
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con.Connection;
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = ("Select Max(" + Coulmn_Name + ") From " + TableDb + " where " + condi + " = " + value + "");
                dt = Convert.ToInt32(Cmd.ExecuteScalar());
                return dt;

            }
            catch
            {
                dt = 0;
                con.CloseConnection();
                return dt;
            }
        }
    }
}
