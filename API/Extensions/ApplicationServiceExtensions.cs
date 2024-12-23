﻿using Application.Contracts;
using Application.Services;
using Application.Validators.Orders;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register application services
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IMenuCategoryService, MenuCategoryService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IIngredientService, IngredientService>();
        services.AddScoped<ITagService, TagService>();

        // FluentValidation
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<OrderCreateDtoValidator>();

        // Automapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
