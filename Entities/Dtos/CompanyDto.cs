﻿namespace Entities.Dtos
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string FullAddress { get; set; } = null!;
    }
}
