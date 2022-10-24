using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using EventChart_AP;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromSeconds(1800);
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
});

//解決中文被編碼
builder.Services.AddSingleton(System.Text.Encodings.Web.HtmlEncoder.Create(System.Text.Unicode.UnicodeRanges.All));

//新增檔案壓縮(ResponseCompression)
builder.Services.AddResponseCompression(options =>
{
  options.Providers.Add<BrotliCompressionProvider>();
  options.Providers.Add<GzipCompressionProvider>();
  options.MimeTypes =
      ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/javascript", "application/json", "application/xml", "text/css", "text/html", "text/json", "text/plain", "text/xml" });
});

//API 參數支援 text 與 byte[]
builder.Services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
}

app.UseResponseCompression();

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "System",
    pattern: "System/{controller}/{action=List}/{id?}");

app.MapControllerRoute(
    name: "Management",
    pattern: "Management/{controller}/{action=List}/{id?}");


app.Run();
