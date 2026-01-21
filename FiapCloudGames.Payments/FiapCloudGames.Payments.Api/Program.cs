using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using FiapCloudGames.Payments.Api;
using FiapCloudGames.Payments.Api.Extensions;
using FiapCloudGames.Payments.Api.Utils;
using FiapCloudGames.Payments.Infrastructure;
using FiapCloudGames.Payments.Infrastructure.Data;
using FiapCloudGames.Payments.Infrastructure.Messaging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiModule();
builder.Services.AddInfraModule(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Exemplo: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });

    options.ExampleFilters();
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PaymentsDb")));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderPlacedEventConsumer>();
    x.AddConsumer<UserCreatedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]!);
            h.Password(builder.Configuration["RabbitMQ:Password"]!);
        });

        cfg.UseMessageRetry(r =>
        {
            r.Exponential(
                retryLimit: 5,
                minInterval: TimeSpan.FromSeconds(1),
                maxInterval: TimeSpan.FromSeconds(5),
                intervalDelta: TimeSpan.FromSeconds(1)
            );
        });

        cfg.ReceiveEndpoint("payments-usercreated-queue", e =>
        {
            e.ConfigureConsumer<UserCreatedEventConsumer>(context);
        });

        cfg.ReceiveEndpoint("payments-orderplaced-queue", e =>
        {
            e.ConfigureConsumer<OrderPlacedEventConsumer>(context);
        });
    });
});

builder.Services.AddHealthChecks();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options => provider.ApiVersionDescriptions.ToList()
        .ForEach(
            description => options.SwaggerEndpoint(
                url: $"/swagger/{description.GroupName}/swagger.json",
                name: description.GroupName.ToUpperInvariant()
            )
        )
    );
}

app.UseErrorHandlingMiddleware();

app.UseRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

await app.RunAsync();