﻿using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Illnesses.Responses;
using Hospital.Domain.Models;
using MediatR;

namespace Hospital.Application.Illnesses.Queries
{
    public record GetIllnessesByProperty(Func<Illness, bool> IllnessProperty)
        : IRequest<List<IllnessDto>>;

    public class GetIllnessesByPropertyHandler : IRequestHandler<GetIllnessesByProperty,
        List<IllnessDto>>
    {
        private readonly IIllnessRepository _illnessRepository;

        public GetIllnessesByPropertyHandler(IIllnessRepository illnessRepository)
        {
            _illnessRepository = illnessRepository;
        }

        public Task<List<IllnessDto>> Handle(GetIllnessesByProperty request, CancellationToken cancellationToken)
        {
            var illnesses = _illnessRepository.SearchByProperty(request.IllnessProperty);

            if (illnesses is null)
            {
                throw new NoEntityFoundException("No illnesses with such properties exist");
            }

            return Task.FromResult(illnesses.Select(IllnessDto.FromIllness).ToList());
        }
    }
}