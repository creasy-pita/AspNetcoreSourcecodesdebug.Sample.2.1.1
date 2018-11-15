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


##FileProvider的 detect file change 的源码查看
###1    配置 时 设置reloadonchange=true
         builder.ConfigureAppConfiguration( (context,configurationBinder)=>
                {
                    configurationBinder.AddJsonFile("appsettings.json", false, true);
                }
                
                );
###2 trace AddJsonFile 方法 发现 FileConfigurationProvider的 构造方法 public FileConfigurationProvider(FileConfigurationSource source) 中使用到了  IFileProvider 接口 的Watch方法，即实时监听文件是否修改

###3 进一步trace 需要知道 IFileProvider 属于哪个项目， 并下载其源码
在github  中search 框输入： "public interface IFileProvider" in:file org:aspnet

###4 下载对应项目，并引入相关的项目 FS.Physical、FS.Abstractions 到解决方案中
（注意 因为FS.Physical 还引用了 项目FS.Globbing，所以同时把这个项目引入
Config.FileExtensions  引用了FS.Abstractions，需要在Config.FileExtensions引入 FS.Abstractions ）

###5 进行代码调试
