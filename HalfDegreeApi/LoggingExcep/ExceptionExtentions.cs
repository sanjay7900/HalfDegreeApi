using HalfDegreeApi.ExceptionHandler;

namespace HalfDegreeApi.LoggingExcep
{
    public static class ExceptionExtentions
    {
        public static IApplicationBuilder ApplyCustomException(this IApplicationBuilder app)
        {
             return app.UseMiddleware<LoggingExceptionsInFile>();

        }
    }
}
