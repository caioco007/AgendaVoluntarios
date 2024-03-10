namespace AgendaVoluntarios.DTO.InputModels
{
    public class EditProfileInputModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public List<Guid> Functions { get; set; }
    }
}
