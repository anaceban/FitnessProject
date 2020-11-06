using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserReviewService _userReviewService;
        public ReviewController(IMapper mapper, IUserReviewService userReviewService)
        {
            _mapper = mapper;
            _userReviewService = userReviewService;
        }
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            var reviews = _userReviewService.GetReviews();
            var rezult = reviews.Select(r => _mapper.Map<ReviewDto>(r));
            return Ok(rezult);
        }

        [HttpGet("{UserId}")]
        public IActionResult GetAllReviewsByUserId(int id)
        {
            var reviews = _userReviewService.GetReviewsByUserId(id);
            if (reviews == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<ReviewDto>(reviews));
            }
        }
        [HttpPost]
        public IActionResult AddNewReview([FromBody] ReviewDto dto)
        {
            var review = _userReviewService.AddNewReview(dto);

            var result = _mapper.Map<ReviewDto>(review);

            return CreatedAtAction(nameof(GetAllReviews), new { id = review.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReviewDto dto)
        {
            _userReviewService.UpdateReview(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userReviewService.RemoveReview(id);
            return NoContent();
        }
    }
}
