using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebUI.Extensions
{
    /// <summary>
    /// Class for services module that implement DependencyInjection.
    /// </summary>
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        public static void ConfigureIssueService(this IServiceCollection services) =>
            services.AddScoped<IIssueService, IssueService>();
        public static void ConfigureProjectService(this IServiceCollection services) =>
            services.AddScoped<IProjectService, ProjectService>();
        public static void ConfigureAttachmentService(this IServiceCollection services) =>
            services.AddScoped<IAttachmentService, AttachmentService>();
        public static void ConfigureReportService(this IServiceCollection services) =>
            services.AddScoped<IReportService, ReportService>();
        public static void ConfigureEmployeeService(this IServiceCollection services) =>
            services.AddScoped<IEmployeeService, EmployeeService>();
    }
}
