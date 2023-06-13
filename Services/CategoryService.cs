using System.Net.Http.Json;
using System.Text.Json;

namespace net_BlazorApp_WebAssembly_fundamentos_incio
{
    public class CategoryService : ICategoryService
	{
		//Atribute
		private readonly HttpClient cliente;
		private readonly JsonSerializerOptions options;

		//Construct
		public CategoryService(
			HttpClient _cliente
			) {
			this.cliente = _cliente;
			options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		//Metodos
		public async Task<List<Category>?> Get()
		{
			var response = await cliente.GetAsync("v1/categories");
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}

			return JsonSerializer.Deserialize<List<Category>>(content, options);
		}

		public async Task Add(Category objRow)
		{
			var response = await cliente.PostAsync("v1/categories", JsonContent.Create(objRow));
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
		}

		public async Task Delete(int categoryId)
		{
			var response = await cliente.DeleteAsync($"v1/categories/{categoryId}");
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
		}

		public async Task Update(int categoryId, Category objRow)
		{
			var response = await cliente.PutAsync($"v1/categories/{categoryId}", JsonContent.Create(objRow));
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
		}

	}

	public interface ICategoryService
	{
		Task<List<Category>?> Get();
		Task Add(Category objRow);
		Task Delete(int categoryId);
		Task Update(int categoryId, Category objRow);
	}
}
