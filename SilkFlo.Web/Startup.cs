using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Serialization;
using Extensions;
using Microsoft.AspNetCore.Hosting.Server.Features;
using SilkFlo.Web.Insert;
using static SilkFlo.Web.Insert.KeyValueData;
using SilkFlo.Web.Controllers2.FileUpload;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Features;
using SilkFlo.Security;
using SilkFlo.Data.Core;
using SilkFlo.Data.Persistence;
//using SilkFlo.ThirdPartyServices;

namespace SilkFlo.Web
{
    public class Startup
    {
#if RELEASE
        internal static string PageNotFound = "";
#endif
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }


        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to add services to the container.
        /// For more information on how to configure your application,
        /// visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // This is need for SilkFlo.Services.Authorization.AuthorizeTagHelper
                                                                                // services.AddThirdPartyServices(Configuration);

            //services.AddScoped<Data.Core.Persistence.ApplicationDbContext>();
            services.AddTransient<IAuthorizationHandler, Services.Authorization.DifferentUserHandler>();
            services.AddTransient<IAuthorizationHandler, Services.Authorization.AnyRoleHandler>();
            services.AddTransient<SilkFlo.Security.API.ReCaptcha.Interfaces.ISignUpService, SilkFlo.Security.API.ReCaptcha.SignUpService>();

            services.AddBlobServices(Configuration);

