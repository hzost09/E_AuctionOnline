using ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.ItemValid
{
    public interface IItemValidChain
    {
        Task<(bool, string)> validateItemChain(ItemModel model);
    }
}
