using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        ////private readonly IGenericRepository<Product> _productsRepo;
        ////private readonly IGenericRepository<DeliveryMethod> _deliveryMethodsRepo;
        ////private readonly IGenericRepository<Order> _ordersRepo;

        public OrderService(
            IBasketRepository basketRepo,
            IUnitOfWork unitOfWork,
            IPaymentService paymentService
            ///IGenericRepository<Product> productsRepo,
            ///IGenericRepository<DeliveryMethod> deliveryMethodsRepo,
            ///IGenericRepository<Order> ordersRepo
            )
        {
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            ///_basketRepo = basketRepo;
            ///_productsRepo = productsRepo;
            ///_deliveryMethodsRepo = deliveryMethodsRepo;
            ///_ordersRepo = ordersRepo;
        }


        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            //Get basket
            var basket = await _basketRepo.GetBasketAsync(basketId);


            //Get selected items from basket
            var OrderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                var ProductItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);

                var OrderItem = new OrderItem(ProductItemOrdered, product.Price, item.Quantity);

                OrderItems.Add(OrderItem);
            }


            //Calculate subtotal
            var subtotal = OrderItems.Sum(item => item.Price * item.Quantity);


            //Get delivery method
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //Validate payment intent
            var spec = new OrderWithPaymentIntentIdSpecification(basket.PaymentIntentId);

            var existingOrder = _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);

            if (existingOrder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(await existingOrder);

                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }

            //Create order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, OrderItems, subtotal, basket.PaymentIntentId);

            await _unitOfWork.Repository<Order>().Add(order);


            //Save to database
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;

            return order;
        }

        public Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
