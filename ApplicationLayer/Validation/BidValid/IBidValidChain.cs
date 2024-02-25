using ApplicationLayer.DTO;
using DomainLayer.Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.BidValid
{
    public interface IBidValidChain
    {
        Task<(bool, string)> validateBidChain(BidModel model);
    }
}
