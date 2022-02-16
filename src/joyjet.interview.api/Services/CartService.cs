using joyjet_interview_test.ApiModels;
using joyjet_interview_test.Models;
using joyjet_interview_test.Interfaces.Factories;
using joyjet_interview_test.Interfaces.Services;

namespace joyjet_interview_test.Services
{
    public class CartService : ICartService
    {
        private readonly IDiscountTypeFactory _discountTypeFactory;

        public CartService(IDiscountTypeFactory discountTypeFactory)
        {
            this._discountTypeFactory = discountTypeFactory;
        }

        public IEnumerable<PostCartResult> CalculateCart(PostCartInput param)
        {
            var result = this.CalculateCartSubTotal(param);
            result = this.CalculateDeliveryFee(param.DeliveryFees, result.ToList());
            return result;
        }

        private IEnumerable<PostCartResult> CalculateCartSubTotal(PostCartInput param)
        {
            return param.Carts
                .Select(x =>
                    new PostCartResult(id: x.Id,
                        subTotal: x.Items
                            .Join(param.Articles,
                                    item => item.ArticleId,
                                    article => article.Id,
                                    (item, article) => new
                                    {
                                        total = item.Quantity * CalculateDiscountedPrice(
                                                                    discount: param.Discounts.FirstOrDefault(d=>d.ArticleId == item.ArticleId), 
                                                                    subTotal: article.Price
                                                                )
                                    })
                            .Sum(x => x.total)
                ))
                .AsEnumerable();
        }

        private IEnumerable<PostCartResult> CalculateDeliveryFee(IEnumerable<DeliveryFeeModel> deliveryFees, IList<PostCartResult> param)
        {
            foreach (var item in param.ToList())
            {
                var deliveryFee = GetDeliveryFeeBySubTotalRange(deliveryFees, item.SubTotal);
                if (deliveryFee.HasValue)
                    item.DeliveryFee = deliveryFee.Value;
            }
            return param;
        }

        private long? GetDeliveryFeeBySubTotalRange(IEnumerable<DeliveryFeeModel> deliveryFees, long subTotal)
        {
            foreach (var item in deliveryFees)
            {
                if (subTotal >= item.EligibleTransactionVolume.MinPrice && (!item.EligibleTransactionVolume.MaxPrice.HasValue || subTotal < item.EligibleTransactionVolume.MaxPrice))
                {
                    return item.Price;
                }
            }
            return null;
        }

        private long CalculateDiscountedPrice(DiscountModel? discount, long subTotal)
        {
            if (discount == null)
                return subTotal;

            return _discountTypeFactory
                .Create(discount.Type)
                .GetDiscountedPrice(discount.Value, subTotal);
        }
    }
}
