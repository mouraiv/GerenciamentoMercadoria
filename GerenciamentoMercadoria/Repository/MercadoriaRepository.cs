using GerenciamentoMercadoria.Context;
using GerenciamentoMercadoria.Models;

namespace GerenciamentoMercadoria.Repository
{
    public class MercadoriaRepository
    {
        private readonly AppDbContext _context;

        public MercadoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Mercadoria Cadastrar(Mercadoria mercadoria)
        {
            _context.Mercadorias.Add(mercadoria);
            _context.SaveChanges();
            return mercadoria;
        }
        public Mercadoria Atualizar(Mercadoria mercadoria)
        {
            Mercadoria db = CarregarId(mercadoria.Id);

            if (db == null) throw new Exception("Houve um erro na atualização");

            db.Nome = mercadoria.Nome;

            _context.Mercadorias.Update(db);
            _context.SaveChanges();

            return db;
        }
        public bool Deletar(int id)
        {
            Mercadoria db = CarregarId(id);

            if (db == null) throw new Exception("Houve um erro ao apagar");

            _context.Mercadorias.Remove(db);
            _context.SaveChanges();

            return true;
        }

        public List<Mercadoria> Listar()
        {
            return _context.Mercadorias.ToList();
        }

        public Mercadoria CarregarId(int id)
        {
            return _context.Mercadorias.FirstOrDefault(p => p.Id == id);
        }
    }
}
    

