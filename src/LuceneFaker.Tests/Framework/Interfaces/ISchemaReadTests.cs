﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneFaker.Tests.Framework.Interfaces
{
    public interface ISchemaReadTests
    {
        void CanReadIndexSchema();
        void CanGetRandomDocumentFromIndex();
    }
}
