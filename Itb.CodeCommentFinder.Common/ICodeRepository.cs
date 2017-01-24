using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itb.CodeCommentFinder.Common
{
	public interface ICodeRepository
	{
		// TODO: needs to deal with authentication if repo not public
   	 Task<List<RepositoryFile>> GetAllFilesAsync(string userName, string repositoryName);
	}
}
