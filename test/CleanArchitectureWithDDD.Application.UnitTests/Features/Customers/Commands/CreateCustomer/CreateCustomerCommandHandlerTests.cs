using CleanArchitectureWithDDD.Application.Features.Customers.Commands.CreateCustomer;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Errors;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace CleanArchitectureWithDDD.Application.UnitTests.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRespository> _customerRepositoryMock;// Moq
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;// Moq

    public CreateCustomerCommandHandlerTests()
    {
        _customerRepositoryMock = new();
        _unitOfWorkMock = new();
    }
    #region Access Internal Application Member Assembly Hint
    // To access internal class members for testing, grant permission to the UnitTest project 
    // to access internal members inside the Application assembly.
    // This can be achieved by adding the following lines to the Application project file:

    /*
    <ItemGroup>
        <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
            <_Parameter1>CleanArchitectureWithDDD.Application.UnitTests</_Parameter1>
        </AssemblyAttribute>
    </ItemGroup>
    */
    #endregion
    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenEmailIsNotUniqueAsync()
    {
        // Scenario: Handling creation of a customer should return a failure result when email is not unique.

        // Arrange
        var command = new CreateCustomerCommand("first", "last", "01000000000", "test@test.com");
        _customerRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        // Act
        var handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);
        Result<Customer> result = await handler.Handle(command, default);
        // Assert
        result.IsFailure.Should().BeTrue();// FluentAssertions
        result.Error.Should().Be(DomainErrors.CustomerErrors.IsCustomerEmailAlreadyExist);

    }
    [Fact]
    public async Task Handle_should_ReturnSuccessResult_WhenEmailIsUnique()
    {
        // Scenario: Handling creation of a customer should return a success result when email is unique.

        // Arrange
        var command = new CreateCustomerCommand("first", "last", "01000000000", "test@test.com");
        _customerRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        // Act
        var handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);
        Result<Customer> result = await handler.Handle(command, default);
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

    }
    [Fact]
    public async Task Handle_Should_Call_AddAsync_OnRespository_WhenEmailIsUnique()
    {
        // Scenario: Handling creation of a customer should call AddAsync when email is unique.

        // Arrange
        var command = new CreateCustomerCommand("first", "last", "01000000000", "test@test.com");
        _customerRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        // Act
        var handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);
        Result<Customer> result = await handler.Handle(command, default);
        // Assert
        _customerRepositoryMock.Verify(x => x.AddAsync(It.Is<Customer>(c => c.Id == result.Value.Id)), Times.Once);
    }
    [Fact]
    public async Task Handle_Should_Not_Call_UnitOFWORk_WhenEmailIsNotUnqiue()
    {
        // Scenario: Handling creation of a customer should not call UnitOFWork when email is not unique.

        // Arrange
        var command = new CreateCustomerCommand("first", "last", "01000000000", "test@test.com");
        _customerRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        // Act
        var handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);
        await handler.Handle(command, default);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
