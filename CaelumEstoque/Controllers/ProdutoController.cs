using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CaelumEstoque.DAO;
using CaelumEstoque.Models;

namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();
            
            return View(produtos);
        }

        public ActionResult Form()
        {
            CategoriasDAO categoriaDAO = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = categoriaDAO.Lista();
            ViewBag.Categorias = categorias;
            ViewBag.Produto = new Produto();

            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Produto produto)
        {
            CategoriaDoProduto categoriaInformatica = new CategoriasDAO().BuscaPorNome("Informática");
            if (categoriaInformatica != null)
            {
                int idDadInformatica = categoriaInformatica.Id;

                if (produto.CategoriaId.Equals(idDadInformatica) && produto.Preco < 100)
                {
                    ModelState.AddModelError("produto.PrecoInvalido", "Informática com preço abaixo de 100 reais");
                }
            }
            
            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                dao.Adiciona(produto);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Produto = produto;
                CategoriasDAO categoriasDAO = new CategoriasDAO();
                ViewBag.Categorias = categoriasDAO.Lista();

                return View("Form");
            }
        }

        
    }
}