using System.Net.Http.Json;
using System.Text.Json;

namespace net_BlazorApp_WebAssembly_fundamentos_incio
{
    public class ProductService : IProductService
    {
		//Atribute
		private readonly HttpClient cliente;
		private readonly JsonSerializerOptions options;

		//Construct
		public ProductService(
			HttpClient _httpClient
			) { 
            cliente = _httpClient;
            options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

		//Metodos
		public async Task<List<Product>?> Get()
		{
            //https://api.escuelajs.co/api/v1/products
            var response = await cliente.GetAsync("v1/products");
			//Se peude usar "ReadAsStringAsync", pero no es compatible por eso se usa "ReadAsStreamAsync", ojo!
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}

			return JsonSerializer.Deserialize<List<Product>>(content, options);
		}
				
		public async Task Add(Product product)
		{
			var response = await cliente.PostAsync("v1/products", JsonContent.Create(product));
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
		}

		public async Task Delete(int productId)
		{
			var response = await cliente.DeleteAsync($"v1/products/{productId}");
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
		}

		public async Task Update(int productId, Product objrow)
		{
			var response = await cliente.PutAsync($"v1/products/{productId}", JsonContent.Create(objrow));
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
		}
	}

	public interface IProductService
	{
		Task<List<Product>?> Get();
		Task Add(Product product);
		Task Delete(int productId);
		Task Update(int productId, Product objrow);

	}
}
