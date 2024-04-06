using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Treatments.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Treatments.Queries
{
    public record GetTreatmentById(int TreatmentId) : IRequest<TreatmentDto>;

    public class GetTreatmentByIdHandler : IRequestHandler<GetTreatmentById, TreatmentDto>
    {
        private readonly IRepository<Treatment> _treatmentRepository;

        public GetTreatmentByIdHandler(IRepository<Treatment> treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public Task<TreatmentDto> Handle(GetTreatmentById request, CancellationToken cancellationToken)
        {
            var treatment = _treatmentRepository.GetById(request.TreatmentId);

            if (treatment is null)
            {
                throw new NoEntityFoundException($"There is no treatment with id {request.TreatmentId}");
            }

            return Task.FromResult(TreatmentDto.FromTreatment(treatment));
        }
    }
}
