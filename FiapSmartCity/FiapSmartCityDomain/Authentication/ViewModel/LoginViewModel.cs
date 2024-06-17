namespace FiapSmartCityDomain.Authentication.ViewModel
{
    public class LoginViewModel
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }

        public string DisplayName { get; set; }

        public bool Active { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
