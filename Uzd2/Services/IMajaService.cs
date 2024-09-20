using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uzd2.Datatypes;
using Uzd2.DTOs;

namespace Uzd2.Services
{
    public interface IMajaService
    {
        Task<Maja> CreateMaja(Maja maja);
        Task<Maja> UpdateMaja(long id,Maja maja);
        Task<Maja> DeleteMaja(long id);
        Task<Maja> GetMajaById(long id);
        MajaDTO GetMajaDTO(Maja maja);
        Task<ActionResult<IEnumerable<Maja>>> GetMaja();
        Maja GetMajaFromDTO(MajaDTO majaDTO);
    }
    public class MajaService : IMajaService
    {
        private readonly Uzd2Context _context;
        private readonly IMapper _mapper;

        public MajaService(Uzd2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Maja> CreateMaja(Maja maja)
        {
            _context.MajaItems.Add(maja);
            await _context.SaveChangesAsync();

            return maja;
        }
        public async Task<Maja> UpdateMaja(long id, Maja maja)
        {
            _context.Entry(maja).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MajaExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return maja;
        }
        public async Task<Maja> DeleteMaja(long id)
        {
            var maja = await _context.MajaItems.FindAsync(id);
            if (maja == null)
            {
                return null;
            }

            _context.MajaItems.Remove(maja);
            await _context.SaveChangesAsync();

            return maja;
        }
        public async Task<Maja> GetMajaById(long id)
        {
            var maja = await _context.MajaItems.FindAsync(id);

            if (maja == null)
            {
                return null;
            }

            return maja;
        }
        public async Task<ActionResult<IEnumerable<Maja>>> GetMaja()
        {
            return await _context.MajaItems.ToListAsync();
        }
        public MajaDTO GetMajaDTO(Maja maja)
        {
            return _mapper.Map<MajaDTO>(maja);
        }
        public Maja GetMajaFromDTO(MajaDTO majaDTO)
        {
            return _mapper.Map<Maja>(majaDTO);
        }
        private bool MajaExists(long id)
        {
            return _context.MajaItems.Any(e => e.MajaNumurs == id);
        }
    }

}
