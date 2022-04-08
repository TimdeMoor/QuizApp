namespace ASPQuizApp.Models
{
    public class GebruikerDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public GebruikerDetailsViewModel() { }

        public GebruikerDetailsViewModel(int id, string name, string email, bool isAdmin)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.IsAdmin = isAdmin;
        }
    }
}
