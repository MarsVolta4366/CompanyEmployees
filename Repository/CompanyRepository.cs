using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();

        public Company? GetCompanyByName(string companyName, bool trackChanges) =>
            FindByCondition(x => x.Name == companyName, trackChanges)
            .FirstOrDefault();
    }
}
