using AutoMapper;
using Rent.Core.Managers.Data;
using Rent.DataAccess.Entity;

namespace Rent.Core.Managers.Profiles
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<User, UserData>();
			CreateMap<UserData, User>();
		}
	}
}
