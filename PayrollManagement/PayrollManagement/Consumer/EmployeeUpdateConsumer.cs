using MassTransit;
using PayrollManagement.Models;
using Shared.Messages;

namespace PayrollManagement.Consumer
{
    public class EmployeeUpdateConsumer : IConsumer<EmployeeUpdateDto>
    {
        private readonly AppDbContext _context;

        public EmployeeUpdateConsumer(AppDbContext context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<EmployeeUpdateDto> context)
        {
            var msg = context.Message;

            var payroll = new Payroll
            {
                EmployeeId = msg.Id,
                Salary = msg.Salary,
                PayDate = DateTime.Now
            };

            _context.Payrolls.Add(payroll);
            await _context.SaveChangesAsync();
        }
    }
}
