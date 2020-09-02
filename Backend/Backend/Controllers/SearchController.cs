using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly NorvoDBContext _context;

        public SearchController(NorvoDBContext context)
        {
            _context = context;
        }

        //[HttpGet("{sentence}")]
        //public async Task<ActionResult<List<Object>>> GetArtist(string sentence)
        //{
            
        //}
    }
}
