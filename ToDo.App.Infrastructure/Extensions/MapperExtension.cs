using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using ToDo.App.Application.Dto;
using ToDo.App.Domain.Entities;

namespace ToDo.App.Infrastructure.Extensions
{
    public static class MapperExtension
    {
        public static void AddMapper(this IServiceCollection services)
        {
            var configs = new TypeAdapterConfig();

            configs.ForType<ToDoItem, ToDoItemDto>()
                .Map(dst => dst.PriorityLevel, src => src.PriorityLevel.Value);
            configs.ForType<ToDoList, ToDoDto>()
                .Map(dst => dst.ToDoItems, src => src.ToDoItems);

            services.AddSingleton<IMapper>(new Mapper(configs));

        }
    }
}
