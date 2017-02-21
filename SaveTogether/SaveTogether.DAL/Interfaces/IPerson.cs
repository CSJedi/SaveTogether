namespace SaveTogether.DAL.Interfaces
{
    public interface IPerson
    {
        int Id { get; set; }

        string Email { get; set; }

        string Name { get; set; }
    }
}