using ISOtrainSCIMIntegration.ApiWeb.Model;
using ISOtrainSCIMIntegration.ApiWeb.Model.SCIMModel;
using ISOtrainSCIMIntegration.ApiWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISOtrainSCIMIntegration.ApiWeb.Controllers
{
    [ApiController]
    [Route("scim/v2/[controller]")]
    //[Authorize] // Protegido por tu JWT
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public UsersController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Buscar si existe el usuario
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUsers([FromQuery(Name = "filter")] string? filter = null, [FromQuery(Name = "startIndex")] int startIndex = 1, [FromQuery(Name = "count")] int count = 10)
        {
            // Devolvemos una estructura SCIM vacía pero válida
            var response = new
            {
                schemas = new[] { "urn:ietf:params:scim:api:messages:2.0:ListResponse" },
                totalResults = 0, // Indica que el usuario NO existe, por lo tanto debe ejecutar el POST
                startIndex = startIndex,
                itemsPerPage = 0,
                Resources = new List<object>() // Lista vacía
            };

            return Ok(response); // Retorna 200 OK
        }

        /// <summary>
        /// No existe, crearlo.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // POST: /scim/v2/Users
        [HttpPost]
        // 1. Aceptamos explícitamente el formato de SCIM
        [Consumes("application/scim+json", "application/json", "text/plain")] // Acepta esos content types
        public async Task<IActionResult> CreateUser([FromBody] ScimUserRequest request)
        {
            try
            {
                // 1. Mapear de SCIM a tu entidad corporativa
                var newEmployee = new ISOtrainEmployeeRequest
                {
                    ExternalId = request.ExternalId,
                    Email = request.UserName,
                    FirstName = request.Name.GivenName,
                    LastName = request.Name.FamilyName,
                    IsActive = true
                };

                // 2. Llamar a tu CRUD existente
                var result = await _employeeService.CreateAsync(newEmployee);

                // 3. Responder con el formato SCIM (Importante: Status 201)
                return CreatedAtAction(nameof(GetUser), new { id = result.EmpId }, MapToScim(result));



            }
            catch (Exception ex)
            {
                // Si falla el GetProperty o cualquier cosa, a la tabla de logs
                // _context.EventLogs.Add(new EventLog { Message = "Error en POST", Detail = ex.Message });
                return BadRequest(new { detail = ex.Message });
            }
        }

        // PATCH: /scim/v2/Users/{id}
        // Muy usado por Azure/Okta para desactivar usuarios
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(string id, [FromBody] ScimPatchRequest request)
        {
            // 1. El 'id' que viene aquí es tu ID interno (ej: "550e8400-e29b...")
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound(); // SCIM espera un 404 si el ID no existe en tu sistema
            }

            foreach (var operation in request.Operations)
            {
                // Caso común: Desactivar/Activar usuario
                if (operation.Path == "active" && operation.Op.ToLower() == "replace")
                {
                    bool newStatus = bool.Parse(operation.Value.ToString());
                    await _employeeService.SetStatusAsync(id, newStatus);
                }

                // Caso: Cambio de apellido
                if (operation.Path == "name.familyName" && operation.Op.ToLower() == "replace")
                {
                    string newLastName = operation.Value.ToString();
                    await _employeeService.UpdateLastNameAsync(id, newLastName);
                }

                // Nota: SCIM también permite enviar el 'value' como un objeto
                // si el 'path' no está definido en la operación.
            }

            return NoContent(); // SCIM espera un 204 si todo salió bien
        }

        /// <summary>
        /// Dame los detalles del usuario con id 123.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            /* ... */
            return Ok();
        }

        /// <summary>
        /// Este método es el encargado de transformar tu entidad de base de datos (por ejemplo, Employee) al formato que el estándar SCIM espera recibir como respuesta.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private ScimUserRequest MapToScim(Employee employee)
        {
            return new ScimUserRequest
            {
                Id = employee.EmpId.ToString(),
                ExternalId = employee.ExternalId, // El ID original que nos mandó el IdP
                UserName = employee.Email,
                Active = employee.IsActive,
                Name = new ScimName
                {
                    GivenName = employee.Name
                    //FamilyName = employee.LastName
                },
                Emails = new List<ScimEmail> {
                                new ScimEmail { Value = employee.Email, Primary = true, Type = "work" }
                },
                Meta = new ScimMeta
                {
                    Created = employee.ModificationDate,
                    // Importante: SCIM pide la URL del recurso creado
                    Location = $"/scim/v2/Users/{employee.EmpId}"
                }
            };
        }
    }
}
