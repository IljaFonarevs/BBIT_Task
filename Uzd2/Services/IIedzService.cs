using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uzd2.Datatypes;
using Uzd2.DTOs;

namespace Uzd2.Services
{
    public interface IIedzService
    {
        Task<Iedzivotajs> CreateIedz(Iedzivotajs iedz);
        Task<Iedzivotajs> UpdateIedz(long id, Iedzivotajs iedz);
        Task<Iedzivotajs> DeleteIedz(long id);
        Task<Iedzivotajs> GetIedzById(long id);
        Task<ActionResult<IEnumerable<Iedzivotajs>>> GetIedz();
        IedzDTO GetIedzDTO(Iedzivotajs iedz);
        Iedzivotajs GetIedzFromDTO(IedzDTO iedzDTO);
    }
    public class IedzService : IIedzService
    {
        private readonly Uzd2Context _context;
        private readonly IMapper _mapper;

        public IedzService(Uzd2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Iedzivotajs> CreateIedz(Iedzivotajs iedz)
        {
            var dziv = await _context.DzivoklisItems.FindAsync(iedz.DzivNumurs);
            iedz.Dzivoklis = dziv;

            _context.IedzivotajsItems.Add(iedz);
            await _context.SaveChangesAsync();

            return iedz;
        }
        public async Task<Iedzivotajs> UpdateIedz(long id, Iedzivotajs iedz)
        {
            var dziv = await _context.DzivoklisItems.FindAsync(iedz.DzivNumurs);
            iedz.Dzivoklis = dziv;

            _context.Entry(iedz).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IedzivotajsExists(id))
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
        public async Task<Iedzivotajs> DeleteIedz(long id)
        {
            var iedz = await _context.IedzivotajsItems.FindAsync(id);
            if (iedz == null)
            {
                return null;
            }

            _context.IedzivotajsItems.Remove(iedz);
            await _context.SaveChangesAsync();

            return iedz;
        }
        public async Task<Iedzivotajs> GetIedzById(long id)
        {
            var iedz = await _context.IedzivotajsItems.FindAsync(id);

            if (iedz == null)
            {
                return null;
            }

            return iedz;
        }
        public async Task<ActionResult<IEnumerable<Iedzivotajs>>> GetIedz()
        {
            return await _context.IedzivotajsItems.ToListAsync();
        }
        private bool IedzivotajsExists(long id)
        {
            return _context.IedzivotajsItems.Any(e => e.PersKods == id);
        }
        public IedzDTO GetIedzDTO(Iedzivotajs iedz)
        {
            return _mapper.Map<IedzDTO>(iedz);
        }
        public Iedzivotajs GetIedzFromDTO(IedzDTO iedzDTO)
        {
            return _mapper.Map<Iedzivotajs>(iedzDTO);
        }
    }

}
