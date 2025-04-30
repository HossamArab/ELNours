using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBaseOperations;

namespace ELNour.Classes
{
    internal class Fills
    {
        DatabaseConnection con;
        public Fills()
        {
            con = new DatabaseConnection(Connections.Constr);
        }
        public void fillComboBoxInDGV(string table, string value, string display, DataGridView dgv)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            dt.Clear();
            da = new SqlDataAdapter("Select * FROM " + table + " Where " + value + " > 0 ", con.Connection);
            da.Fill(dt);
            DataGridViewComboBoxColumn dgCol = (DataGridViewComboBoxColumn)dgv.Columns[0];
            if (dt.Rows.Count > 0)
            {
                dgCol.DataSource = dt;
                dgCol.DisplayMember = "" + display + "";
                dgCol.ValueMember = "" + value + "";
            }
            else
            {
                dgCol.DataSource = null;
            }
            con.Connection.Close();
        }
        public void fillComboBoxInDGV(string table, string value, string display, DataGridView dgv, int Coulmn)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            dt.Clear();
            da = new SqlDataAdapter("Select * FROM " + table + " Where " + value + " > 0 ", con.Connection);
            da.Fill(dt);
            DataGridViewComboBoxColumn dgCol = (DataGridViewComboBoxColumn)dgv.Columns[Coulmn];
            if (dt.Rows.Count > 0)
            {
                dgCol.DataSource = dt;
                dgCol.DisplayMember = "" + display + "";
                dgCol.ValueMember = "" + value + "";
            }
            else
            {
                dgCol.DataSource = null;
            }
            con.Connection.Close();
        }
        public void fillComboBoxInDGVAll(string table, string value, string display, DataGridView dgv)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            dt.Clear();
            da = new SqlDataAdapter("Select * FROM " + table + " ", con.Connection);
            da.Fill(dt);
            DataGridViewComboBoxColumn dgCol = (DataGridViewComboBoxColumn)dgv.Columns[0];
            if (dt.Rows.Count > 0)
            {
                dgCol.DataSource = dt;
                dgCol.DisplayMember = "" + display + "";
                dgCol.ValueMember = "" + value + "";
            }
            else
            {
                dgCol.DataSource = null;
            }
            con.Connection.Close();
        }
        public void fillComboBox(ComboBox cmb, string table, string value, string display)
        {
            try
            {
                if (con.Connection.State == ConnectionState.Open)
                {
                    con.Connection.Close();
                }
                con.Connection.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                dt.Clear();
                da = new SqlDataAdapter("Select " + value + " ," + display + " FROM " + table + " Where " + value + " > 0 ", con.Connection);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cmb.DataSource = dt;
                    cmb.DisplayMember = "" + display + "";
                    cmb.ValueMember = "" + value + "";

                }
                else
                {
                    cmb.DataSource = null;
                }
                con.Connection.Close();
            }
            catch
            {
                con.Connection.Close();
            }
        }

        public void fillComboBox(ComboBox cmb, string table, string value, string display, string condi, int condVal)
        {
            try
            {
                if (con.Connection.State == ConnectionState.Open)
                {
                    con.Connection.Close();
                }
                con.Connection.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                dt.Clear();
                da = new SqlDataAdapter("Select " + value + " ," + display + " FROM " + table + " Where " + value + " > 0 and " + condi + " = " + condVal + " ", con.Connection);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cmb.DataSource = dt;
                    cmb.DisplayMember = "" + display + "";
                    cmb.ValueMember = "" + value + "";

                }
                else
                {
                    cmb.DataSource = null;
                }
                con.Connection.Close();
            }
            catch
            {
                con.Connection.Close();
            }
        }

        public void fillComboboxAll(ComboBox cmb, string table, string value, string display)
        {
            try
            {
                if (con.Connection.State == ConnectionState.Open)
                {
                    con.Connection.Close();
                }
                con.Connection.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                dt.Clear();
                da = new SqlDataAdapter("Select " + value + " ," + display + " FROM " + table + " ", con.Connection);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    
                    cmb.DataSource = dt;
                    cmb.DisplayMember = "" + display + "";
                    cmb.ValueMember = "" + value + "";
                    cmb.SelectedIndex = -1;

                }
                else
                {
                    cmb.DataSource = null;
                }
                con.Connection.Close();
            }
            catch
            {
                con.Connection.Close();
            }
        }
    }
}
