using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MammalAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MammalAPI.Context
{
    class FakeConfiguration : IEntityTypeConfiguration<FakeMammal>
    {
        //add tablespecific configuration code here
        public void Configure(EntityTypeBuilder<FakeMammal> builder)
        {
            builder.HasData //add initial seed data
            (
                new FakeMammal
                {
                    FakeMammalId = 1,
                    Name = "Raninbow Whale"
                }
            );
        }
    }
}
