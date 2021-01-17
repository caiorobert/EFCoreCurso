using EFCore.Domain;
using EFCore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		public readonly HeroiContext _context;
		public ValuesController(HeroiContext context)
		{
			_context = context;
		}
		// GET api/values
		[HttpGet("filtro/{nome}")]
		public ActionResult GetFiltro(string nome)
		{
			//LINQ Method: Linha abaixo, menos verboso a forma de buscar os dados
			var listHeroi = _context.Herois
											//.Where(h => h.Nome.Contains(nome)) //Where do método LINQ Method
											.Where(h => EF.Functions.Like(h.Nome, $"%{nome}%")) //Where usando o LIKE do SQL
											.OrderByDescending(h => h.Id) //Ordenando DESC
											//.OrderBy(h => h.Id) //Ordenação ASC normal
											//.ToList(); //Resultado para uma lista
											//.FirstOrDefault(); //Retorna o primeiro resultado que encontrar (TOP 1).
											//.SingleOrDefault(); //Faz um TOP 2, e se retornar mais de 1 resultado, é levantado uma exceção.
											//Usar a linha acima somente quando há a real necessidade de retornar apenas 1 linha do BD.
											.LastOrDefault(); //Retorna o último da lista, dependendo da ornação. É o oposto do FirstOrDefault().
			//LINQ Query Syntax: duas linhas abaixo, é como um select ao contrário, é um pouco mais verboso. Ambas as formas funcionam.
			//var listHeroi = (from heroi in _context.Herois
			//								 where heroi.Nome.Contains(nome) //Aqui um where, recebendo da rota filtro/{nome} e filtrando o nome como like '%%'
			//								 select heroi).ToList();
			return Ok(listHeroi);

			//o .ToList(); fecha a conexão com o BD quando termina, mas se for usar o foreach, como abaixo, o banco ficaria aberto se 
			//passemos o _context nos parametros do loop. Para evitar deixar a conexão aberta, faz o list igual acima (var listHeroi  = _context ...)
			//e passa esse list no parametro do for, ai ele já vai ter pego o resultado do banco, armazenado na variável, 
			//e vc usa ela, sem deixar a conexão aberta, causando problemas (lentidão por exemplo).
			//foreach(var item in listHeroi)
			//{
			//	realizarAcao();
			//	novaAcao();
			//	SalvaDados();
			//}

			//Tente sempre realizar a criação de uma variável e logo após isso, executar o looping utilizando essa variável que recebeu 
			//o valor de .ToList(); Assim, neste momento sua conexão ao banco de dados estará encerrada.
		}

		// GET api/values/5
		[HttpGet("Atualizar/{nameHero}")]
		public ActionResult<string> Get(string nameHero)
		{
			//recebe da URL a variável e salva numa var interna
			//var heroi = new Heroi { Nome = nameHero };

			//declara uma variavel e pega o valor do banco de dados, onde o Id for 3.
			var heroi = _context.Herois
											.Where(h => h.Id == 3)
											.FirstOrDefault();

			heroi.Nome = "Homem Aranha";
			//_context.Herois.Add(heroi);
			//_context.Add(heroi);
			_context.SaveChanges();
			return Ok();
		}

		// GET api/values/5
		[HttpGet("AddRange")]
		public ActionResult GetAddRange()
		{
			_context.AddRange(
				new Heroi { Nome = "Capitão América" },
				new Heroi { Nome = "Doutor Estranho" },
				new Heroi { Nome = "Pantera Negra" },
				new Heroi { Nome = "Viúva Negra" },
				new Heroi { Nome = "Hulk" },
				new Heroi { Nome = "Gavião Arqueiro" },
				new Heroi { Nome = "Capitã Marvel" }
			);

			_context.SaveChanges();

			return Ok();
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpGet("Delete/{id}")]
		public void Delete(int id)
		{
			var heroi = _context.Herois
									.Where(x => x.Id == id)
									.Single();

			_context.Herois.Remove(heroi);
			_context.SaveChanges();
		}
	}
}
