using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Utilities
{
    public class KeyGeneratorService
    {
        public KeyGeneratorService() { }


        public string GenerateSecretKey(int length = 9)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[length];
                rng.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }
    }
}
