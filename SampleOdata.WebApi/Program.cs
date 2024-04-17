

using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using SampleOdata.Models;
using SampleOdata.Service;
using SampleOdata.Service.Implements;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
IServiceCollection services = builder.Services;
IdentityModelEventSource.ShowPII = true;
services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
    x.JsonSerializerOptions.WriteIndented = true;
}).AddOData(options => options.Select().Filter().OrderBy().Expand().EnableQueryFeatures().AddRouteComponents(GetEdmModel()));
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = int.MaxValue;
});
//string connectionString = builder.Configuration["ConnectionStrings:value"];
string connectionString = builder.Configuration["value"];
builder.Services.AddDbContext<NorthWindContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});


services.AddRouting();
services.AddTransient<IProductService, ProductService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mirror API", Version = "v1", Description = "Version: 1.0.0" });
   
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
        c.RoutePrefix = "DocumentAPI";

    });
}
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});


app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<Product>("Products").EntityType.HasKey(x => x.ProductId).Expand(5).Count().Page(100, 100);
    return odataBuilder.GetEdmModel();
}