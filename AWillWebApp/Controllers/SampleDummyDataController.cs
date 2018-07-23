using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AWillWebApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SampleDummyDataController : ControllerBase
	{
		// GET: api/SampleDummyData
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		//// GET: api/SampleDummyData/5
		//[HttpGet("{id}", Name = "Get")]
		//public string Get(int id)
		//{
		//	return "value";
		//}

		//// POST: api/SampleDummyData
		//[HttpPost]
		//public void Post([FromBody] string value)
		//{
		//}

		//// PUT: api/SampleDummyData/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//// DELETE: api/ApiWithActions/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
