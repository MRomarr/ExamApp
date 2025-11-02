using ExamApp.API.Extensions;
using ExamApp.Application;
using ExamApp.Infrastructure;
using ExamApp.Infrastructure.Configurations;
using Hangfire;
using HangfireBasicAuthenticationFilter;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddPresentation()
    .AddApplication()
    .AddConfiguration(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    #region swagger
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExamApp");
        c.RoutePrefix = string.Empty;
    });
    #endregion
}

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireCustomBasicAuthenticationFilter(){
        User = "admin",
        Pass = "admin"
    } },
});
HangfireJobsConfigurator.ConfigureRecurringJobs();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
