﻿using HospitalManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.DataAccess.Persistence.Configrautions
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecords>
    {
        public void Configure(EntityTypeBuilder<MedicalRecords> builder)
        {

        }
    }
}
