using ApplicationLayer.DTO;
using ApplicationLayer.Validation.RegisterValid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.ItemValid
{
    public class ItemValid
    {
        private readonly List<IItemValidChain> _valid = new List<IItemValidChain>();

        public ItemValid AddValidator(IItemValidChain ilogchain)
        {
            _valid.Add(ilogchain);
            return this;
        }

        public async Task ActionValid(ItemModel model)
        {
            foreach (var valid in _valid)
            {
                await valid.validateItemChain(model);
            }
        }
    }
}
