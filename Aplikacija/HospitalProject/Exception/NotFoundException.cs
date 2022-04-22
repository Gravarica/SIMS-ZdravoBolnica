using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Exception
    {
        class NotFoundException : System.Exception
        {
            public NotFoundException()
            {

            }

            public NotFoundException(string message) : base(message)
            {

            }

            public NotFoundException(string message, System.Exception inner) : base(message, inner)
            {

            }
        }
    }

