using BiletPortal.Data;
using BiletPortal.Dto;
using BiletPortal.Models;
using BiletPortal.Session;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Component
{
    public class CardSumList : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public CardSumList(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<CardItem> card = HttpContext.Session.GetJson<List<CardItem>>("Card") ?? new List<CardItem>();

            CardViewModel cardVm = new()
            {
                CardItems = card,
                GrandTotal = card.Sum(x => x.Quantity * x.Price)
            };
            return View(cardVm);
        }
    }
}
