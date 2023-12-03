using CQRS.Core.Commands.Abstract;
using Employee.Messages.Enums;

namespace Employee.Messages.Commands
{
    public class RegisterEmployeeCommand: ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public EmployeeRole Role { get; set; }
    }
}
