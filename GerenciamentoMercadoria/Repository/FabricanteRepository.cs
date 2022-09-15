using GerenciamentoMercadoria.Context;
using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository.Interface;

namespace GerenciamentoMercadoria.Repository
{
    public class FabricanteRepository : IFabricanteRepository
    {
        private readonly AppDbContext _context;

        public FabricanteRepository(AppDbContext context)
        {
            _context = context;
        }
        public Fabricante Cadastrar(Fabricante fabricante)
        {
            _context.Fabricantes.Add(fabricante);
            _context.SaveChanges();
            return fabricante;
        }
        public Fabricante Atualizar(Fabricante fabricante)
        {
            Fabricante db = CarregarId(fabricante.Id);

            if (db == null) throw new Exception("Houve um erro na atualização");

            db.Nome = fabricante.Nome;

            _context.Fabricantes.Update(db);
            _context.SaveChanges();

            return db;
        }
        public bool Deletar(int id)
        {
            Fabricante db = CarregarId(id);

            if (db == null) throw new Exception("Houve um erro ao apagar");

            _context.Fabricantes.Remove(db);
            _context.SaveChanges();

            return true;
        }

        public List<Fabricante> Listar()
        {
            return _context.Fabricantes.ToList();
        }

        public Fabricante CarregarId(int id)
        {
            return _context.Fabricantes.FirstOrDefault(p => p.Id == id);
        }
    }
}
