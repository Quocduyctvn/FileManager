using Microsoft.AspNetCore.Mvc;

namespace FileManager.Common
{
    public class File_Manager
    {
        protected string _rootPath;
		protected string _command;
		protected string _value;
        // dùng trong trường hợp đổi tên/ di chuyển file 
		protected string _secondaryValue;


		public File_Manager(string rootPath, HttpRequest request)
        {
            _rootPath = rootPath;
            _command = request.Query["cmd"].ToString();
			_value = request.Query["value"].ToString();
			_secondaryValue = request.Query["secondaryValue"].ToString();

		}

        public FileManagerResponse ExecuteCmd()
        {
            FileManagerResponse response = new();

            try
            {
				switch (_command)
				{
					case "GET_ALL_DIR":
						{
                            response.Data = GetAllDirs();
							break;
						}
				}
			}
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = "";
            }
            return response;
        }
		protected string[] GetAllDirs()   // hàm trả vè 1 mảng kiểu chuổi 
        {
            var dirs = Directory.GetDirectories(_rootPath, "*", SearchOption.AllDirectories);   // trả về 1 JSON chứa tất cả các đg dẫn của các file
            //var dirs = Directory.GetDirectories(_rootPath);   // trả về 1 JSON chứa tất cả các đg dẫn của các file

            for (int i = 0; i < dirs.Length; i++)
            {
                // dirs[i] = dirs[i].Replace(_rootPath, string.Empty).Trim('\\');  // Trim loại bỏ kí tự \\ 
                dirs[i] = dirs[i].Replace(_rootPath, string.Empty).Trim(Path.DirectorySeparatorChar);  // Path.DirectorySeparatorChar được hiẻu là kí tự trên \\ trên máy tính 

            }
            return dirs.OrderBy(i => i).ToArray();
        }
    }

    public class FileManagerResponse
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public object? Data {  get; set; }
    }
}
