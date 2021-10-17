using System.ComponentModel.DataAnnotations;

namespace MicroServiceExamplePlatformService.Dtos
{
    public class PlatformCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Cost { get; set; }
    }

    public class PlatformPublishDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Event { get; set; }
    }
}