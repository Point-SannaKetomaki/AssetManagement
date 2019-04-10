using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementWeb.Models
    // sisältää jsonin sisältämän datan
{
    public class AssetLocationModel
    {
        public string AssetCode { get; set; }
        public string LocationCode { get; set; }
    }
}