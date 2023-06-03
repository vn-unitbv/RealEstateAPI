using Core.Dtos;
using DataAccess.Entities;

namespace Core.Extensions;

public static class CommentMappingExtensions
{
	public static CommentDto ToCommentDto(this Comment comment)
	{
		CommentDto dto = new()
		{
			CommenterId = comment.Poster.Id,
			CommenterName = comment.Poster.FirstName + " " + comment.Poster.LastName,
			CommentMessage = comment.Message,
			CommentDate = comment.CreateDate.ToString("MM/dd/yyyy h:mm tt")
		};

		return dto;
	}

	public static IEnumerable<CommentDto> ToCommentDtos(this IEnumerable<Comment> comments)
	{
		return comments.Select(c => c.ToCommentDto());
	}

	public static Comment ToComment(this AddCommentDto dto)
	{
		Comment comment = new()
		{
			Message = dto.CommentMessage,
		};

		return comment;
	}
}