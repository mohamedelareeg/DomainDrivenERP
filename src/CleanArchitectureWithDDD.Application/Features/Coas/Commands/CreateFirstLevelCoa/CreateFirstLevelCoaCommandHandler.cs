using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.COAs;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Commands.CreateFirstLevelCoa;
internal class CreateFirstLevelCoaCommandHandler : ICommandHandler<CreateFirstLevelCoaCommand, COA>
{
    private readonly ICoaRepository _coaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFirstLevelCoaCommandHandler(ICoaRepository coaRepository, IUnitOfWork unitOfWork)
    {
        _coaRepository = coaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<COA>> Handle(CreateFirstLevelCoaCommand request, CancellationToken cancellationToken)
    {
        bool isExist = await _coaRepository.IsCoaExist(request.HeadName, 1);
        if (isExist)
        {
            Result.Failure<COA>(new Error("COA.CreateFirstLevelCoa", $"Coa Name '{request.HeadName}' already Exist"));
        }
        string lastHeadCodeInLevelOne = await _coaRepository.GetLastHeadCodeInLevelOne() ?? "0";
        int nextHeadCode = int.Parse(lastHeadCodeInLevelOne) + 1;
        string nextHeadCodeString = nextHeadCode.ToString();
        Result<COA> coaResult = COA.Create(request.HeadName, nextHeadCodeString, request.Type);
        if (coaResult.IsFailure)
        {
            return Result.Failure<COA>(coaResult.Error);
        }
        await _coaRepository.CreateCoa(coaResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return coaResult;
    }
}
