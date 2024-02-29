using ClementoniWebAPI.Models.DB;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ClementoniWebAPI.Handlers.QueryHandlers;

public sealed record GetPersonaQuery() : IRequest<List<Person>>;
public sealed record GetPersonaByIdQuery(int id) : IRequest<Person>;
public sealed record GetPersonaConDapperQuery() : IRequest<List<Person>>;
public sealed record GetPersonaByIdConDapperQuery(int id) : IRequest<Person>;

public sealed class PersonaQueryHandler :
     IRequestHandler<GetPersonaQuery, List<Person>>,
     IRequestHandler<GetPersonaByIdQuery, Person>,
     IRequestHandler<GetPersonaConDapperQuery, List<Person>>,
     IRequestHandler<GetPersonaByIdConDapperQuery, Person>
{

    private readonly FormazioneDBContext _context;
    private readonly string _connectionString;

    public PersonaQueryHandler(FormazioneDBContext context)
    {
        _context = context;
        _connectionString = context.Database.GetConnectionString()!;
    }

    public async Task<List<Person>> Handle(GetPersonaQuery request, CancellationToken cancellationToken)
    {
        
        return await _context.Person.ToListAsync();

    }

    public async Task<Person> Handle(GetPersonaByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await _context.Person.FindAsync(request.id);


        return person;
    }
    public async Task<List<Person>> Handle(GetPersonaConDapperQuery request, CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM Person";
        using var connection = new SqlConnection(_connectionString);
        var risultato = (await connection.QueryAsync<Person>(query,
            new { param = (int?)1 })).ToList();
        
        return risultato;

    }

    public async Task<Person> Handle(GetPersonaByIdConDapperQuery request, CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM Person WHERE Person.id = @param";
        using var connection = new SqlConnection(_connectionString);
        var risultato = (await connection.QueryAsync<Person>(query,
            new { param = request.id })).SingleOrDefault();


        return risultato;
    }
}

