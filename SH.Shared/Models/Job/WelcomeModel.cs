using SH.Shared.Core.Types;

namespace SH.Shared.Models.Job
{
    public class WelcomeModel(string id, DateTimeOffset runAt) : JobDocument(new JobIdentity(JobType.GoPublic, id), runAt)
    {
        public string? Email { get; set; }
    }
}