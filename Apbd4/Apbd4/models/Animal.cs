﻿namespace Apbd4.models;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Weight { get; set; }
    public String Color { get; set; }
    public ICollection<Visit> Visits { get; set; }
    
}