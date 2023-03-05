using System.Collections.Generic;
using CatCards.DAO;
using CatCards.Models;
using CatCards.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatCards.Controllers
{
    
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly ICatCardDao cardDao;
        private readonly ICatFactService catFactService;
        private readonly ICatPicService catPicService;

        public CatController(ICatCardDao _cardDao, ICatFactService _catFact, ICatPicService _catPic)
        {
            catFactService = _catFact;
            catPicService = _catPic;
            cardDao = _cardDao;
        }

        [HttpGet("/cards/{id}random")]
        public CatCard RandomCard(int id)
        {
            CatCard randomFactCard = cardDao.GetCard(id);
            return randomFactCard;
        }


        [HttpGet]
        public List<CatCard> ListCards()
        {
            return cardDao.GetAllCards();
        }

        [HttpGet("{id}")]
        public CatCard GetCards(int id)
        {
            CatCard catCard = cardDao.GetCard(id);
            return catCard;
        }

        [HttpPost]
        public ActionResult<CatCard> AddCard(CatCard cardToSave)
        {
            CatCard added = cardDao.SaveCard(cardToSave);
            return Created($"/cards/{added.CatCardId}", added);
        }

        [HttpPut("{id}")]
        public ActionResult<CatCard> UpdateCard(int id, CatCard cardIn)
        {
            if (id != cardIn.CatCardId)
            {
                return BadRequest("Wrong endpoint");
            }

            CatCard existingCard = cardDao.GetCard(id);

            if (existingCard == null)
            {
                return NotFound();
            }

            existingCard.CatFact = cardIn.CatFact;
            existingCard.ImgUrl = cardIn.ImgUrl;
            existingCard.Caption = cardIn.Caption;

            CatCard result = cardDao.UpdateCard(existingCard != cardIn);

            return result;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCard(int id)
        {
            CatCard existingCard = cardDao.GetCard(id);
            if (existingCard == null)
            {
                return NotFound();
            }
            bool result = cardDao.RemoveCard(id);
            if (result)
            {
                return NoContent();
            }
            return StatusCode(500);
        }
    }
}
