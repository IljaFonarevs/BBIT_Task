using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uzd2.Datatypes;
using Uzd2.DTOs;

namespace Uzd2.Services
{
    public interface IDzivService
    {
        Task<Dzivoklis> CreateDziv(Dzivoklis iedz);
        Task<Dzivoklis> UpdateDziv(long id, Dzivoklis iedz);
        Task<Dzivoklis> DeleteDziv(long id);
        Task<Dzivoklis> GetDzivById(long id);
        Task<ActionResult<IEnumerable<Dzivoklis>>> GetDziv();
        Task<ActionResult<IEnumerable<Dzivoklis>>> GetApartmentsFromHouse(int houseID);
        DzivDTO GetDzivDTO(Dzivoklis dziv);
        Dzivoklis GetDzivFromDTO(DzivDTO dzivDTO);

    }
    public class DzivService : IDzivService
    {
        public Uzd2Context _context;
        private readonly IMapper _mapper;

        public DzivService(Uzd2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Dzivoklis> CreateDziv(Dzivoklis iedz)
        {
            var maja = await _context.MajaItems.FindAsync(iedz.MajaID);
            iedz.Maja = maja;

            _context.DzivoklisItems.Add(iedz);
            await _context.SaveChangesAsync();

            return iedz;
        }
        public async Task<ActionResult<IEnumerable<Dzivoklis>>> GetApartmentsFromHouse(int houseID)
        {
            var allAparts = await _context.DzivoklisItems.ToListAsync();
            List<Dzivoklis> neededAparts = new List<Dzivoklis>();

            allAparts.ForEach(d => { if(d.MajaID == houseID) neededAparts.Add(d); });

            return neededAparts;


        }
        public async Task<Dzivoklis> UpdateDziv(long id, Dzivoklis iedz)
        {
            var maja = await _context.MajaItems.FindAsync(iedz.MajaID);
            iedz.Maja = maja;
            _context.Entry(iedz).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DzivoklisExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return iedz;
        }
        public async Task<Dzivoklis> DeleteDziv(long id)
        {
            var iedz = await _context.DzivoklisItems.FindAsync(id);
            if (iedz == null)
            {
                return null;
            }

            _context.DzivoklisItems.Remove(iedz);
            await _context.SaveChangesAsync();

            return iedz;
        }
        public async Task<Dzivoklis> GetDzivById(long id)
        {
            var iedz = await _context.DzivoklisItems.FindAsync(id);

            if (iedz == null)
            {
                return null;
            }

            return iedz;
        }
        public async Task<ActionResult<IEnumerable<Dzivoklis>>> GetDziv()
        {
            return await _context.DzivoklisItems.ToListAsync();
        }
        private bool DzivoklisExists(long id)
        {
            return _context.DzivoklisItems.Include(d => d.Maja).Any(e => e.DzivNumurs == id);
        }
        public DzivDTO GetDzivDTO(Dzivoklis dziv)
        {
            return _mapper.Map<DzivDTO>(dziv);
        }
        public Dzivoklis GetDzivFromDTO(DzivDTO dzivDTO)
        {
            return _mapper.Map<Dzivoklis>(dzivDTO);
        }
    }

}
