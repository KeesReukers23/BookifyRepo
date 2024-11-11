namespace Logic.Entities
{
    public class User
    {
        private string? _password;
        public Guid UserId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Password
        {
            get { return this._password; }
            set { _password = PasswordHasher.HashPassword(value!); }
        }

        //Constructor for registering new User
        public User(string firstName, string lastName, string email, string password)
        {
            UserId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        //Constructor for retrieving User from db
        public User(Guid userId, string firstName, string lastName, string email)
        {
            this.UserId = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

    }
}