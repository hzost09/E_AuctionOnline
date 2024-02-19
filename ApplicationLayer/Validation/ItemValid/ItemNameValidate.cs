using ApplicationLayer.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.ItemValid
{
    public class ItemNameValidate : IItemValidChain
    {
        public async Task<(bool, string)> validateItemChain(ItemModel model)
        {
            if (model.Name == null)
            {
                throw new ValidationException("Name is require");
            }
            if (model.Name.Length < 3)
            {
                throw new ValidationException("Name at least have 3 character");
            }
            return (true, "");
        }
    }
    public class ItemPriceValidate : IItemValidChain
    {
        public async Task<(bool, string)> validateItemChain(ItemModel model)
        {
            if (model.BeginPrice == null || model.WinningPrice == null || model.UpPrice == null)
            {
                throw new ValidationException("All Price in put must be fill");
            }
            if (model.BeginPrice <= 0)
            {
                throw new ValidationException("BeginPrice cant be 0 or lower");
            }
            if(model.UpPrice < 1)
            {
                throw new ValidationException("UpPrice at least more than 1 rup");
            }
            if (model.WinningPrice <= model.BeginPrice)
            {
                throw new ValidationException("WinningPrice must bigger than BeginPrice");
            }
            return (true, "");
        }
    }
    public class ItemImageValidate : IItemValidChain
    {
        public async Task<(bool, string)> validateItemChain(ItemModel model)
        {
            if (model.ImageFile == null)
            {
                throw new ValidationException("Image is require");
            }
            return (true, "");
        }
        
    }
    public class ItemDocumentValidate : IItemValidChain
    {
        public async Task<(bool, string)> validateItemChain(ItemModel model)
        {
            if (model.DocumentFile == null)
            {
                throw new ValidationException("DocumentFile is require");
            }
            return (true, "");
        }
    }
    public class ItemSellerIdValidate : IItemValidChain
    {
        public async Task<(bool, string)> validateItemChain(ItemModel model)
        {
            if (model.sellerId == null)
            {
                throw new ValidationException("Please provide the seller code is require");
            }
            return (true, "");
        }
    }
    public class ItemDateValidate : IItemValidChain
    {
        public async Task<(bool, string)> validateItemChain(ItemModel model)
        {
            if (model.BeginDate == null || model.EndDate == null )
            {
                throw new ValidationException("required");
            }
            if (model.BeginDate < DateTime.Now)
            {
                throw new ValidationException("EndDate cannot lower than today");
            }
            if (model.EndDate < model.BeginDate)
            {
                throw new ValidationException("EndDate cannot lower than BeginDate");
            }
            return (true, "");
        }
    }
}