using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public static class Enum
    {
        public enum Role
        {
            admin = 1,
            approver = 2,
            verifier = 3,
            executor = 4,
            student = 5
        }
    }
}
