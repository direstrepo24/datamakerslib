using Microsoft.EntityFrameworkCore;

namespace datamakerslib.Repository
{
    public interface IRepositoryInjection
    {
          IRepositoryInjection SetContext(DbContext context);

    }
}