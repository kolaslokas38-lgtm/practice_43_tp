using System;
using System.Collections.Generic;

namespace Task;

public class PromotionManager
{
    private List<ICustomer> customers = new List<ICustomer>();

    public void Subscribe(ICustomer customer)
    {
        customers.Add(customer);
        Console.WriteLine($"{customer.GetName()} подписался на акции");
    }

    public void Unsubscribe(ICustomer customer)
    {
        customers.Remove(customer);
        Console.WriteLine($"{customer.GetName()} отписался от акций");
    }

    public void AddPromotion(string promotion)
    {
        Console.WriteLine($"\nНовая акция: {promotion}");
        NotifyCustomers(promotion);
    }

    private void NotifyCustomers(string promotion)
    {
        foreach (var customer in customers)
        {
            customer.Update(promotion);
        }
    }
}