using RestaurantOrdering.Events.Domain;

namespace RestaurantOrdering.Events.Application;

public class EventContextMiddleware
{
    public void HandleEventContextAdded(EventContext entity)
    {
        Console.WriteLine($"Middleware triggered for new EventContext: {entity}");
    }
}