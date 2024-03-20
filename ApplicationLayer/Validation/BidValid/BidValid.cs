using ApplicationLayer.DTO;
using ApplicationLayer.Validation.ItemValid;
using DomainLayer.Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.BidValid
{
    public class BidValid
    {
        private readonly List<IBidValidChain> _valid = new List<IBidValidChain>();

        public BidValid AddValidator(IBidValidChain ilogchain)
        {
            _valid.Add(ilogchain);
            return this;
        }

        public async Task ActionValid(BidModel model)
        {
            foreach (var valid in _valid)
            {
                await valid.validateBidChain(model);
            }
        }
    }
}
