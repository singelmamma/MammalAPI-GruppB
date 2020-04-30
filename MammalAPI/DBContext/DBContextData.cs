using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MammalAPI.Models;
using System.Threading.Tasks;

namespace MammalAPI.Context
{
    public class DBContextData
    {
        DBContext _dbContext = new DBContext();

        public FakeMammal Get()
        {            
            var fakeData = _dbContext.FakeMammal
            .Where(f => f.FakeMammalId == 1)
            .Select(f => f).FirstOrDefault();
            
            return fakeData;
        }
    }
}
