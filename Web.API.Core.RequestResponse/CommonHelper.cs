using System;
using System.Collections.Generic;
using System.Text;

namespace Web.API.Core.RequestResponse
{
    public static class CommonHelper
    {
        public enum OperationType
        {
            None=0,
            SelectById=1,
            SelectAll=2,
            AddOne=3,
            AddMany=4,
            UpdateOne=5,
            UpdateAll=6,
            SelectSingle=7,
            DeleteAll=8,
            DeleteOne=9,
            GetAll=10,
            GetById=11,             

        }
    }
}
