using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company? GetCompanyByName(string companyName, bool trackChanges);
    }
}
