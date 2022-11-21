using GerenciamentoMercadoria.Context;
using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoMercadoria.Repository
{
    public class MercadoriaRepository : IMercadoriaRepository
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

            db.FabricanteId = mercadoria.FabricanteId;
            db.Nome = mercadoria.Nome;
            db.CategoriaId = mercadoria.CategoriaId;
            db.Descricao = mercadoria.Descricao;

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
            return _context.Mercadorias
                .Include(p => p.categoria)
                .Include(p => p.Fabricante).ToList();
        }

        public Mercadoria CarregarId(int id)
        {
            return _context.Mercadorias.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Fabricante> Fabricantes()
        {
            return _context.Fabricantes;
        }

        public IEnumerable<Categoria> Categorias()
        {
            return _context.Categorias;
        }
    }
}
    

