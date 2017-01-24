using System;
using System.Collections.Generic;

namespace Itb.CodeCommentFinder.Common
{
	public interface ICodeRepository
	{
		// TODO: needs to deal with authentication if repo not public
   	 List<RepositoryFile> GetAllFiles(string userName, string repositoryName);
	}
}
