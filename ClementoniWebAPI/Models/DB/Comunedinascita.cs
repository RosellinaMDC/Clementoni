using System;
using System.Collections.Generic;

namespace ClementoniWebAPI.Models.DB;

public partial class Comunedinascita
{
    public int Id { get; set; }

    public string NomeComuneDiNascita { get; set; } = null!;

    public virtual ICollection<Person> Person { get; set; } = new List<Person>();
}
