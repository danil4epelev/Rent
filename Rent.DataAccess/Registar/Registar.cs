using Fx.Ef;
using Microsoft.Extensions.DependencyInjection;
using Rent.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.DataAccess.Registar
{
	public static class Registar
	{
		public static void RegisterDataAccess(this IServiceCollection services)
		{
			services.AddScoped<IEntityRepository<User>, EntityRepository<User>>();
		}
	}
}
