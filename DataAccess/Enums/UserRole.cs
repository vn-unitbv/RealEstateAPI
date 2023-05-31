using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Enums
{
    public enum UserRole
    {
        [Description("User")]
        User,
        [Description("Administrator")]
        Administrator
    }
}
