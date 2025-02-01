namespace API.BuilderServices
{
    public static class CORSServiceExtensions
    {
        public static IServiceCollection AddCORSServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("generic", policy =>
                {
                    policy.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .WithExposedHeaders("Access-Control-Allow-Origin", "X-Pagination", "Content-Type");
                });
            });

            return services;
        }
    }
}