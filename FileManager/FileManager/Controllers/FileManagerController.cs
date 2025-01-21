using FileManager.Common;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Controllers
{
    [ApiController]
    [Route("filemanager")]
    public class FileManagerController : Controller
    {
        File_Manager _fm;


        public IActionResult ExecuteCmd( [FromServices] IWebHostEnvironment env)
        {
			// lấy đường dẫn thư mục wwwroot 
			var wwwroot = env.WebRootPath;
			// nôiis chuổi để có đường dẫn tới thư mục upload 
			var uploadPath = Path.Combine(wwwroot, "upload");
			_fm = new File_Manager(uploadPath, Request);
			return Ok(_fm.ExecuteCmd());
        }
    }
}
