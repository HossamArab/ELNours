using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELNour.Data
{
    internal class UserPermission
    {
        public static bool Vendor { get; set; } = false;
        public static bool AddVendor { get; set; } = false;
        public static bool EditVendor { get; set; } = false;
        public static bool DeleteVendor { get; set; } = false;
        public static bool ExportVendor { get; set; } = false;
        public static bool ShowVendor { get; set; } = false;
        public static bool PrintVendor { get; set; } = false;
        public static bool Customer { get; set; } = false;
        public static bool AddCustomer { get; set; } = false;
        public static bool EditCustomer { get; set; } = false;
        public static bool DeleteCustomer { get; set; } = false;
        public static bool ExportCustomer { get; set; } = false;
        public static bool ShowCustomer { get; set; } = false;
        public static bool PrintCustomer { get; set; } = false;
        public static bool Product { get; set; } = false;
        public static bool AddProduct { get; set; } = false;
        public static bool EditProduct { get; set; } = false;
        public static bool DeleteProduct { get; set; } = false;
        public static bool ExportProduct { get; set; } = false;
        public static bool ShowProduct { get; set; } = false;
        public static bool PrintProduct { get; set; } = false;
        public static bool Users { get; set; } = false;
        public static bool AddUsers { get; set; } = false;
        public static bool EditUsers { get; set; } = false;
        public static bool DeleteUsers { get; set; } = false;
        public static bool ExportUsers { get; set; } = false;
        public static bool ShowUsers { get; set; } = false;
        public static bool printUsers { get; set; } = false;
        public static bool Settings { get; set; } = false;
        public static bool Operation { get; set; } = false;
        public static bool OutOperation { get; set; } = false;
        public static bool MakeReceive { get; set; } = false;
        public static bool EditMakeReceive { get; set; } = false;
        public static bool DeleteMakeReceive { get; set; } = false;
        public static bool ManageRecieve { get; set; } = false;
        public static bool ManageSupply { get; set; } = false;
        public static bool AllOperations { get; set; } = false;
        public static bool TakeBakeUp { get; set; } = false;
        public static bool RestoreBackUp { get; set; } = false;
    }
}
