using Microsoft.AspNetCore.Authorization;
using Uzd2.Controllers;
using Uzd2.Datatypes;
using Uzd2.Services;
namespace Uzd2.Policies
{
    public class ApartmentNumberRequirment : IAuthorizationRequirement
    {
    }
    public class ApartmentNumberHandler : AuthorizationHandler<ApartmentNumberRequirment>
    {
        
        protected  override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApartmentNumberRequirment requirement)
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
                    
                    

                    // Check if the user's apartment number matches the one in the request
                    if (userApartmentNumber == routeApartmentNumber)
                    {
                        context.Succeed(requirement); // Authorization succeeds if they match
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
