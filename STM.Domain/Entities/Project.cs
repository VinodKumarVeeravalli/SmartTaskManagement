namespace STM.Domain.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public string? Description { get; set; }

        //foreign key to aspnetusers.id
        public string CreatedBy { get; set; } = string.Empty;

        //navigation property to ApplicationUser
        public ApplicationUser CreatedByUser { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
