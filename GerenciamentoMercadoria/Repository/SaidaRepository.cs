using GerenciamentoMercadoria.Context;
using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoMercadoria.Repository
{
    public class SaidaRepository : ISaidaRepository
    {
        private readonly AppDbContext _context;

        public SaidaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Saida Cadastrar(Saida saida)
        {
            _context.Saidas.Add(saida);
            saida.DataHora = DateTime.Now;
            _context.SaveChanges();
            return saida;
        }
        public Saida Atualizar(Saida saida)
        {
            Saida db = CarregarId(saida.Id);

            if (db == null) throw new Exception("Houve um erro na atualização");

            db.Quantidade = saida.Quantidade;
            db.Rua = saida.Rua;
            db.Numero = saida.Numero;
            db.Bairro = saida.Bairro;
            db.Estado = saida.Estado;
            db.MercadoriaId = saida.MercadoriaId;

            _context.Saidas.Update(db);
            _context.SaveChanges();

            return db;
        }
        public bool Deletar(int id)
        {
            Saida db = CarregarId(id);

            if (db == null) throw new Exception("Houve um erro ao apagar");

            _context.Saidas.Remove(db);
            _context.SaveChanges();

            return true;
        }

        public List<Saida> Listar()
        {
            return _context.Saidas
                .Include(p => p.mercadoria).ToList();
        }

        public Saida CarregarId(int id)
        {
            return _context.Saidas.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Mercadoria> Mercadorias()
        {
            return _context.Mercadorias;
        }
    }
}




