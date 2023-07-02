using Microsoft.AspNetCore.Mvc;
using signup_verification_refreshToken_api.Entities;

namespace signup_verification_refreshToken_api.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        // returns the current authenticated account (null if not logged in)
        public Account Account => (Account)HttpContext.Items["Account"];
    }
}
