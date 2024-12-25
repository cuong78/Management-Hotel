using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Service
{
    public string NameService { get; set; } = null!;

    public decimal Price { get; set; }

    public int Status { get; set; }

    public virtual ICollection<BookedService> BookedServices { get; set; } = new List<BookedService>();
}