            services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);

            services.AddViewToString();
            //Added Third Party Services
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = _ => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            // Load test data here.
            services.AddScoped<Data.Core.IUnitOfWork, Data.Persistence.UnitOfWork>();
            // Scoped in this case means scoped to a HTTP request,
            // which also means it is a singleton while the current request is running.

            services
                .AddMvc(setupAction =>
                        {
                            setupAction.EnableEndpointRouting = false;
                        })
                .AddNewtonsoftJson(options =>
                                   options.SerializerSettings.ContractResolver =
                                      new CamelCasePropertyNamesContractResolver())
                .AddRazorRuntimeCompilation();



            #region Create Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.Subscriber,
                    policy => policy.RequireClaim("Role",
                                                  "Administrator",
                                                  "Account Owner",
                                                  "Program Manager",
                                                  "Idea Approver",
                                                  "Authorised User",
                                                  "Automation Sponsor",
                                                  "Standard User",
                                                  "Agency User",
                                                  "Agency Administrator"));

                options.AddPolicy(Policy.ViewIdeas,
                                policy => policy.RequireClaim("Role",
                                                              "Administrator",
                                                              "Account Owner",
                                                              "Program Manager",
                                                              "Idea Approver",
                                                              "Authorised User",
                                                              "Automation Sponsor",
                                                              "Standard User",
                                                              "Agency User"));

                options.AddPolicy(Policy.ShareEmployeeDrivenIdeas,
                                policy => policy.RequireClaim("Role",
                                                              "Administrator",
                                                              "Account Owner",
                                                              "Program Manager",
                                                              "Idea Approver",
                                                              "Authorised User",
                                                              "Automation Sponsor",
                                                              "Standard User",
                                                              "Agency User"));

                options.AddPolicy(Policy.SubmitCoEDrivenIdeas,
                                policy => policy.RequireClaim("Role",
                                                              "Administrator",
                                                              "Account Owner",
                                                              "Program Manager",
                                                              "Authorised User",
                                                              "Agency User"));

                options.AddPolicy(Policy.ReviewNewIdeas,
                                policy => policy.RequireClaim("Role",
                                                              "Administrator",
                                                              "Account Owner",
                                                              "Program Manager",
                                                              "Idea Approver",
                                                              "Agency User"));

                options.AddPolicy(Policy.ReviewAssessedIdeas,
                                policy => policy.RequireClaim("Role",
                                                              "Administrator",
                                                              "Account Owner",
                                                              "Program Manager",
                                                              "Agency User"));

                options.AddPolicy(Policy.EditAllIdeaFields,
                                policy => policy.RequireClaim("Role",
                                                              "Administrator",
                                                              "Account Owner",
                                                              "Program Manager",
                                                              "Agency User"));

                options.AddPolicy(Policy.EditIdeasStageAndStatus,
                                policy => policy.RequireClaim("Role",
                                                              "Administrator",
                                                              "Account Owner",
                                                              "Program Manager",
                                                              "Agency User"));

                options.AddPolicy(Policy.AssignProcessOwner,
                                policy => policy.RequireClaim("Role",
                                                              "Administrator",
                                                              "Account Owner",
                                                              "Program Manager",
                                                              "Idea Approver",
                                                              "Agency User"));

                options.AddPolicy(Policy.ArchiveIdeas,
                                    policy => policy.RequireClaim("Role",
                                                                  "Administrator",
                                                                  "Account Owner",
                                                                  "Program Manager",
                                                                  "Agency User"));

                options.AddPolicy(Policy.DeleteIdeas,
                                policy => policy.RequireClaim("Role",
                                                              "Administrator",
                                                              "Account Owner",
                                                              "Program Manager",
                                                              "Agency User"));

                options.AddPolicy(Policy.ViewReports,
                                policy => policy.RequireClaim("Role",
                                                              "Administrator",
                                                              "Account Owner",
                                                              "Program Manager",
                                                              "Automation Sponsor",
                                                              "Agency User"));

                options.AddPolicy(Policy.ViewTenantDashboards,
                                policy => policy.RequireClaim("Role",
                                                              "Administrator",
                                                              "Account Owner",
                                                              "Program Manager",
                                                              "Automation Sponsor",
                                                              "Agency User"));

                options.AddPolicy(Policy.ViewCostInfoInAutomationPipeline,
                                    policy => policy.RequireClaim("Role",
                                                                  "Administrator",
                                                                  "Account Owner",
                                                                  "Program Manager",
                                                                  "Authorised User",
                                                                  "Automation Sponsor",
                                                                  "Agency User"));


                options.AddPolicy(Policy.ManageTenantSettings,
                                    policy => policy.RequireClaim("Role",
                                                                  "Administrator",
                                                                  "Account Owner",
                                                                  "Agency User"));

                options.AddPolicy(Policy.ManageTenantUsers,
                                    policy => policy.RequireClaim("Role",
                                                                  "Administrator",
                                                                  "Account Owner",
                                                                  "Agency User"));

                options.AddPolicy(Policy.ManageTenantUserRoles,
                                    policy => policy.RequireClaim("Role",
                                                                  "Administrator",
                                                                  "Account Owner",
                                                                  "Agency Administrator",
                                                                  "Agency User"));



                options.AddPolicy(Policy.ManageAgencySettings,
                    policy => policy.RequireClaim("Role",
                                                  "Administrator",
                                                  "Agency Administrator"));

                options.AddPolicy(Policy.ManageAgencyUsers,
                                    policy => policy.RequireClaim("Role",
                                                                  "Administrator",
                                                                  "Agency Administrator"));

                options.AddPolicy(Policy.ManageAgencyUserRoles,
                                    policy => policy.RequireClaim("Role",
                                                                  "Administrator",
                                                                  "Agency Administrator"));






                options.AddPolicy(Policy.DifferentUser,
                                  policy => policy.Requirements.Add(new Services.Authorization.DifferentUserRequirement()));

                options.AddPolicy("Any Role",
                        policy => policy.Requirements.Add(new Services.Authorization.AnyRoleRequirement()));

                options.AddPolicy(Policy.CanViewAdministrationArea,
                    policy => policy.RequireClaim("Role",
                                                  "Administrator"));

                options.AddPolicy(Policy.CanBackupDataSet,
                    policy => policy.RequireClaim("Role",
                                                  "Administrator",
                                                  "Can Backup DataSet"));

                options.AddPolicy(Policy.Administrator,
                    policy => policy.RequireClaim("Role",
                                                  "Administrator"));
            });
            #endregion


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/signin";
                    options.LogoutPath = "/account/signout";
                });

            //services.AddAuthentication(options =>
            //{
            //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //options.DefaultSignInScheme = "AzureAD";
            //options.DefaultScheme = "Cookies";
            //options.SignInScheme = "AzureAD";
            //});

            //services.AddMicrosoftIdentityWebAppAuthentication(Configuration);
            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = 2048;
            });

        }


        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app, Data.Core.IUnitOfWork serviceProvider)
        {
#if DEBUG
            app.UseDeveloperExceptionPage();
#else
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
#endif

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "administration",
                    template: "Administration/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


#if RELEASE
            ////app.Run(async (context) =>
            ////{
            ////    if (string.IsNullOrWhiteSpace(Startup.PageNotFound))
            ////    {
            ////        using (var unitOfWork = new Data.Persistence.UnitOfWork())
            ////        {
            ////            var page = unitOfWork.ApplicationPages.GetAsync(Data.Core.Enumerators.Page.PageNotFound.ToString()).Result;
            ////            PageNotFound = page == null ? 
            ////                "404 Page Not found" : 
            ////                page.Text;
            ////        }
            ////    }
            ////    await context.Response.WriteAsync(PageNotFound);
            ////});
#endif
            Data.Persistence.UnitOfWork.GetDataSetAsync().GetAwaiter().GetResult();
            // InsertData(app, serviceProvider).GetAwaiter();
            

            SetupEmail(app).GetAwaiter().GetResult();
            SilkFlo.Web.Services2.Models.PaymentManager.SetUpWebHooks(); // Payment.Manager.SetUpWebHooks();
        }

        private static async Task InsertData(IApplicationBuilder app, Data.Core.IUnitOfWork unitOfWork)
        {

            //using (var scope = serviceProvider.CreateScope())
            //{
            //    var unitOfWork = scope.ServiceProvider.GetRequiredService<Data.Core.IUnitOfWork>();

            //using (var dataSet1 = new SilkFlo.Data.Core.Persistence.ApplicationDbContext())
            //{
            //    var unitOW = new Data.Persistence.UnitOfWork(dataSet1);
            //    var password = Security.Settings.Password;

            //    //Seed-Data SystemSettings
            //    //var setting = new Models.Application.Setting(unitOW);
            //    //await setting.CreateDefaultTrialPeriod();
            //    const int trialPeriod = 30;
            //    var id = Enumerators.Setting.TrialPeriod.ToString();
            //    var setting = await unitOW.ApplicationSettings.GetAsync(id);
            //    if (setting == null)
            //    {
            //        setting = new Data.Core.Domain.Application.Setting
            //        {
            //            Value = trialPeriod.ToString()
            //        };
            //        setting.Id = id;
            //        dataSet1.ApplicationSettings.Add(setting);
            //        await dataSet1.SaveChangesAsync();
            //    }

            //    //Seed-Data Roles
            //    var Roles = (SilkFlo.Data.Core.Repositories.IRoleRepository)new SilkFlo.Data.Persistence.Repositories.RoleRepository(unitOW);
            //    SilkFlo.Data.Core.Domain.Role role = await Roles.GetAsync("-2023");
            //    if (role == null)
            //    {
            //        role = new SilkFlo.Data.Core.Domain.Role()
            //        {
            //            Name = "Can Backup DataSet",
            //            Description = "",
            //            Sort = -2
            //        };
            //        role.Id = "-2023";
            //        dataSet1.Roles.Add(role);
            //    }
            //    role = await Roles.GetAsync("-2022");
            //    if (role == null)
            //    {
            //        role = new SilkFlo.Data.Core.Domain.Role()
            //        {
            //            Name = "UAT Tester",
            //            Description = "",
            //            Sort = -1
            //        };
            //        role.Id = "-2022";
            //        dataSet1.Roles.Add(role);
            //    }
            //    role = await Roles.GetAsync("-2021");
            //    if (role == null)
            //    {
            //        role = new SilkFlo.Data.Core.Domain.Role()
            //        {
            //            Name = "Administrator",
            //            Description = "<p>A user with this role has complete access to the solutions and it's data.</p>",
            //            Sort = 0
            //        };
            //        role.Id = "-2021";
            //        dataSet1.Roles.Add(role);
            //    }
            //    await dataSet1.SaveChangesAsync();

            //    //Seed-Data SystemRoles
            //    KeyValueData keyValueData = new KeyValueData();
            //    await keyValueData.InsertSystemRolesAsync(unitOW);

            //    var email = "admin@scriptbot.io";
            //    var adminUser = await unitOW.Users.GetByEmailAsync(email);
            //    if (adminUser == null) {
            //        adminUser = await Services.Authorization.User
            //                        .CreateAsync("Douglas",
            //                            "Adams", email, password,
            //                            Data.Core.Enumerators.Role.Administrator,
            //                            unitOW,
            //                            true);
            //        await unitOW.CompleteAsync();
            //    }

            //    var email2 = "jimmy@silkflo.com";
            //    var powerUser = await unitOW.Users.GetByEmailAsync(email2);
            //    if (powerUser == null)
            //    {
            //        powerUser = await Services.Authorization.User
            //                        .CreateAsync("Jimmy",
            //                            "McGill", email2, password,
            //                            Data.Core.Enumerators.Role.Administrator,
            //                            unitOW,
            //                            true);
            //        await unitOW.CompleteAsync();
            //    }

            //    //Seed-Data SystemUsageData
            //    ////await keyValueData.Insert(unitOW, adminUser);
            //    //await SubscriptionData.ShopProductsAsync(unitOW);

            //    //Seed-Data CreatingSystemClients
            //    var clientSilkFlo = await SilkFloClientAsync(unitOW, powerUser);
            //    clientSilkFlo.AccountOwner = powerUser;
            //    powerUser.Client = clientSilkFlo;


            //    await TestData.InsertClients(unitOW, clientSilkFlo, powerUser);
            //    await unitOW.CompleteAsync();
            //    #region Add complimentary companies
            //    // Add complimentary companies
            //    //await Models.Business.Client.CreateAsync(
            //    //    unitOW,
            //    //    "d9ee8262-3e94-4d61-b540-855d4cb9d621",
            //    //    "PSI CRO AG",
            //    //    "Doug",
            //    //    "Shannon",
            //    //    "Doug.Shannon@psi-cro.com",
            //    //    "WaterBottle",
            //    //    "Baarerstrasse 113a",
            //    //    "Zug",
            //    //    "",
            //    //    "",
            //    //    "6300",
            //    //    30,
            //    //    new Models.Business.Client(clientSilkFlo),
            //    //    true);
            //    //await Models.Business.Client.CreateAsync(
            //    //    unitOfWork,
            //    //    "f7eda9d6-42c7-4a25-85b4-79ee40bfd26f",
            //    //    "FD Intelligence Ltd.",
            //    //    "Michael",
            //    //    "Perrin",
            //    //    "m.perrin@FDIntelligence.co.uk",
            //    //    "CoffeeCup",
            //    //    "56 Palmerston Place",
            //    //    "Edinburgh",
            //    //    "",
            //    //    "",
            //    //    "EH12 5AY",
            //    //    30,
            //    //    new Models.Business.Client(clientSilkFlo),
            //    //    true);


            //    //var client = await Models.Business.Client.CreateAsync(
            //    //    unitOfWork,
            //    //    "14aececd-174c-4c5a-99a5-9a6042ad5060",
            //    //    "Amsted Rail Headquarters",
            //    //    "Ray",
            //    //    "Ludwig",
            //    //    "rludwig@amstedrail.com",
            //    //    "CoffeeCup",
            //    //    "311 S. Wacker Drive",
            //    //    "Suite 5300",
            //    //    "Chicago",
            //    //    "Illinois",
            //    //    "60606",
            //    //    30,
            //    //    new Models.Business.Client(clientSilkFlo),
            //    //    true);
            //    #endregion
            //}

            return;

//            var dataSet = await unitOfWork.GetDataSetAsync(); // Data.Persistence.UnitOfWork.GetDataSetAsync();
//            if (dataSet == null)
//                return;

//            //using var unitOfWork = new Data.Persistence.UnitOfWork();
//            try
//            {
//                var setting = new Models.Application.Setting(unitOfWork);
//                await setting.CreateDefaultTrialPeriod();

//                await unitOfWork.InsertRolesAsync(unitOfWork);// Data.Persistence.UnitOfWork.InsertRolesAsync(unitOfWork);
//                KeyValueData keyValueData = new KeyValueData();
//                await keyValueData.InsertSystemRolesAsync(unitOfWork);

//                await unitOfWork.CompleteAsync();

//                var password = Security.Settings.Password;

//                var email = "admin@scriptbot.io";
//                var adminUser = await unitOfWork.Users.GetByEmailAsync(email)
//                                ?? await Services.Authorization
//                                    .User
//                                    .CreateAsync("Douglas",
//                                        "Adams",
//                                        email,
//                                        password,
//                                        Data.Core.Enumerators.Role.Administrator,
//                                        unitOfWork,
//                                        true);


//                email = "jimmy@silkflo.com";
//                var powerUser = await unitOfWork.Users.GetByEmailAsync(email)
//                                ?? await Services
//                                    .Authorization
//                                    .User
//                                    .CreateAsync("Jimmy",
//                                        "McGill",
//                                        email,
//                                        password,
//                                        Data.Core.Enumerators.Role.Administrator,
//                                        unitOfWork,
//                                        true);

//                await unitOfWork.CompleteAsync();

//                await keyValueData.Insert(unitOfWork, adminUser);
//                //await SubscriptionData.ShopProductsAsync(unitOfWork);

//                var clientSilkFlo = await SilkFloClientAsync(
//                    unitOfWork,
//                    powerUser);

//                clientSilkFlo.AccountOwner = powerUser;
//                powerUser.Client = clientSilkFlo;


//                await TestData.InsertClients(
//                    unitOfWork,
//                    clientSilkFlo,
//                    powerUser);

//                // Add complimentary companies
//                await Models.Business.Client.CreateAsync(
//                    unitOfWork,
//                    "d9ee8262-3e94-4d61-b540-855d4cb9d621",
//                    "PSI CRO AG",
//                    "Doug",
//                    "Shannon",
//                    "Doug.Shannon@psi-cro.com",
//                    "WaterBottle",
//                    "Baarerstrasse 113a",
//                    "Zug",
//                    "",
//                    "",
//                    "6300",
//                    30,
//                    new Models.Business.Client(clientSilkFlo),
//                    true);

//                //#if DEBUG
//                //                var core = await unitOfWork.Users.GetByEmailAsync("Doug.Shannon@psi-cro.com");
//                //                if (core != null)
//                //                {
//                //                    core.PasswordHash = unitOfWork.GeneratePasswordHash("WaterBottle");
//                //                }
//                //#endif

//                await Models.Business.Client.CreateAsync(
//                    unitOfWork,
//                    "f7eda9d6-42c7-4a25-85b4-79ee40bfd26f",
//                    "FD Intelligence Ltd.",
//                    "Michael",
//                    "Perrin",
//                    "m.perrin@FDIntelligence.co.uk",
//                    "CoffeeCup",
//                    "56 Palmerston Place",
//                    "Edinburgh",
//                    "",
//                    "",
//                    "EH12 5AY",
//                    30,
//                    new Models.Business.Client(clientSilkFlo),
//                    true);


//                var client = await Models.Business.Client.CreateAsync(
//                    unitOfWork,
//                    "14aececd-174c-4c5a-99a5-9a6042ad5060",
//                    "Amsted Rail Headquarters",
//                    "Ray",
//                    "Ludwig",
//                    "rludwig@amstedrail.com",
//                    "CoffeeCup",
//                    "311 S. Wacker Drive",
//                    "Suite 5300",
//                    "Chicago",
//                    "Illinois",
//                    "60606",
//                    30,
//                    new Models.Business.Client(clientSilkFlo),
//                    true);

//                if (client != null)
//                {
//                    email = "aseale@amstedrail.com";
//                    var user = await unitOfWork.Users.GetByEmailAsync(email)
//                               ?? await Services
//                                   .Authorization
//                                   .User
//                                   .CreateAsync("Adam",
//                                       "Seale",
//                                       email,
//                                       "CoffeeCup",
//                                       Data.Core.Enumerators.Role.StandardUser,
//                                       unitOfWork,
//                                       true,
//                                       client.GetCore());
//                }


//                await DatabaseChanges.Fixes(unitOfWork);

//                await unitOfWork.CompleteAsync();

//#if DEBUG
//                var subscription = await unitOfWork.ShopSubscriptions.GetAsync("666a57aa-40e4-4f86-a5ad-7705e7f70f5a");
//                if (subscription != null)
//                    subscription.PriceId = "price_1KZyu0AzFT9dgqImERw519gW";
//#endif
//            }
//            catch (Exception ex)
//            {
//                Console.ForegroundColor = ConsoleColor.Red;
//                Console.WriteLine(ex.Message);
//                Console.ResetColor();
//                unitOfWork.Log(ex);
//            }
        }


        private static async Task SetupEmail(IApplicationBuilder app)
        {
            var domain = GetDomain(app);

            var isProduction = true;
            var testEmail = "";
            //if (Security.Settings.GetEnvironment() != Security.Environment.Production)
            //{
            //    isProduction = false;

            //    using var unitOfWork = new Data.Persistence.UnitOfWork();

            //    var id = Data.Core.Enumerators.Setting.TestEmailAccount.ToString();
            //    var setting = await unitOfWork.ApplicationSettings.GetAsync(id);

            //    testEmail = setting == null ? "" : setting.Value;
            //}

            Email.Service.Setup(
                true, //isProduction, //remove it and send true directly
                testEmail,
                domain);
        }


        private static string GetDomain(IApplicationBuilder app)
        {
            return "https://app.silkflo.com";

            //var str = "";

            //var urls = app.ServerFeatures.Get<IServerAddressesFeature>()?.Addresses;
            //if (urls == null)
            //    return str;

            //foreach (var url in urls)
            //{
            //    if (url.IndexOf("https://", StringComparison.CurrentCultureIgnoreCase) > -1)
            //        return url;

            //    str = url;
            //}

            //return str = "https://app.silkflo.com";
        }
    }
}