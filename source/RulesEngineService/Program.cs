var builder = WebApplication.CreateBuilder();

builder.Services.AddResponseCompression();
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

var application = builder.Build();

application.UseDeveloperExceptionPage();
application.UseHsts();
application.UseHttpsRedirection();
application.UseResponseCompression();
application.MapControllers();

application.Run();
