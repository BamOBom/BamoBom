using EnumValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BomoBana.Areas.Admin
{
    public class AdminPanelUserDtoViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public UserType UserType { get; set; }
        public Status Status { get; set; }

    }
}
