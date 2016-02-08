using System;
using AutoMapper;
using InventoryManagement.Web.Infrastructure.Mapping;

namespace InventoryManagement.Web.Models.Carton
{
    public class CartonSummaryViewModel : IHaveCustomMappings
    {
        public int CartonId { get; set; }
        public int NumberOfPieces { get; set; }
        public string ItemName { get; set; }
        public DateTime CreatedAt { get; set; }


        public void CreateMappings(IConfiguration configuration)
        {

            configuration.CreateMap<Domain.Carton, CartonSummaryViewModel>()
                .ForMember(m => m.CartonId, opt => opt.MapFrom(u => u.CartonId))
                .ForMember(m => m.NumberOfPieces, opt => opt.MapFrom(u => u.NumberOfPieces))
                .ForMember(m => m.ItemName, opt => opt.MapFrom(u => u.Item.ItemName))
                .ForMember(m => m.CreatedAt, opt => opt.MapFrom(u => u.CreatedAt));
         
        }
    }
}