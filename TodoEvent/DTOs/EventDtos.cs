using System.ComponentModel.DataAnnotations;

namespace TodoEvent.DTOs
{
    public class EventDtoList
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Event Start and time is required.")]
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool AllDay { get; set; }
    }
    public class EventDtoCreate
    {
        [MaxLength(40)]
        [Required(ErrorMessage = "Event title is required.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(400)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Event Start and time is required.")]
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool AllDay { get; set; }
    }

    public class EventDtoUpdate
    {
        [MaxLength(40)]
        [Required(ErrorMessage = "Event title is required.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(400)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Event Start and time is required.")]
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool AllDay { get; set; }
    }
}
