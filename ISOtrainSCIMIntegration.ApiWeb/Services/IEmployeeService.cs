using ISOtrainSCIMIntegration.ApiWeb.Model;

namespace ISOtrainSCIMIntegration.ApiWeb.Services
{
    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(ISOtrainEmployeeRequest employee);

        Task SetStatusAsync(string id, bool status);

        Task UpdateLastNameAsync(string id, string newLastName);

        Task <Employee> GetByIdAsync(string id);
    }
}
