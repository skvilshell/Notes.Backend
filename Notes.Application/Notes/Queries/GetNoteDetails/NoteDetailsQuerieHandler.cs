using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails;
public class NoteDetailsQuerieHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
{
   private readonly INotesDbContext _dbContext;
   private readonly IMapper _mapper;
    public NoteDetailsQuerieHandler(INotesDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

    }

    public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
   {
      var entity = await _dbContext.Notes
         .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

      if (entity == null || entity.UserID != request.UserId)
      {
         throw new NotFoundException(nameof(Note), request.Id);
      }

      return _mapper.Map<NoteDetailsVm>(entity);
   }
}
