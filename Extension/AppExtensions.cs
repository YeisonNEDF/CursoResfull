using WebApi.Middlewares;

namespace WebApi.Extension
{
    public static class AppExtensions
    {
        public static void UseErrorHandlerMiddelware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddelware>();
        }
    }
}
