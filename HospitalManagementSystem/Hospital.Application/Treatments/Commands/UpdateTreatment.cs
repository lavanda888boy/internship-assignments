using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Treatments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Treatments.Commands
{
    public record UpdateTreatment(int Id, string PrescribedMedicine, TimeSpan TreatmentDuration) 
        : IRequest<TreatmentDto>;

    public class UpdateTreatmentHandler : IRequestHandler<UpdateTreatment, TreatmentDto>
    {
        private readonly IRepository<Treatment> _treatmentRepository;

        public UpdateTreatmentHandler(IRepository<Treatment> treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public Task<TreatmentDto> Handle(UpdateTreatment request, CancellationToken cancellationToken)
        {
            var result = _treatmentRepository.Update(new Treatment()
            {
                Id = request.Id,
                PrescribedMedicine = request.PrescribedMedicine,
                TreatmentDuration = request.TreatmentDuration,
            });

            if (result)
            {
                var treatment = _treatmentRepository.GetById(request.Id);
                return Task.FromResult(TreatmentDto.FromTreatment(treatment));
            }
            else
            {
                throw new NoEntityFoundException($"Cannot update non-existing treatment with id {request.Id}");
            }
        }
    }
}
