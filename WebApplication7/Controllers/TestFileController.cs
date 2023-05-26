using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApplication7.Controllers
{

    [ApiController]
    public class TestFileController : ControllerBase
    {
        

        [HttpPost]
        [Route("SaveFile")]

        public async Task<IActionResult> UploadFile(IFormFile file,CancellationToken cancellationToken)
        {
            if(await WriteFile(file))
            return BadRequest(new { message = " File upload Success " });
            else
            return BadRequest(new { message = "Invalid Extension  Used Valid Extension (.pdf , .doc, .docx) And File should less than 2MB " });

            return Ok(file);

        }  
        private async Task <bool> WriteFile(IFormFile file)
        {
            bool isSaveSuccess = false;

            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", file.FileName);
                string fileextension = System.IO.Path.GetExtension(file.FileName).ToLower();
                string fname = file.FileName;
                if ((fname.Contains(".pdf", StringComparison.OrdinalIgnoreCase ) || fname.Contains(".doc", StringComparison.OrdinalIgnoreCase) || fname.Contains(".docx", StringComparison.OrdinalIgnoreCase)) && file.Length < 2 * 1024 * 1024)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                        {
                        await file.CopyToAsync(stream);
                        }
                    isSaveSuccess = true;
                   
                }
                else
                {
                    isSaveSuccess = false;
                  
                }

            }
            catch(Exception )
            {
            }
            return isSaveSuccess;
        }
    }
}
