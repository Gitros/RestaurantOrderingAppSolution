using Application.Contracts;
using Application.Dtos.CustomerInformations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CustomerInformationController(ICustomerInformationService customerInformationService) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateCustomerInformation([FromBody] CustomerInformationCreateDto customerInformationCreateDto, Guid orderId) =>
        HandleResult(await customerInformationService.CreateCustomerInformation(customerInformationCreateDto, orderId));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerInformation(Guid id) =>
        HandleResult(await customerInformationService.GetCustomerInformation(id));

    [HttpGet]
    public async Task<IActionResult> GetAllCustomerInformations() =>
        HandleResult(await customerInformationService.GetAllCustomerInformations());

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomerInformation(Guid id, [FromBody] CustomerInformationUpdateDto customerInformationUpdateDto) =>
        HandleResult(await customerInformationService.UpdateCustomerInformation(customerInformationUpdateDto, id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerInformation(Guid id) =>
        HandleResult(await customerInformationService.DeleteCustomerInformation(id));
}
