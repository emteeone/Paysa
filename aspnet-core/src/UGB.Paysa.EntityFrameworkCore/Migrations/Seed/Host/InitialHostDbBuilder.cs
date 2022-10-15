using UGB.Paysa.EntityFrameworkCore;

namespace UGB.Paysa.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly PaysaDbContext _context;

        public InitialHostDbBuilder(PaysaDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
