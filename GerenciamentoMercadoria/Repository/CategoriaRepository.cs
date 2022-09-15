using GerenciamentoMercadoria.Context;
using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoMercadoria.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Categoria Cadastrar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }
        public Categoria Atualizar(Categoria categoria)
        {
            Categoria db = CarregarId(categoria.Id);

            if (db == null) throw new Exception("Houve um erro na atualização");

            db.Nome = categoria.Nome;

            _context.Categorias.Update(db);
            _context.SaveChanges();

            return db;
        }
        public bool Deletar(int id)
        {
            Categoria db = CarregarId(id);

            if (db == null) throw new Exception("Houve um erro ao apagar");

            _context.Categorias.Remove(db);
            _context.SaveChanges();

            return true;
        }

        public List<Categoria> Listar()
        {
            return _context.Categorias.ToList();
        }

        public Categoria CarregarId(int id)
        {
            return _context.Categorias.FirstOrDefault(p => p.Id == id);
        }
    }
}
