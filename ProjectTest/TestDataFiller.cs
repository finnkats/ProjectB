﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest
{
    public static class TestDataFiller
    {
        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            // Change location of Source files
            DataAccess.FilePrefix = "Sources/";
        }


        public static void FillApp()
        {
            
        }
    }
}
