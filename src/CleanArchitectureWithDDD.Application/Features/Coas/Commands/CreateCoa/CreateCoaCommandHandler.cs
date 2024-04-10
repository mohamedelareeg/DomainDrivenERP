using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.COAs;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Commands.CreateCoa;
internal class CreateCoaCommandHandler : ICommandHandler<CreateCoaCommand, COA>
{
    private readonly ICoaRepository _coaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCoaCommandHandler(ICoaRepository coaRepository, IUnitOfWork unitOfWork)
    {
        _coaRepository = coaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<COA>> Handle(CreateCoaCommand request, CancellationToken cancellationToken)
    {
        bool isExist = await _coaRepository.IsCoaExist(request.CoaName, request.CoaParentName);
        if (isExist)
        {
            return Result.Failure<COA>(new Error("COA.Create", $"COA with name '{request.CoaName}' and parent name '{request.CoaParentName}' already exists."));
        }

        COA? parentCoa = await _coaRepository.GetCoaByName(request.CoaParentName, cancellationToken);
        if (parentCoa is null)
        {
            return Result.Failure<COA>(new Error("COA.Create", $"Parent COA with name '{request.CoaParentName}' does not exist."));
        }

        Result<COA> coaResult = COA.Create(request.CoaName, parentCoa);
        if (coaResult.IsFailure)
        {
            return Result.Failure<COA>(coaResult.Error);
        }

        await _coaRepository.CreateCoa(coaResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return coaResult;
    }
}
