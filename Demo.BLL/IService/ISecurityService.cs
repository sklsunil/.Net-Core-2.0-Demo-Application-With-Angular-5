using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.BLL.IService
{
    public interface ISecurityService
    {
        string GetSha256Hash(string input);
    }
}
