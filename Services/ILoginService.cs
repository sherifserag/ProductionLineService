using System.Threading.Tasks;

namespace ProductionLineService.Services
{
	public interface ILoginService
	{
		Task<string> LoginAsync(string username, string password);
	}
}