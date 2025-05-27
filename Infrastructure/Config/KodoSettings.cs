namespace Infrastructure.Config
{
    public class KodoSettings
    {
        public required string Host { get; set; }
        public required ClickUpSettings ClickUp { get; set; }

        public class ClickUpSettings
        {
            public required string ApiAddress { get; set; }
            public required string ClientId { get; set; }
            public required string ClientSecret { get; set; }
            public required string PersonalToken { get; set; }
        }
    }
}
