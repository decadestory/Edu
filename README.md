# Edu
## .net core 3.1 开发框架 - efcore codefirst, 全局错误处理，登录认证，参数检查，autofac属性注入，MapSter model mapper转换,SHA-Hash加密,日志服务，代码自成器
### EFcore 

``` csharp
 public interface IAContext
 {
    DbContextOption Option { get; }
    DatabaseFacade GetDatabase();
    DbSet<T> Set<T>() where T : BaseEntity;
    int SaveChanges();
    bool EnsureCreated();

    void IsEntityValid<T>(T entity) where T : BaseEntity;

    void BulkInsert<T>(IList<T> entities, string destinationTableName = null) where T : class;
    DataTable GetDataTable(string sql, params DbParameter[] parameters);
}
```

### autufac

``` csharp
public void ConfigureContainer(ContainerBuilder builder)
{
    //注册controller 注册之后才可以在controller里使用Autofac属性注入
    var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
    .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
    builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();

    //注册DbContext为Scope模式
    var connStr = Configuration.GetValue<string>("ConnectionStrings:DbConn");
    builder.RegisterType<AContext>().As<IAContext>().WithParameter(new TypedParameter(typeof(DbContextOption), new DbContextOption
    {
        ConnectionString = connStr,
        ModelAssemblyName = "05.Edu.Entity"
    })).PropertiesAutowired().InstancePerLifetimeScope();

    //注册Service,Repository
    Assembly service = Assembly.Load("02.Edu.Svc");
    Assembly repository = Assembly.Load("03.Edu.Repo");
    builder.RegisterAssemblyTypes(service).Where(t => t.Name.EndsWith("Svc")).AsImplementedInterfaces().PropertiesAutowired();
    builder.RegisterAssemblyTypes(repository).Where(t => t.Name.EndsWith("Repo")).AsImplementedInterfaces().PropertiesAutowired();
}
```


### MapSter

```csharp
public int AddOrEditUser(UserModel user, UserTokenModel curUser)
{
    var ue = user.Adapt<User>();
    var pwd = Atom.Lib.Security.CryptographyUtils.Pwd(ue.Password);
    ue.Password = pwd.Item1;
    ue.Salt = pwd.Item2;
    ue.AddUserId = curUser.UserId;
    ue.EditUserId = curUser.UserId;

    db.IsEntityValid(ue);
    db.Set<User>().Add(ue);
    return db.SaveChanges();
}
```

### ServiceStack.Text

```csharp
public static string Serialize<T>(T userToken, string loginType = "")
{
    var keyByteArray = Base64UrlTextEncoder.Decode(SecretKey);
    var userDataStr = JsonSerializer.SerializeToString(userToken);
    var claimCollection = new List<Claim> {
            new Claim(ClaimTypes.UserData, userDataStr),
        };

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claimCollection),
        Issuer = IssUser,
        Audience = AudienceId,
        //Expires = DateTime.Now.AddMinutes(30),
        Expires = loginType == "app" ? DateTime.Now.AddMonths(1) : DateTime.Now.AddDays(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyByteArray), SecurityAlgorithms.HmacSha256)
    };
    var tokenHandler = new JwtSecurityTokenHandler();

    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(securityToken);
}
```

### Jwt

```csharp
public class AuthAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {

        filterContext.HttpContext.Request.Headers.TryGetValue("Jwt-Token", out StringValues token);
        if (!token.Any())
        {
            var errorBr = new Br<string>("拒绝访问", -1, "没有身份认证");
            filterContext.Result = new JsonResult(errorBr);
            return;
        }

        var isValid = AuthorizeUtils.Validate(token);
        if (!isValid)
        {
            var errorBr = new Br<string>("拒绝访问", -1, "身份认证失败");
            filterContext.Result = new JsonResult(errorBr);
            return;
        }

        filterContext.HttpContext.Items["curUserInfo"] = AuthorizeUtils.GetCurUser<UserTokenModel>(token);

        base.OnActionExecuting(filterContext);

    }

}
```
### swagger


![](https://github.com/decadestory/Edu/blob/master/swagger.png?raw=true)
