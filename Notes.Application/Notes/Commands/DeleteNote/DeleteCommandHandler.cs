using MediatR;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteNote;
public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
   private readonly INotesDbContext _dbContext;

   public DeleteNoteCommandHandler(INotesDbContext dbContext)
   {
      _dbContext = dbContext;
   }


   // Unit - это тип пустого ответа
   public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
   {

      var entity = await _dbContext.Notes
         .FindAsync(new object[] { request.Id }, cancellationToken);

      if (entity == null || entity.UserID != request.UserId)
      {
         throw new NotFoundException(nameof(Note), request.Id);
      }

      _dbContext.Notes.Remove(entity);
      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
   }

   Task IRequestHandler<DeleteNoteCommand>.Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
   {
      throw new NotImplementedException();
   }

}
