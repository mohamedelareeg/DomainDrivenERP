using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Enums;

namespace DomainDrivenERP.Domain.Dtos;
public class CoaWithChildrenDto
{
    public string HeadCode { get; set; }
    public string HeadName { get; set; }
    public string ParentHeadCode { get; set; }
    public int HeadLevel { get; set; }
    public bool IsActive { get; set; }
    public bool IsGl { get; set; }
    public COA_Type Type { get; set; }
    public List<CoaWithChildrenDto> CoAs { get; set; }
}
