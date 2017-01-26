using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itb.CodeCommentFinder.Common
{
	public interface ICodeRepository
	{
        IEnumerable<string> ActiveFileExtensions { get; }
        Task<IEnumerable<RepositoryFile>> GetAllFilesAsync(string userName, string password, string repositoryName);
	}
}
