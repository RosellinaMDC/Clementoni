using ClementoniWebAPI.Models.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClementoniWebAPI.Handlers.CommandHandlers;

public sealed record PostPersonaCommand(Person person) : IRequest;
public sealed record PutPersonaCommand(int id, Person person) : IRequest<bool>;
public sealed record DeletePersonaCommand(int id) : IRequest;

public sealed class PersonaCommandHandler :

     IRequestHandler<PostPersonaCommand>,
     IRequestHandler<PutPersonaCommand, bool>,
     IRequestHandler<DeletePersonaCommand>
    

{

    private readonly FormazioneDBContext _context;
    
    public PersonaCommandHandler(FormazioneDBContext context)
    {
        _context = context;
    }

    //POST
    public async Task Handle(PostPersonaCommand request, CancellationToken cancellationToken)
    {
        _context.Person.Add(request.person);
        await _context.SaveChangesAsync();
 
    }

    //PUT
    public async Task<bool> Handle(PutPersonaCommand request, CancellationToken cancellationToken)
    {
        if (request.id != request.person.Id)
        {
            return false;
        }

        _context.Entry(request.person).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;    
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonExists(request.id))
            {
                return false;
            }
            else
            {
                throw;
            }
        }
        
    }

    private bool PersonExists(int id)
    {
        throw new NotImplementedException();
    }

    //DELETE
    public async Task Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
    {
        var person = await _context.Person.FindAsync(request.id);
       

        _context.Person.Remove(person);
        await _context.SaveChangesAsync();

    }
}

