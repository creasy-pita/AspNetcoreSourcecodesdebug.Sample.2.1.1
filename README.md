# AspNetcoreSourcecodesdebug.Sample.2.1.1



aspnetcore simple webapi  aspnetcore sourcecode  debug  with aspnetcore2.1.1 api
	startup 类中 Configure 可以实现 多个输入参数的注入 IServiceProvider、ILoggerFactory、ILogger、IApplicationLifetime
            var configureMethod = FindConfigureDelegate(startupType, environmentName);

            var servicesMethod = FindConfigureServicesDelegate(startupType, environmentName);
            var configureContainerMethod = FindConfigureContainerDelegate(startupType, environmentName);

            object instance = null;
            if (!configureMethod.MethodInfo.IsStatic || (servicesMethod != null && !servicesMethod.MethodInfo.IsStatic))
            {
                instance = ActivatorUtilities.GetServiceOrCreateInstance(hostingServiceProvider, startupType);
            }
	通过 startupmethod  的 action<applicationBuilder> configureDelegate 调用 ，在进入 ConfigureBuilder Invoke 方法 传入其他startup.configure 的参数 完成调用


#源代码调试   aspnetcore 代码调试

##启动流程中 加载configuration 的过程
###Microsoft.AspNetCore.dll WebHost 下CreateDefaultBuilder方法中的如下代码
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
...

###configuration会加入到依赖注入容器中（见如下代码），之后可以提供整个application获取使用，比如提取当中某段配置
            var configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);
##startup类
###IServiceProvider、ILoggerFactory、ILogger、IApplicationLifetime等可用服务。这些预置服务可以注入到Startup类的构造函数或Configure方法中

##Startup 类的调试
webhostbuidler Method 中显示内容
