using System;

namespace WindowsService.Triggers
{
    public class VariableRowDto
    {

        public string Name { get; set; }
        public Guid id { get; set; }
        public string EntityTypeName { get; set; }
        public Guid EntityTypeId { get; set; }
    }
}