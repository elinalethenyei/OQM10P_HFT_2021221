﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Validation.Interfaces
{
    public interface IModelValidator
    {
        bool Validate(object instance);
    }
}
