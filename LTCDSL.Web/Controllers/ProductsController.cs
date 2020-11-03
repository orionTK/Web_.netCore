using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTCSDL.Web.Controllers
{
    using BLL;
    using DAL.Models;
    using Common.Req;
    using System.Collections.Generic;
    //using BLL.Req;
    using Common.Rsp;
    using LTCSDL.DAL;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
            _svc = new ProductsSvc();
        }

        
        //Đề 5 câu 3
        [HttpPost("SP-Insert-Products")]
        public IActionResult SP_InsertProducts([FromBody]ProductsReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_InsertProducts(req);
            res.Data = hist;
            return Ok(res);
        }
        private readonly ProductsSvc _svc;
    }
}