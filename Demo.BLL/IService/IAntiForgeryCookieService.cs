using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Demo.BLL.IService
{
    public interface IAntiForgeryCookieService
    {
        void RegenerateAntiForgeryCookies(IEnumerable<Claim> claims);
        void DeleteAntiForgeryCookies();
    }
}
