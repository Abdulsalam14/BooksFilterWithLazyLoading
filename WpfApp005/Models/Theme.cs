﻿using System;
using System.Collections.Generic;

namespace WpfApp005.Models;

public partial class Theme
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public override string ToString()
    {
        return Name;
    }
}
