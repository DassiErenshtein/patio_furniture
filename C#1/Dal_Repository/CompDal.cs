﻿using Dal_Repository.models;
using DTO_Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Repository
{
    public class CompDal : IDal_Repository.IDal<DTO_Command.Company>
    {
        PatioFurnitureContext db;
        public CompDal(PatioFurnitureContext dB)
        {
            db = dB;
        }
        public async Task<List<DTO_Command.Company>> selectAllAsync()
        {
            var list = await db.Companies.ToListAsync();
            return converters.CompanyConverter.toListDtoComp(list);
        }
    }
}
