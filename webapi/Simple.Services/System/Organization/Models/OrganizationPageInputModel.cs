﻿namespace Simple.Services;

public class OrganizationPageInputModel : PageInputModel
{
    public string? Name { get; set; }
    public Guid? ParentId { get; set; }
}