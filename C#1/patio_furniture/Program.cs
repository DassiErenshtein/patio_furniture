using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opotion => opotion.AddPolicy("AllowAll",
    p => p.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()));
builder.Services.AddScoped<IDal_Repository.IDal<DTO_Command.Category>, Dal_Repository.CatFunc>();
builder.Services.AddScoped<IDal_Repository.IDalProd, Dal_Repository.ProdDal>();
builder.Services.AddScoped<IDal_Repository.IDal<DTO_Command.Company>, Dal_Repository.CompDal>();
builder.Services.AddScoped<IDal_Repository.IDalClient, Dal_Repository.ClientDal>();
builder.Services.AddScoped<IDal_Repository.IDalBuy, Dal_Repository.BuyDal>();
builder.Services.AddScoped<IDal_Repository.IDalPurchase, Dal_Repository.purchaseDal>();


builder.Services.AddScoped<IBll_Services.IBll<DTO_Command.Category>, Bll_Services.funcCat>();
builder.Services.AddScoped<IBll_Services.IProductBll, Bll_Services.funcProd>();
builder.Services.AddScoped<IBll_Services.IBll<DTO_Command.Company>, Bll_Services.funcComp>();
builder.Services.AddScoped<IBll_Services.IClientBll, Bll_Services.funcClient>();
builder.Services.AddScoped<IBll_Services.IBuyBll, Bll_Services.funcBuy>();

builder.Services.AddDbContext<Dal_Repository.models.PatioFurnitureContext>
(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connectionSql")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
