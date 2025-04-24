using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interfaces;
using Repository.Repositories;

public class Startup
{
    public IConfiguration _config
    {
        get;
    }

    public Startup(IConfiguration configuration)
    {
        _config = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add controllers
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<GenericContext>(options =>
           options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));


        // Repositories
        services.AddScoped<IGenericRepository, GenericRepository>();


        // Domain
        services.AddScoped<IGenericDomain, GenericDomain>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            // Map controller routes
            endpoints.MapControllers();
        });
    }
}


