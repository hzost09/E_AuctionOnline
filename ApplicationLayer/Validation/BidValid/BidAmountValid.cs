using ApplicationLayer.DTO;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.BidValid
{
    public class BidAmountValid : IBidValidChain
    {
        private readonly IUnitOfWork _u;
        public BidAmountValid(IUnitOfWork u)
        {
            _u = u;
        }
        public async Task<(bool, string)> validateBidChain(BidModel model)
        {
            if (model == null || model.ItemId == 0 || model.UserId == 0)
            {
                throw new ValidationException("there something missing with your request");
            }
            var checkitem = await _u.Repository<Item>().EntitiesCondition().Include(x => x.Auctionhistory).FirstOrDefaultAsync(x => x.Id == model.ItemId);
            var checkbidAmout = await _u.Repository<Bid>().EntitiesCondition().Where(x => x.ItemId == checkitem.Id).ToListAsync();
            if (checkbidAmout.Count == 0 )
            {
                return (true, "");
            }
            var takehighestbidAmount = checkbidAmout.OrderByDescending(x => x.BidAmount).FirstOrDefault();         
            if (model.BidAmount <= takehighestbidAmount.BidAmount || model.BidAmount < checkitem.UpPrice || model.BidAmount <= 0 )
            {
                throw new ValidationException("Price to bid must follow those condition:higher than last bid , more than 0 and more than UpPrice");
            }
            return (true,"");
        }
    }
    public class BidTimeValid:IBidValidChain
    {
        private readonly IUnitOfWork _u;
        public BidTimeValid(IUnitOfWork u)
        {
            _u  = u;
        }
        public async Task<(bool, string)> validateBidChain(BidModel model)
        {
            var checkitem = await _u.Repository<Item>().EntitiesCondition().Include(x => x.Auctionhistory).FirstOrDefaultAsync(x => x.Id == model.ItemId);
            if (checkitem.EndDate <= DateTime.Now)
            {
                throw new ValidationException("Auction End");
            }
            return (true,"");
        }
    }
    public class BidWinnerValid: IBidValidChain
    {
        private readonly IUnitOfWork _u;
        public BidWinnerValid(IUnitOfWork u)
        {
            _u = u;
        }
        public async Task<(bool, string)> validateBidChain(BidModel model)
        {
            var checkitem = await _u.Repository<Item>().EntitiesCondition().Include(x => x.Seller).FirstOrDefaultAsync(x => x.Id == model.ItemId);
            if (model.UserId == checkitem.sellerId)
            {
                throw new ValidationException("you cant bid your own item");
            }
            if (checkitem.Auctionhistory.WinnerId != null && checkitem.Auctionhistory.WinnerId != 0)
            {
                throw new ValidationException("there was a winner");
            }
            return (true, "");
        }
    }
}
