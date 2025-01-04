﻿using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.CustomerInformations;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class CustomerInformationService(RestaurantOrderingContext orderingContext, IMapper mapper) : ICustomerInformationService
{
    public async Task<ResultDto<CustomerInformationReadDto>> CreateCustomerInformation(CustomerInformationCreateDto customerInformationCreateDto, Guid orderId)
    {
        try
        {
            var customerInformation = mapper.Map<CustomerInformation>(customerInformationCreateDto);
            customerInformation.OrderId = orderId;

            await orderingContext.CustomerInformation.AddAsync(customerInformation);
            await orderingContext.SaveChangesAsync();

            var customerInformationDto = mapper.Map<CustomerInformationReadDto>(customerInformation);

            return ResultDto<CustomerInformationReadDto>
                .Success(customerInformationDto, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return ResultDto<CustomerInformationReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<CustomerInformationReadDto>> GetCustomerInformation(Guid id)
    {
        try
        {
            var customerInformation = await orderingContext.CustomerInformation
                .FirstOrDefaultAsync(ci => ci.Id == id);

            if (customerInformation == null)
            {
                return ResultDto<CustomerInformationReadDto>
                    .Failure("Customer information not found.", HttpStatusCode.NotFound);
            }

            var customerInformationDto = mapper.Map<CustomerInformationReadDto>(customerInformation);

            return ResultDto<CustomerInformationReadDto>
                .Success(customerInformationDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<CustomerInformationReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<List<CustomerInformationReadDto>>> GetAllCustomerInformations()
    {
        try
        {
            var customerInformations = await orderingContext.CustomerInformation.ToListAsync();

            var customerInformationsDto = mapper.Map<List<CustomerInformationReadDto>>(customerInformations);

            return ResultDto<List<CustomerInformationReadDto>>
                .Success(customerInformationsDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<CustomerInformationReadDto>>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<CustomerInformationReadDto>> UpdateCustomerInformation(CustomerInformationUpdateDto customerInformationUpdateDto, Guid id)
    {
        try
        {
            var customerInformation = await orderingContext.CustomerInformation
                .FirstOrDefaultAsync(ci => ci.Id == id);

            if (customerInformation == null)
            {
                return ResultDto<CustomerInformationReadDto>
                    .Failure("Customer information not found.", HttpStatusCode.NotFound);
            }

            mapper.Map(customerInformationUpdateDto, customerInformation);
            await orderingContext.SaveChangesAsync();

            var updatedCustomerInformationDto = mapper.Map<CustomerInformationReadDto>(customerInformation);

            return ResultDto<CustomerInformationReadDto>
                .Success(updatedCustomerInformationDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<CustomerInformationReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<bool>> DeleteCustomerInformation(Guid id)
    {
        try
        {
            var customerInformation = await orderingContext.CustomerInformation
                .FirstOrDefaultAsync(ci => ci.Id == id);

            if (customerInformation == null)
            {
                return ResultDto<bool>
                    .Failure("Customer information not found.", HttpStatusCode.NotFound);
            }

            orderingContext.CustomerInformation.Remove(customerInformation);
            await orderingContext.SaveChangesAsync();

            return ResultDto<bool>
                .Success(true, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<bool>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}
