using Microsoft.AspNetCore.Authorization;
using Uzd2.Datatypes;

namespace Uzd2.Policies
{
    public class ApartmentNumberRequirmentResidentUpdate : IAuthorizationRequirement
    {
    }
    public class ApartmentNumberHandlerUpdate : AuthorizationHandler<ApartmentNumberRequirmentResidentUpdate>
    {
        private readonly Uzd2Context _context;

        // Inject DbContext into the handler's constructor
        public ApartmentNumberHandlerUpdate(Uzd2Context context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApartmentNumberRequirmentResidentUpdate requirement)
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
                    

                    var iedz =  _context.IedzivotajsItems.Find(long.Parse(routeApartmentNumber));
                    

                    if(iedz.DzivNumurs == long.Parse(userApartmentNumber))context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
