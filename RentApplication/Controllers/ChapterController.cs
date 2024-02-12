using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Contracts.Managers;
using Rent.Core.Managers.Data;
using RentApplication.Models;

namespace RentApplication.Controllers
{
    [ApiController]
	[Route("api/chapter")]
	// [Authorize(Roles = "Admin")]
	public class ChapterController : ControllerBase
	{
		private readonly IChapterManager _chapterManager;

		public ChapterController(IChapterManager chapterManager)
		{
			_chapterManager = chapterManager;
		}


		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateChapter([FromBody] CreateChapterModel createModel)
		{
			if (createModel == null)
			{
				return BadRequest("Отсутствует модель данных.");
			}

			if (_chapterManager.GetList().Where(t => t.Name == createModel.Name).Any())
			{
				return BadRequest("Раздел с таким наименование уже существует.");
			}

			if (string.IsNullOrEmpty(createModel.Name))
			{
				return BadRequest("Наименование не должно быть пустым.");
			}

			if (createModel.ParentId != null && !_chapterManager.GetList().Where(t => t.Id == createModel.ParentId).Any())
			{
				return BadRequest($"Родительский раздел и id {createModel.ParentId} не найден.");
			}

			var newChapter = await _chapterManager.AddAsync(new Rent.Core.Managers.Data.ChapterData
			{
				Name = createModel.Name,
				ParentChapterId = createModel.ParentId,
			});

			return Ok(newChapter);
		}

		[HttpDelete]
		[Route("delete/{chapterid}")]
		public async Task<IActionResult> DeleteChapter([FromRoute] long chapterId)
		{
			if (!_chapterManager.GetList().Where(t => t.Id == chapterId).Any())
			{
				return NotFound();
			}

			var allChilds = GetAllChildsChapter(chapterId).OrderByDescending(t => t.ParentChapterId);

			foreach (var chapter in allChilds)
			{
				await _chapterManager.DeleteAsync(chapter.Id);
			}

			return Ok();
		}

		[HttpGet]
		[Route("get/main")]
		public async Task<IActionResult> GetMainChapters()
		{
			return Ok(_chapterManager.GetList().Where(t => t.ParentChapterId == null));
		}

		[HttpGet]
		[Route("get/childs/{chapterid}")]
		public async Task<IActionResult> GetChildsChapter([FromRoute] long chapterId)
		{
			if (!_chapterManager.GetList().Where(t => t.Id == chapterId).Any())
			{
				return NotFound();
			}

			return Ok(_chapterManager.GetList().Where(t => t.ParentChapterId == chapterId));
		}

		/// <summary>
		/// Получить все дочернии разделы, включая сам родительский
		/// </summary>
		/// <param name="parentId">ИД родительского раздела</param>
		/// <returns>Лист разделов</returns>
		private List<ChapterData> GetAllChildsChapter(long parentId)
		{
			var childs = _chapterManager.GetList().Where(t => t.ParentChapterId == parentId).ToList();
			var parent = _chapterManager.GetList().Where(t => t.Id == parentId).SingleOrDefault();
			var allChilds = new List<ChapterData> { parent };

			foreach(var child in childs)
			{
				allChilds.AddRange(GetAllChildsChapter(child.Id));
			}

			return allChilds;
		}
	}
}
