using MesMicroservice.Api.Application.Mapping;
using Microsoft.IdentityModel.Tokens;
using MesMicroservice.Domain.SeedWork;
using MesMicroservice.Domain.AggregateModels.MaterialDefinitionAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialLotAggregate;
using MesMicroservice.Domain.AggregateModels.EquipmentClassAggregate;
using MesMicroservice.Domain.AggregateModels.EquipmentAggregate;
using MesMicroservice.Domain.AggregateModels.HierarchyModelAggregate;
using MesMicroservice.Domain.AggregateModels.ManufacturingOrderAggregate;
using MesMicroservice.Domain.AggregateModels.WorkOrderAggregate;
using MesMicroservice.Api.Application.Buffers;
using MesMicroservice.Domain.AggregateModels.WorkCenterOutputAggregate;
using MesMicroservice.Domain.AggregateModels.ResourceRelationshipNetworkAggregate;
using MesMicroservice.Domain.AggregateModels.ManufacucturingRecordAggregate;
using MesMicroservice.Domain.AggregateModels.MaterialClassAggregate;
using MesMicroservice.Api.Application.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .WithOrigins("localhost", "http://localhost:5173", "https://web-thaiduong-mes.vercel.app")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration.GetValue("Authority", "");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddSignalR();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MesMicroservice.Api"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile));
builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblyContaining<ModelToViewModelProfile>();
        cfg.RegisterServicesFromAssemblyContaining<ApplicationDbContext>();
        cfg.RegisterServicesFromAssemblyContaining<Entity>();
    });

builder.Services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
builder.Services.AddScoped<IMaterialClassRepository, MaterialClassRepository>();
builder.Services.AddScoped<IMaterialDefinitionRepository, MaterialDefinitionRepository>();
builder.Services.AddScoped<IMaterialLotRepository, MaterialLotRepository>();
builder.Services.AddScoped<IEquipmentClassRepository, EquipmentClassRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IManufacturingOrderRepository, ManufacturingOrderRepository>();
builder.Services.AddScoped<IManufacturingRecordRepository, ManufacturingOrderRecordRepository>();
builder.Services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
builder.Services.AddScoped<IWorkCenterOutputRepository, WorkCenterOutputRepository>();
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<IResourceRelationshipNetworkRepository, ResourceRelationshipNetworkRepository>();

builder.Services.AddSingleton<DueDateBuffer>();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    }
));

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/notificationHub");

app.Run();
