using Clay.AccessControl.Api.Data;

namespace Clay.AccessControl.Api.Services {
    public class AccessControlService : IAccessControlService {
        private readonly AccessControlDbContext _context;

        public AccessControlService(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<AccessControlDbContext>();
        }
    }
}