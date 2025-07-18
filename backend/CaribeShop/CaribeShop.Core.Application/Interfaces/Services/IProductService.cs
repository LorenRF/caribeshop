using CaribeShop.Core.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaribeShop.Core.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
    }
}
