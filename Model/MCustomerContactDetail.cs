using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MCustomerContactDetail
    {
        public string ContactId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string DisplayPicture { get; set; }
        public int? Followers { get; set; }
        public int? Following { get; set; }
    }
}
