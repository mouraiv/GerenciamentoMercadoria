using GerenciamentoEntrada.Repository.Interface;
using GerenciamentoMercadoria.Context;
using GerenciamentoMercadoria.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoEntrada.Repository
{
    public class EntradaRepository : IEntradaRepository
    {
        private readonly AppDbContext _context;

        public EntradaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Entrada Cadastrar(Entrada entrada)
        {
            _context.Entradas.Add(entrada);
            entrada.DataHora = DateTime.Now;
            _context.SaveChanges();
            return entrada;
        }
        public Entrada Atualizar(Entrada entrada)
        {
            Entrada db = CarregarId(entrada.Id);

            if (db == null) throw new Exception("Houve um erro na atualização");

            db.Quantidade = entrada.Quantidade;
            db.Rua = entrada.Rua;
            db.Numero = entrada.Numero;
            db.Bairro = entrada.Bairro;
            db.Estado = entrada.Estado;
            db.MercadoriaId = entrada.MercadoriaId;

            _context.Entradas.Update(db);
            _context.SaveChanges();

            return db;
        }
        public bool Deletar(int id)
        {
            Entrada db = CarregarId(id);

            if (db == null) throw new Exception("Houve um erro ao apagar");

            _context.Entradas.Remove(db);
            _context.SaveChanges();
            return true;
        }

        public List<Entrada> Listar()
        {
            return _context.Entradas
                .Include(p => p.mercadoria).ToList();
        }

        public Entrada CarregarId(int id)
        {
            return _context.Entradas.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Mercadoria> Mercadorias()
        {
            return _context.Mercadorias;
        }

        public List<Entrada> Listar(string day, string mes, string ano)
        {
            DateTime mesInicio = DateTime.Parse($"1/{mes}/{ano}");
            DateTime mesFim = DateTime.Parse($"{day}/{mes}/{ano}");

            return _context.Entradas
                .Include(p => p.mercadoria).Where(p => p.DataHora >= mesInicio && p.DataHora <= mesFim).ToList();
        }
    }
}

