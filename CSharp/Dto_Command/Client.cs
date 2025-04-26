using System;
using System.Collections.Generic;

namespace DTO_Command;

public partial class Client
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? BearthDate { get; set; } = new DateTime() ;

}
