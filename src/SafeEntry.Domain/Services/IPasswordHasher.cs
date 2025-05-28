using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeEntry.Domain.Services
{
    public interface IPasswordHasher
    {
        string Hash(string plain);
        bool Verify(string plain, string hash);
    }

}
