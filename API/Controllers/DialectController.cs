using API.Helpers;
using Application.Interfaces;
using Domain.Models.API.Results;
using Domain.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class DialectController(IDialect dialect) : MyController<DialectController>
{
    [HttpGet]
    public async Task<Result<IReadOnlyCollection<DialectResult>>> GetAllDialects()
        => await dialect.GetDialects(HttpContext.RequestAborted);
}