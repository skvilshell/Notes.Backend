
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Application.Common.Exceptions;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.Update
{
  public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
  {
    private readonly INotesDbContext _context;

    public UpdateNoteCommandHandler(INotesDbContext context)
    {
      _context = context;
    }

    public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
      var entity = await _context.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);
      
      if (entity == null || entity.UserID != request.UserId)
      {
        throw new NotFoundException(nameof(Note), request.Id);
      }
      entity.Details = request.Details;
      entity.Title = request.Title;
      entity.EditDate = DateTime.Now;

      await _context.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }


    // хз это нужно было, чтоб не ругался vs
    Task IRequestHandler<UpdateNoteCommand>.Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
