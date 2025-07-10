using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// เพิ่มบริการ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// เพิ่ม DbContext เชื่อมต่อกับ MySQL
var connectionString = "server=localhost;database=testdb;user=root;password=";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// เปิดใช้ Swagger
app.UseSwagger();
app.UseSwaggerUI();

// GET: ดูสินค้าทั้งหมด
app.MapGet("/products", async (AppDbContext db) =>
    await db.Products.ToListAsync());

// GET: ดูสินค้าตาม id
app.MapGet("/products/{id}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

// POST: เพิ่มสินค้า
app.MapPost("/products", async (Product product, AppDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();

    return Results.Created($"/products/{product.Id}", product);
});

// PUT: แก้ไขสินค้า
app.MapPut("/products/{id}", async (int id, Product inputProduct, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    product.Name = inputProduct.Name;
    await db.SaveChangesAsync();

    return Results.Ok(product);
});

// DELETE: ลบสินค้า
app.MapDelete("/products/{id}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    db.Products.Remove(product);
    await db.SaveChangesAsync();

    return Results.Ok(product);
});

app.Run();
