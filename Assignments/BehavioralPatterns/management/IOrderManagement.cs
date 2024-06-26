﻿using BehavioralPatterns.publisher;
using BehavioralPatterns.subscriber;

namespace BehavioralPatterns.management
{
    internal interface IOrderManagement
    {
        List<Order> GetOrders();
        void PlaceOrder(Customer customer);
        void ProcessOrder(string orderNumber);
        void PrepareOrderForShipping(string orderNumber);
    }
}
