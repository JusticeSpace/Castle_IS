using System;

namespace Castle
{
    public static class Logger
    {
        public static void LogAction(string action, int userId, string details = null)
        {
            using (var context = new Amo_CastleEntities1())
            {
                var log = new Logs
                {
                    Action = action,
                    Date = DateTime.Now,
                    UserId = userId,
                    Details = details
                };
                context.Logs.Add(log);
                context.SaveChanges();
            }
        }
    }
}