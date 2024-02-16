using ApplicationLayer.DTO;
using ApplicationLayer.InterfaceService;
using DomainLayer.Core.Enities;
using DomainLayer.Imterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Service
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _u;
        private readonly IphotoService _p;

        public UserService(IUnitOfWork u,IphotoService p)
        {
            _u = u;
            _p = p; 
        }

        //update user 
        public async Task<int> UpdateUser(UserModel model)
        {
            try
            {
                var oldmodel = await _u.Repository<User>().GetById(model.Id);
                string avatalink = "";
                if (oldmodel != null)
                {
                    if (model.AvatarFile == null)
                    {
                        oldmodel.Name = model.Name;
                        oldmodel.Email = model.Email;
                    }
                    else
                    {                                           
                        if (oldmodel.Avatar != null)
                        {
                            var avataResult = await _p.DeletPhoto(oldmodel.Avatar);
                            avatalink =  await _p.addPhoto(model.AvatarFile);
                        }
                        else
                        {
                            avatalink = await _p.addPhoto(model.AvatarFile);
                        }
                        oldmodel.Name = model.Name;
                        oldmodel.Email = model.Email;
                        oldmodel.Avatar = avatalink;

                    }                    
                }
                await _u.SaveChangesAsync();
                return 1;
            }
            catch(Exception ex) 
            { 
                await _u.RollBackChangesAsync();
                return -1;
            }
        
           
        }
    }
}
