﻿using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.Reservations;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class ReservationService(RestaurantOrderingContext orderingContext, IMapper mapper) : IReservationService
{
    public async Task<ResultDto<ReservationReadDto>> CreateReservation(ReservationCreateDto reservationCreate)
    {
        try
        {
            var reservation = mapper.Map<Reservation>(reservationCreate);

            await orderingContext.Reservations.AddAsync(reservation);
            await orderingContext.SaveChangesAsync();

            var createdReservation = mapper.Map<ReservationReadDto>(reservation);

            return ResultDto<ReservationReadDto>
                .Success(createdReservation, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return ResultDto<ReservationReadDto>
                .Failure($"An error occured: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<ReservationReadDto>> GetReservation(Guid id)
    {
        try
        {
            var reservation = await orderingContext.Reservations
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return ResultDto<ReservationReadDto>
                    .Failure("Reservation not found.", HttpStatusCode.NotFound);

            var reservationDto = mapper.Map<ReservationReadDto>(reservation);

            return ResultDto<ReservationReadDto>
                .Success(reservationDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<ReservationReadDto>
                .Failure($"An error occured: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<List<ReservationReadDto>>> GetAllReservations()
    {
        try
        {
            var reservations = await orderingContext.Reservations
                .Include(r => r.Table)
                .ToListAsync();

            var reservationDtos = mapper.Map<List<ReservationReadDto>>(reservations);

            return ResultDto<List<ReservationReadDto>>
                .Success(reservationDtos, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<ReservationReadDto>>
                .Failure($"An error occured: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<ReservationReadDto>> AssignTableToReservation(Guid reservationId, Guid tableId)
    {
        try
        {
            var reservation = await orderingContext.Reservations
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
                return ResultDto<ReservationReadDto>
                    .Failure("Reservation not found.", HttpStatusCode.NotFound);

            var table = await orderingContext.Tables
                .FirstOrDefaultAsync(t => t.Id == tableId);

            if (table == null)
                return ResultDto<ReservationReadDto>
                    .Failure("Table not found.", HttpStatusCode.NotFound);

            if (table.IsOccupied)
                return ResultDto<ReservationReadDto>
                    .Failure("Table is already occupied.", HttpStatusCode.Conflict);

            reservation.IsAssigned = true;
            reservation.TableId = tableId;

            await orderingContext.SaveChangesAsync();

            var updatedReservationDto = mapper.Map<ReservationReadDto>(reservation);

            return ResultDto<ReservationReadDto>
                .Success(updatedReservationDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<ReservationReadDto>
                .Failure($"An error occured: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<ReservationReadDto>> UpdateReservation(ReservationUpdateDto reservationUpdate, Guid id)
    {
        try
        {
            var reservation = await orderingContext.Reservations.FindAsync(id);

            if (reservation == null)
                return ResultDto<ReservationReadDto>
                    .Failure("Reservation not found.", HttpStatusCode.NotFound);

            mapper.Map(reservationUpdate, reservation);

            await orderingContext.SaveChangesAsync();

            var updatedReservationDto = mapper.Map<ReservationReadDto>(reservation);

            return ResultDto<ReservationReadDto>
                .Success(updatedReservationDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<ReservationReadDto>
                .Failure($"An error occured: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<bool>> DeleteReservation(Guid id)
    {
        try
        {
            var reservation = await orderingContext.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return ResultDto<bool>
                    .Failure("Reservation not found.", HttpStatusCode.NotFound);

            orderingContext.Reservations.Remove(reservation);
            await orderingContext.SaveChangesAsync();

            return ResultDto<bool>
                .Success(true, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            return ResultDto<bool>
                .Failure($"An error occured: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}
