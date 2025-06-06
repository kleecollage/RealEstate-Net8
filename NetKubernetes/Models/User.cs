using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NetKubernetes.Models;

public class User: IdentityUser
{
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public string? Phone { get; set; }
}