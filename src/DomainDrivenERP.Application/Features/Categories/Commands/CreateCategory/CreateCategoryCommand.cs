using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Categories;

namespace DomainDrivenERP.Application.Features.Categories.Commands.CreateCategory;
public record CreateCategoryCommand(string Name) : ICommand<Category>;

