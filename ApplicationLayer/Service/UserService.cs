using ApplicationLayer.DTO;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Service
{
    public class UserService
    {
        private readonly IUnitOfWork _u;
        public UserService(IUnitOfWork u)
        {
            _u = u;
        }

        //update user 
        public async Task<> UpdateUser(UserModel model)
        {
            try
            {
                var oldmodel = await _u.Repository<User>().GetById(model.Id);             
                if (oldmodel != null)
                {
                    if (model.AvatarFile == null)
                    {
                        oldmodel.Name = model.Name;
                        oldmodel.Email = model.Email;
                    }
                    else
                    {
                        oldmodel.Name = model.Name;
                        oldmodel.Email = model.Email;
                        //thuc hien xoa hinh cu up hinh moi va tra ve string avatar
                    }
                  
                }
            }
            catch(Exception ex) { 
            
            }
        
           
        }
    }
}
