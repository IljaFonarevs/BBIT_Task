using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Uzd2.Controllers;
using Uzd2.Datatypes;
using Uzd2.Services;
namespace Uzd2.Policies
{
    public class ApartmentNumberRequirmentHouse : IAuthorizationRequirement
    {
    }
    public class ApartmentNumberHandlerHouse : AuthorizationHandler<ApartmentNumberRequirmentHouse>
    {
        private readonly Uzd2Context _context;

        // Inject DbContext into the handler's constructor
        public ApartmentNumberHandlerHouse(Uzd2Context context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApartmentNumberRequirmentHouse requirement)
        {


            // Check if the user has the "ApartmentNumber" claim
            var apartmentNumberClaim = context.User.FindFirst(c => c.Type == "ApartmentNumber");
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (apartmentNumberClaim != null)
            {

                // Get the apartment number from the claim
                var userApartmentNumber = apartmentNumberClaim.Value;

                // Retrieve the apartment number from the current request (e.g., from route data)
                var requestedApartmentNumber = context.Resource as HttpContext;

                if (requestedApartmentNumber != null)
                {
                    // For example, get the apartment number from the route
                    var routeApartmentNumber = requestedApartmentNumber.Request.RouteValues["id"].ToString();
                    

                    var apart =  _context.DzivoklisItems.Find(long.Parse(userApartmentNumber));
                    

                    if(apart.MajaID == long.Parse(routeApartmentNumber)) context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
