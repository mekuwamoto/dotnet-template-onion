using AutoMapper;
using Onion.Template.Application.Todos.Response;
using Onion.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Todos.Profiles;

public class TodoMappingProfile : Profile
{
	public TodoMappingProfile()
	{
		CreateMap<Todo, TodoResponse>()
			.ForMember(dest => dest.TodoId, opt => opt.MapFrom(src => src.Id));
	}
}
