using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace VivesRental.Blazor.Security
{
    [Authorize]
    public class AdminPageComponent : ComponentBase
    {
    }
}
