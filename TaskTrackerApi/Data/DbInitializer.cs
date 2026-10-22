using TaskTrackerApi.Services;

namespace TaskTrackerApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Projects.Any())
            {
                return;
            }

            var projects = ProjectStore.Projects;

            context.Projects.AddRange(projects);
            context.SaveChanges();
        }
    }
}