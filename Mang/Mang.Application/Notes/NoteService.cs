using System.Threading.Tasks;
using Game.Domain.Shared;
using Mang.Application.Contract.Notes.Dtos;
using Mang.Domain;
using Mang.Infrastructure.Repository;
using Mang.Public.Util;
using Mang.ServiceBase.Impl;
using NotImplementedException = System.NotImplementedException;

namespace Mang.Application.Contract.Notes
{
    public class NoteService : BaseService, INoteService
    {
        private readonly IRepository<Note> _repository;

        public NoteService(IRepository<Note> repository)
        {
            _repository = repository;
        }

        public async Task<NoteDto> GetAsync(Gender gender)
        {
            var note = await _repository.Where(a => a.Gender == gender).FirstAsync();
            ThrowIf(note == null, new BusinessException("还没有纸条数据"));

            return new NoteDto
            {
                Id = note.Id,
                UserId = note.UserId,
                Content = note.Content,
            };
        }

        public async Task<bool> CreateAsync(CreateNoteDto input)
        {
            throw new NotImplementedException();
        }
    }
}