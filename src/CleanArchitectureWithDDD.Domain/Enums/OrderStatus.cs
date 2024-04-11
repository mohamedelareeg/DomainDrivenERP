﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Enums;
public enum OrderStatus
{
    Created,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}
