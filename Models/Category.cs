﻿namespace Farkas_Zoltán_backend.Models;

public partial class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
