using ISOtrainSCIMIntegration.ApiWeb.Model;

namespace ISOtrainSCIMIntegration.ApiWeb.Services
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<Employee> CreateAsync(ISOtrainEmployeeRequest employee)
        {
            return new Employee() { EmpId = GenerateEmpId(), Email = employee.Email, Name = employee.FirstName + ", " + employee.LastName };
        }

        public async Task<Employee> GetByIdAsync(string id)
        {
            return await Task.FromResult(new Employee() { Email = "a@mail.com", EmpId = id, Name = "name of " + id });
        }

        public async Task SetStatusAsync(string id, bool status)
        {
            var employee = await GetByIdAsync(id);
            employee.EmpStatus = status ? 'A' : 'I';
        }

        public async Task UpdateLastNameAsync(string id, string newLastName)
        {
            var employee = await GetByIdAsync(id);

            employee.Name = newLastName;
        }

        private string GenerateEmpId()
        {
            // Genera un ID alfanumérico de 20 caracteres
            string id = Path.GetRandomFileName();//.Replace(".", "").Substring(0, 20);
            // Si la cadena es más corta de 20, se puede concatenar
            while (id.Length < 20)
            {
                id += Path.GetRandomFileName().Replace(".", "");
            }
            id = id.Substring(0, 20);
            return id;
        }
    }
}
