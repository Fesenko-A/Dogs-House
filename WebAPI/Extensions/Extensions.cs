using WebAPI.Handlers;

namespace WebAPI.Extensions {
    public static class Extensions {
        public static void AddGlobalExceptionHandler(this IServiceCollection services) {
            services.AddExceptionHandler<GlobalExceptionHandler>();
        }
    }
}
