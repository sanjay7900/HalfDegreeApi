using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HalfDegreeApi.Mapper
{
    public class ImageMapper
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public IFormFile? ImageName { get; set; }
        public int? Quantity { get; set; }
    }
}
