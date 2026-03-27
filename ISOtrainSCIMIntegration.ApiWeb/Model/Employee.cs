namespace ISOtrainSCIMIntegration.ApiWeb.Model
{
    public class Employee
    {
        private DateTime modificationDate;

        public string EmpId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// El ID original que nos mandó el IdP
        /// </summary>
        public string ExternalId { get; set; } // debemos guardar este dato en la tabla de empleados de ISOtrain

        public char EmpStatus { get; set; }

        public bool IsActive
        {
            get { return EmpStatus == 'A'; }
        }

        /// <summary>
        /// Fecha de creación del registro en BD
        /// </summary>
        public DateTime ModificationDate { get { return DateTime.Now; } set => modificationDate = value; }
    }
}
