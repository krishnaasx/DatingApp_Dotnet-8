using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions {
    public static class IdentityServiceExtension {
        
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config) {
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                var tokenKey = config["TokenKey"] ?? throw new Exception("Token not found");
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            return services;
        }
    }
}