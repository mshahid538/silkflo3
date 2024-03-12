using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Services.ClientApplicationInterfaceSession.Command
{
    public class CreateClientApplicationInterfaceSessionCommandResult
    {
        public bool IsSucceed { get; set; }
        public bool Message { get; set; }
        public string Result { get; set; }
    }
}
