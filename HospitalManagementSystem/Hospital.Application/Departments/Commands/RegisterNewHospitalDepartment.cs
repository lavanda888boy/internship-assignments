﻿using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Departments.Commands
{
    public record RegisterNewHospitalDepartment(int Id, string Name) : IRequest<DepartmentDto>;

    public class RegisterNewHospitalDepartmentHandler : IRequestHandler<RegisterNewHospitalDepartment, DepartmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterNewHospitalDepartmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDto> Handle(RegisterNewHospitalDepartment request, CancellationToken cancellationToken)
        {
            try
            {
                var department = new Department()
                {
                    Id = request.Id,
                    Name = request.Name,
                };

                await _unitOfWork.BeginTransactionAsync();
                var newDepartment = await _unitOfWork.DepartmentRepository.AddAsync(department);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return await Task.FromResult(DepartmentDto.FromDepartment(newDepartment));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}