using Arizona.APIs.Errors;
using Arizona.APIs.Helpers;
using Arizona.Application.AuthService;
using Arizona.Application.OrderService;
using Arizona.Application.PaymentService;
using Arizona.Application.ProductService;
using Arizona.Core;
using Arizona.Core.Entities.Identity;
using Arizona.Core.Repositories.Contract;
using Arizona.Core.Services.Contract;
using Arizona.Infrastructure;
using Arizona.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
namespace Arizona.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfiles)); //allow DI for auto mapper

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToList();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            services.AddScoped(typeof(IOrderService), typeof(OrderService));

            services.AddScoped(typeof(IProductService), typeof(ProductService));

            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

            return services;
        }
    }
}
