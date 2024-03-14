using Rent.Core.Contracts.Helpers;
using Rent.Core.Contracts.Managers;
using Rent.Core.Managers.Data;
using Rent.Core.Models;
using Rent.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rent.Core.Helpers
{
	public class ChapterHelper : IChapterHelper
	{
		private readonly IChapterManager _chapterManager;

		public ChapterHelper(IChapterManager chapterManager)
		{
			_chapterManager = chapterManager;
		}

		public PropertiesData[] GetAllTreePropertiesByChapter(long chapterId)
		{
			throw new NotImplementedException();
		}
	}
}
