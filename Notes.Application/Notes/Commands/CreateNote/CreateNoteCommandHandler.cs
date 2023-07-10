using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.CreateNote
{
  public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
  {
    // обработчик

    // для сохранения изменения в бд
    private readonly INotesDbContext _dbContext;

    // что то про внедрение зависимостей
    public CreateNoteCommandHandler(INotesDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
      var note = new Note
      {
        UserID = request.UserId,
        Title = request.Title,
        Id = Guid.NewGuid(),
        CreationDate = DateTime.Now,
        EditDate = null
      };
      // добавление и сохранение
      await _dbContext.Notes.AddAsync(note, cancellationToken);
      await _dbContext.SaveChangesAsync(cancellationToken);

      return note.Id;
    }
  }
}
