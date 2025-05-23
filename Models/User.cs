namespace MyMvcApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }   // Ahora nullable
        public string? Email { get; set; }  // Ahora nullable
        public string? Phone { get; set; }  // Nueva propiedad
    }
}