﻿using Microsoft.AspNetCore.Identity;

namespace AmoebaPractaise.Models;

public class ApplicationUser:IdentityUser
{
    public string Name { get; set; }
    public  string Surname { get; set; }
}
